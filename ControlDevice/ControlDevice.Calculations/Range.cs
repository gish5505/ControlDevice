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
        public float LowValueY { get; set; }

        [XmlAttribute]
        public float HighValueY { get; set; }

        [XmlAttribute]
        public float LowValueX { get; set; }

        [XmlAttribute]
        public float HighValueX { get; set; }

        [XmlAttribute]
        public float HighValueDAC { get; set; }

        [XmlAttribute]
        public float LowValueDAC { get; set; }
    }
}
