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

        private double _outboundCurrentActive;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private double _meanderVoltage;
        public bool _autoModeActive = false;
        public float _angleValueK;
        public float _shiftAmountB;
        public float _autoModeTarget;
        private readonly Timer _cardPollTimer;
        private Timer _meanderLength;

        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<Range> _ranges;

        public CalculationViewModel()
        {           
            _cardPollTimer = new Timer(25);

            InternalQueue = new DoubleFixedSizeQueue(100);

            InternalOutputQueue = new DoubleFixedSizeQueue(100);

            InternalInputAnodeQueue = new DoubleFixedSizeQueue(100);

            InternalInputPowerQueue = new DoubleFixedSizeQueue(100);

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

                    InternalInputAnodeQueue.Enqueue((InboundVoltage * ValueFromRangeDAC((float)InboundVoltage) + ShiftAmountBDAC((float)InboundVoltage)) / 10);
                    OnPropertyChanged("OutboundAnodeCurrentActive");

                    InternalInputPowerQueue.Enqueue(3 * InboundVoltage * ValueFromRangeDAC((float)InboundVoltage + ShiftAmountBDAC((float)InboundVoltage)) / 10);
                    OnPropertyChanged("OutboundPowerActive");

                    AngleValueK.Enqueue(ValueFromRangeDAC((float)InboundVoltage));
                    OnPropertyChanged("AngleValueK");

                    InternalOutputQueue.Enqueue(OutboundCurrentActive);
                    OnPropertyChanged("OutboundCurrentActive");

                    InternalOutputAnodeQueue.Enqueue(10 * _outboundCurrentActive * ValueFromRange(10 * (float)_outboundCurrentActive) + ShiftAmountB(10 * (float)_outboundCurrentActive));
                    OnPropertyChanged("OutboundCurrentActiveAnode");

                    InternalOutputPowerQueue.Enqueue(10 * (_outboundCurrentActive / 3) * ValueFromRange(10 * ((float)_outboundCurrentActive / 3)) + ShiftAmountB(10 * ((float)_outboundCurrentActive / 3)));
                    OnPropertyChanged("OutboundCurrentActivePower");

                    if (_autoModeActive)
                    {
                        AutoMode(2);
                    }
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
        public DoubleFixedSizeQueue InternalInputAnodeQueue { get; private set; }
        public DoubleFixedSizeQueue InternalInputPowerQueue { get; private set; }
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
            OutboundCurrentActive = inboundCurrentFromControl;// * ValueFromRange(inboundCurrentFromControl);

            _outboundCurrentActive = OutboundCurrentActive;

            if (_outputBoard != null) 
            {
                _outputBoard.BoardPushValue((float)OutboundCurrentActive);
            }
            
        }

        public void OutputBoardPushPower(float inboundCurrentFromControl)
        {
            OutboundCurrentActive = 10 * (inboundCurrentFromControl / 3) * ValueFromRange(10 * (inboundCurrentFromControl / 3)) + ShiftAmountB(10 * (inboundCurrentFromControl / 3));

            _outboundCurrentActive = OutboundCurrentActive;

            if (_outputBoard != null)
            {
                _outputBoard.BoardPushValue((float)OutboundCurrentActive);
            }

        }

        public void OutputBoardPushAnodeCurrent(float inboundCurrentFromControl)
        {
            OutboundCurrentActive = 10 * inboundCurrentFromControl * ValueFromRange(10 * inboundCurrentFromControl) + ShiftAmountB(10 * inboundCurrentFromControl);

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

        public void AutoMode(float autoModeTarget)
        {
            autoModeTarget = 2.9f;

            float _currentChange;
            double _deltaI = -0.05 * (InboundVoltage - 2.9);
            /*if (Math.Abs(_deltaI) > 0.3) 
            {
                _deltaI = -0.2 * Math.Sign(InboundVoltage - 2.9);
            }*/
            
            float _pushCurrent = (float)(OutboundCurrentActive + _deltaI);
            OutputBoardPush(_pushCurrent);
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
