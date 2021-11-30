using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls
{
    public class ArrowButton : RibbonItemBase
    {
        public override Autodesk.Windows.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            throw new NotImplementedException();
        }
    }
}
