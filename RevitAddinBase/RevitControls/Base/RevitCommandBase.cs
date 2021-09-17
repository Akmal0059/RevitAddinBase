using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls
{
    public abstract class RevitCommandBase : RibbonItemBase
    {
        public string IconPath { get; set; }

        protected RevitCommandBase()
        {
             
        }
    }
}
