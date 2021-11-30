using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddinBase.RevitCommands
{
    public abstract class CommandApplicationBase : IExternalCommand
    {
        public abstract bool CanExecute();
        public abstract Result CommandExecute(ExternalCommandData commandData, ref string message, ElementSet elements);

        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (CanExecute())
            {
                WriteLog();
                return CommandExecute(commandData, ref message, elements);
            }
            else
            {
                MessageBox.Show("Нет лицензии на этот плагин!", "Ошибка!");
                WriteLog();
                return Result.Cancelled;
            }
        }

        public virtual void WriteLog()
        {

        }
    }
}
