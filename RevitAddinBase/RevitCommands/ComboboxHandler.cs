using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;

namespace RevitAddinBase.RevitCommands
{
    public abstract class ComboboxHandler
    {
        public abstract void Combobox_DropDownOpened(object sender, UI.Events.ComboBoxDropDownOpenedEventArgs e);

        public abstract void Combobox_DropDownClosed(object sender, UI.Events.ComboBoxDropDownClosedEventArgs e);

        public abstract void Combobox_CurrentChanged(object sender, UI.Events.ComboBoxCurrentChangedEventArgs e);
    }
}
