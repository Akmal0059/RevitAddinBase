using RevitAddinBase.RevitControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitAddinBase.RevitContainers
{
    public class RibbonPanel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<RibbonItemBase> Items { get; set; }
    }
}
