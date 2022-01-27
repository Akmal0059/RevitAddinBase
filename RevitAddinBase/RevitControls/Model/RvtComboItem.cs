using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls.Model
{
    public class RvtComboItem
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }

        public RvtComboItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
