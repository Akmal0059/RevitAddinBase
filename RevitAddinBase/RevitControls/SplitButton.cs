﻿using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddinBase.RevitControls
{
    public class SplitButton : ButtonListBase
    {
        public int? SelectedIndex { get; set; }

        public override Autodesk.Windows.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }
    }
}
