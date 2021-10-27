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
        public string CommandName { get ; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ContextualHelp { get; set; }
        public string AvailabilityClassName { get; set; }
        public string Text { get; set; }

    }
}
