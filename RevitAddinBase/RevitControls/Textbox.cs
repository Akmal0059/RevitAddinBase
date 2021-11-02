using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls
{
    public class Textbox : RibbonItemBase
    {
        public string HintText { get; set; }
        public double TextboxWidth { get; set; }
        public string IconPath { get; set; }

        public override RibbonItem CreateRibbon(Autodesk.Revit.UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }
    }
}
