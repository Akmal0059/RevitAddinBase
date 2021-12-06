using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;

namespace RevitAddinBase.RevitCommands
{
    public abstract class TextboxHandler
    {
        public abstract void Tb_EnterPressed(object sender, UI.Events.TextBoxEnterPressedEventArgs e);

    }
}
