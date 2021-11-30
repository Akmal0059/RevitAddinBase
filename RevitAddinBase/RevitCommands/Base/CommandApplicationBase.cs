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
        /// <summary>
        /// Checks if the command can be executed in the specific context.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        protected abstract bool CanExecute(ExternalCommandData commandData, ElementSet elements);

        /// <summary>
        /// Checks if the context has permission to execute command at the moment. For example, due to license.
        /// </summary>
        /// <returns></returns>
        protected abstract bool HasPermission();

        /// <summary>
        /// Main execution method of the external command.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        protected abstract Result CommandExecute(ExternalCommandData commandData, ref string message, ElementSet elements);

        /// <summary>
        /// Execution wrapper to check context conditions and write log.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (!HasPermission())
            {
                WriteLog(CommandApplicationExecutionResult.NoPermission);
                return Result.Cancelled;
            }

            if (CanExecute(commandData, elements))
            {
                Result res = CommandExecute(commandData, ref message, elements);
                WriteLog((CommandApplicationExecutionResult)res);
                return res;
            }
            else
            {
                WriteLog(CommandApplicationExecutionResult.CannotBeExecuted);
                return Result.Cancelled;
            }
        }
        /// <summary>
        /// Writes command execution log. Virtual and empty by default.
        /// </summary>
        /// <param name="result"></param>
        protected virtual void WriteLog(CommandApplicationExecutionResult result)
        {

        }

        public enum CommandApplicationExecutionResult
        {
            Failed = -1,
            Succeeded,
            Cancelled,
            CannotBeExecuted,
            NoPermission
        }
    }
}
