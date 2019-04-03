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

        private double _outboundCurrent = 0;
        private double _outboundCurrentActive = 0;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;
        private readonly Timer _cardPollTimer;

        public event PropertyChangedEventHandler PropertyChanged;

        public CalculationViewModel()
        {            
            _cardPollTimer = new System.Timers.Timer(1000);
            _cardPollTimer.Elapsed += (s, e) => {
                
                InboundVoltage = _board.CardPoll();
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

        private void SetPollTimer()
        {
                                                                            

        }



    }
}

