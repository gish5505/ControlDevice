using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDevice.CalculationTest
{
    public class SampleDataSource : INotifyPropertyChanged
    {
        public SampleDataSource()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; set; }

        public string Text { get; set; }
    }
}
