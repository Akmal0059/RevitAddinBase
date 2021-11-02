using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase.RevitControls
{
    public class PushButton : RevitCommandBase
    {
        public PushButton()
        {

        }

        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }
    }
}
