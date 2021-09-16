using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Windows;
using System.Xml.Serialization;

namespace RevitAddinBase.RevitControls
{
    public abstract class RibbonItemBase
    {
        [XmlIgnore]
        public RibbonItem RevitRibbonItem { get; set; }
        public string Name { get ; set; }
        public string Description { get; set; }
        public string Text { get; set; }

    }
}
