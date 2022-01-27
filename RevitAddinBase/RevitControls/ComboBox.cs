using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using RevitAddinBase.RevitControls.Model;

namespace RevitAddinBase.RevitControls
{
    public class ComboBox : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            throw new NotImplementedException();
        }

        public override UI.RibbonItemData GetData(Dictionary<string, object> resources)
        {
            string name = CommandName;
            UI.ComboBoxData cboboboxData = new UI.ComboBoxData(name);
            return cboboboxData;
        }
        public virtual void AddEventHandlers(AdWin.RibbonCombo control)
        {

        }

        public virtual void SetDataTemplate(AdWin.RibbonCombo combo)
        {
            
        }
    }
}
