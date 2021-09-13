using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Windows;

namespace RevitAddinBase
{
    public enum RevitExternalApplicationType
    {
        Application = 0, 
        DBApplication = 1
    }

    public abstract class AddinApplicationBase : IExternalApplication
    {
        public static string ApplicationName { get; }
        public static string ApplicationGuid { get; }
        public static RevitExternalApplicationType ApplicationType { get; }
        public static Version ApplicationReleaseVersion { get; }
        private string RibbonControlManifestFilePath { get; }
        public static UIControlledApplication RevitUIApplication { get; set; }
        public static AddinApplicationBase Instance { get; private set; }
        private bool AllowInSessionLoading { get; } = true;

        //public static AddinApplicationBase GetInstance() => Instance; 

        private AddinApplicationBase()
        {

        }

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {


            var result = Shutdown(application);
            return result;
        }

        private bool CanBeLoaded(UIControlledApplication app, out string message)
        {
            message = "";
            if (app.IsLateAddinLoading && !AllowInSessionLoading)
            {
                message = "Loading of this application mid Revit session is not allowed. Close and start again a Revit session to load the application"; //Consider culture sensitive resource usage
                return false;
            }

            //Consider other checks

            return true;
        }

        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            //Instance = this;
            string cannotBeLoadedMessage = "";
            if (CanBeLoaded(application, out cannotBeLoadedMessage))
            {
                Autodesk.Revit.UI.TaskDialog.Show("error while loading", cannotBeLoadedMessage);
                return Result.Cancelled;
            }

            RevitUIApplication = application;

            var result = Startup(application);
            if (result != Result.Succeeded)
                return result;


            BuildRibbonControl(application);

            return Result.Succeeded;
        }

        private void BuildRibbonControl(UIControlledApplication app)
        {

        }

        public abstract Result Startup(UIControlledApplication app);
        public abstract Result Shutdown(UIControlledApplication app);
    }
}
