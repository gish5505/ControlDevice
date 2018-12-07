using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
