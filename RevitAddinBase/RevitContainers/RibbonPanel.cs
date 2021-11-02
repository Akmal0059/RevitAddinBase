﻿using RevitAddinBase.RevitControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Autodesk.Windows;
using Autodesk.Revit.UI;

namespace RevitAddinBase.RevitContainers
{
    public class RibbonPanel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<RibbonItemBase> Items { get; set; }

        public Autodesk.Windows.RibbonPanel CreatePanel(UIControlledApplication app, Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }
    }
}
