using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ControlDevice.Calculations
{
    public class CalculationViewModel : INotifyPropertyChanged
    {

        private IListenerBoard _board;
        private IOutputBoard _outputBoard;

        private double _outboundCurrent;
        private double _outboundCurrentActive;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private double _meanderVoltage;
        private readonly Timer _cardPollTimer;
        private Timer _meanderLength;

        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<Range> _ranges;

        public CalculationViewModel()
        {           
            _cardPollTimer = new System.Timers.Timer(100);

            InternalQueue = new DoubleFixedSizeQueue(100);

            XAxisTimerQueue = new DateTimeFixedSizeQueue(100);

            _cardPollTimer.Elapsed += (s, e) =>
            {

                InboundVoltage = _board.CardPoll();

                InternalQueue.Enqueue(InboundVoltage);

                VoltageAveraging();

                OnPropertyChanged("InternalQueue");

                XAxisTimerQueue.Enqueue(DateTime.Now);

                OnPropertyChanged("XAxisTimerQueue");
            };

        }

        public CalculationViewModel(IEnumerable<Range> ranges)
            : this()
        {
            _ranges = ranges;
        }

        public DoubleFixedSizeQueue InternalQueue { get; private set; }

        public FixedSizeQueue<DateTime> XAxisTimerQueue { get; private set; }

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

            //result = new ListenerBoardMock();

            result = new ListenerBoard(0);

            return result;
        }

        private IOutputBoard GetOutputBoard()
        {
            if (_outputBoard != null)
                return _outputBoard;

            return new OutputBoard();
            //return new OutputBoardMock();
        }

        public void OutputBoardPush(float inboundCurrentFromControl)
        {
            OutboundCurrentActive = inboundCurrentFromControl; //* ValueFromRange(inboundCurrentFromControl);

            _outputBoard.BoardPushValue((float)OutboundCurrentActive);
            
        }

        public float ValueFromRange(float inboundCurrentFromControl)
        {
            float _lowValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueY).FirstOrDefault();

            float _highValueY = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueY).FirstOrDefault();

            float _lowValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.LowValueX).FirstOrDefault();

            float _highValueX = _ranges.Where(r => inboundCurrentFromControl > r.LowValueX && inboundCurrentFromControl <= r.HighValueX).Select(s => s.HighValueX).FirstOrDefault();

            float _angleValueK = (_highValueY - _lowValueY) / (_highValueX - _lowValueX);

            return _angleValueK;
        }

        public void VoltageAveraging()
        {
            int i = InternalQueue.Queue.Count - 10;

            if (i<0)
            {
                i = 0;
            }

            for (; i<InternalQueue.Queue.Count; i++)
            {
                InboundVoltageAverage = InternalQueue.Queue.ElementAt(i)+InboundVoltageAverage;
            }

            InboundVoltageAverage = InboundVoltageAverage / 10;

        }

        public void MeanderGenerate(float inboundCurrentFromControl)
        {
            _meanderLength = new Timer(100); //to be replaced for winform control

            OutputBoardPush(0);

            _meanderLength.Start();

            OutputBoardPush(inboundCurrentFromControl);

            _meanderLength.Elapsed += (s, e) =>
            {
                _meanderLength.Stop();

                OutputBoardPush(0);
            };

        }

        #region OnPropertyChanged

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

    }
}
