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




    public class CalculationViewModel : INotifyPropertyChanged
    {

        private IListenerBoard _board;
        private IOutputBoard _outputBoard;

        private double _outboundCurrent = 0;
        private double _outboundCurrentActive = 0;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private readonly Timer _cardPollTimer;

        public event PropertyChangedEventHandler PropertyChanged;

        public CalculationViewModel()
        {
            OutboundCurrent = _outboundCurrent;
            OutboundCurrentActive = _outboundCurrentActive;
            _cardPollTimer = new System.Timers.Timer(5000);

            _cardPollTimer.Elapsed += (s, e) => {
                
                InboundVoltage = _board.CardPoll();
                InboundVoltageAverage = (InboundVoltage + InboundVoltageAverage) / 2;

            };

        }



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

       



    }
}

