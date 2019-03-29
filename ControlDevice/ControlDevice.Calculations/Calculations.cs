using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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




    public class CalculationViewModel : INotifyPropertyChanged  //winform fields update methods
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public CalculationViewModel()
        {
            _outboundCurrent = 0;
            _outboundCurrentActive = 0;
        }


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private double _outboundCurrent;
        private double _outboundCurrentActive;
        private double _inboundVoltage;
        private double _inboundVoltageAverage;

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

    }
}

