using ControlDevice.Calculations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ViewWindow
{
    public class RangeConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var xsr = new XmlSerializer(typeof(RangeConfiguration));

            var sr = new XmlNodeReader(section);

            var result = xsr.Deserialize(sr);

            return result;
        }
    }
}
