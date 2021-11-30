using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls
{
    public abstract class ButtonListBase:RibbonItemBase
    {
        public List<RibbonItemBase> Items { get; set; }

        public ButtonListBase()
        {
            Items = new List<RibbonItemBase>();
        }
    }
}
