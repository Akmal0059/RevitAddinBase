using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Windows;

namespace RevitAddinBase
{
    public abstract class RibbonItemBase
    {
        public RibbonItem RevitRibbonItem { get; private set; }
        public string Name { get ; private set; }
        public string Description { get; private set; }
        public string Text { get; private set; }

    }
}
