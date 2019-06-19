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
    public partial class CalculationViewModel : INotifyPropertyChanged
    {

        private IListenerBoard _board;
        private IOutputBoard _outputBoard;

        private double _outboundCurrent;
        private double _outboundCurrentActive;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private double _meanderVoltage;
        public float _angleValueK;
        public float _shiftAmountB;
        private readonly Timer _cardPollTimer;
        private Timer _meanderLength;

        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<Range> _ranges;

        public CalculationViewModel()
        {           
            _cardPollTimer = new System.Timers.Timer(100);

            InternalQueue = new DoubleFixedSizeQueue(100);

            InternalOutputQueue = new DoubleFixedSizeQueue(100);

            InternalOutputAnodeQueue = new DoubleFixedSizeQueue(100);

            InternalOutputPowerQueue = new DoubleFixedSizeQueue(100);

            XAxisTimerQueue = new DateTimeFixedSizeQueue(100);

            AngleValueK = new DoubleFixedSizeQueue(100);


            _cardPollTimer.Elapsed += (s, e) =>
            {
                try
                {

                    _cardPollTimer.Stop();

                    InboundVoltage = Math.Round(_board.CardPoll(), 4, MidpointRounding.ToEven);
                    InternalQueue.Enqueue(InboundVoltage);
                    VoltageAveraging();
                    OnPropertyChanged("InternalQueue");

                    XAxisTimerQueue.Enqueue(DateTime.Now);
                    OnPropertyChanged("XAxisTimerQueue");

                    InternalOutputQueue.Enqueue(OutboundCurrentActive);
                    OnPropertyChanged("OutboundCurrentActive");

                    InternalOutputAnodeQueue.Enqueue((InboundVoltage)* ValueFromRangeDAC((float)InboundVoltage)+ShiftAmountBDAC((float)InboundVoltage));
                    OnPropertyChanged("OutboundAnodeCurrentActive");

                    InternalOutputPowerQueue.Enqueue(3 * InboundVoltage * ValueFromRangeDAC((float)InboundVoltage) + ShiftAmountBDAC((float)InboundVoltage));
                    OnPropertyChanged("OutboundPowerActive");

                    AngleValueK.Enqueue(ValueFromRangeDAC((float)InboundVoltage));
                    OnPropertyChanged("AngleValueK");
                }
                finally
                {
                    _cardPollTimer.Start();
                }
            };

        }

        public CalculationViewModel(IEnumerable<Range> ranges)
            : this()
        {
            _ranges = ranges;
        }

        public DoubleFixedSizeQueue InternalQueue { get; private set; }
        public DoubleFixedSizeQueue InternalOutputQueue { get; private set; }
        public DoubleFixedSizeQueue InternalOutputAnodeQueue { get; private set; }
        public DoubleFixedSizeQueue InternalOutputPowerQueue { get; private set; }
        public FixedSizeQueue<DateTime> XAxisTimerQueue { get; private set; }
        public DoubleFixedSizeQueue AngleValueK { get; private set; }

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
            OutboundCurrentActive = inboundCurrentFromControl * ValueFromRange(inboundCurrentFromControl);

            _outboundCurrentActive = OutboundCurrentActive;

            if (_outputBoard != null) 
            {
                _outputBoard.BoardPushValue((float)OutboundCurrentActive);
            }
            
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
