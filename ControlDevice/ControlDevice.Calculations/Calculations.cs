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

    

    
}

