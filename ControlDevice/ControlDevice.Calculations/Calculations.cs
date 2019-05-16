using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ControlDevice.Models;
using System.Timers;

namespace ControlDevice.Calculations
{

    public interface ICalculations
    {
        float CurrentValueCalculation();



    }

    public class Calculations : ICalculations
    {
        public float CurrentValueCalculation()
        {

            return 1.1F;

        }
    }

    public interface IDataSourceQueue
    {
        float DataSourceQueue();

    }

    public class FixedSizeQueue<T> 
    {
        private Queue<T> _fixedSizeQueue;

        public FixedSizeQueue(int limit)
        {
            Limit = limit;
            _fixedSizeQueue = new Queue<T>(limit);
        }
                
        public int Limit { get; private set; }

        public Queue<T> Queue { get { return _fixedSizeQueue; } }

        public void Enqueue(T obj)
        {
            if (_fixedSizeQueue.Count < Limit)
            {
                _fixedSizeQueue.Enqueue(obj);
            }
            else
            {
                _fixedSizeQueue.Dequeue();
            }
        }

    }

    public class DoubleFixedSizeQueue : FixedSizeQueue<double>
    {
        public DoubleFixedSizeQueue(int limit) 
            : base(limit)
        {
        }

    }

    public class DateTimeFixedSizeQueue : FixedSizeQueue<DateTime>
    {
        public DateTimeFixedSizeQueue(int limit)
            : base(limit)
        {

        }

    }

    public class CalculationViewModel : INotifyPropertyChanged
    {

        private IListenerBoard _board;
        private IOutputBoard _outputBoard;

        private double _outboundCurrent;
        private double _outboundCurrentActive;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private readonly Timer _cardPollTimer;

        public event PropertyChangedEventHandler PropertyChanged;

        public CalculationViewModel()
        {
            OutboundCurrent = _outboundCurrent;
            OutboundCurrentActive = _outboundCurrentActive;
            _cardPollTimer = new System.Timers.Timer(100);

            InternalQueue = new DoubleFixedSizeQueue(100);

            XAxisTimerQueue = new DateTimeFixedSizeQueue(10);

            _cardPollTimer.Elapsed += (s, e) => 
            {
                
                InboundVoltage = _board.CardPoll();

                InboundVoltageAverage = (InboundVoltage + InboundVoltageAverage) / 2;

                InternalQueue.Enqueue(InboundVoltageAverage);

                OnPropertyChanged("InternalQueue");

                XAxisTimerQueue.Enqueue(DateTime.Now);

                OnPropertyChanged("XAxisTimerQueue");
            };

        }

        public DoubleFixedSizeQueue InternalQueue { get; private set; }
        public FixedSizeQueue<DateTime> XAxisTimerQueue { get; private set; }



        #region OnPropertyChanged
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public double OutboundCurrent
        {
            get { return _outboundCurrent; }
            set { _outboundCurrent = value; OnPropertyChanged("OutboundCurrent"); }
        }


        public double OutboundCurrentActive
        {
            get { return _outboundCurrentActive; }
            set { _outboundCurrentActive = value; OnPropertyChanged("OutboundCurrentActive"); }
        }

        public double InboundVoltage
        {
            get { return _inboundVoltage; }
            set { _inboundVoltage = value; OnPropertyChanged("InboundVoltage"); }


        }

        public double InboundVoltageAverage
        {
            get { return _inboundVoltageAverage; }
            set { _inboundVoltageAverage = value; OnPropertyChanged("InboundVoltageAverage"); }
            
        }
        #endregion

        public void Start()
        {
            _board = GetListenerBoard();
            _cardPollTimer.Start();
            _outputBoard = GetOutputBoard();
        }

        public void Stop()
        {
            _cardPollTimer.Stop();
        }

        private IListenerBoard GetListenerBoard() // method for activating board in system
        {
            if (_board != null)
                return _board;

            IListenerBoard result;


            
            result = new ListenerBoardMock();
            
            //result = new ListenerBoard(0);

            return result;
        }

        private IOutputBoard GetOutputBoard()
        {
            if (_outputBoard != null)
                return _outputBoard;

            //return new OutputBoard();
            return new OutputBoardMock();
        }

       public void OutputBoardPush(float inboundCurrentFromControl)
        {

            _outboundCurrentActive = inboundCurrentFromControl;
            _outputBoard.BoardPushValue((float)inboundCurrentFromControl);

        }



    }
}

