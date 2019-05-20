using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControlDevice.Calculations
{
    [Serializable]
    public class Range
    {
        [XmlAttribute]
        public float LowValue { get; set; }

        [XmlAttribute]
        public float HighValue { get; set; }

        [XmlAttribute]
        public float RangeValue { get; set; }
    }
}
