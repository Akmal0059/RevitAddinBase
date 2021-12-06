using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using RevitAddinBase.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Xml.Serialization;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase
{
    public enum RevitExternalApplicationType
    {
        Application = 0,
        DBApplication = 1
    }

    public abstract class AddinApplicationBase : IExternalApplication
    {
        protected internal static string ApplicationName { get; }
        protected internal static string ApplicationGuid { get; }
        protected internal static RevitExternalApplicationType ApplicationType { get; }
        protected internal static Version ApplicationReleaseVersion { get; }
        private string RibbonControlManifestFilePath { get; }
        protected internal static UIControlledApplication RevitUIApplication { get; set; }
        protected internal static AddinApplicationBase Instance { get; private set; }
        public abstract Assembly ExecutingAssembly { get; }
        protected internal static List<RevitContainers.RibbonTab> RibbonTabs { get; private set; }
        public static AddinViewModel ViewModel { get; private set; }
        private string ExecutingAssemblyName;
        //public const string TempTabName = "TempTab";
        //public const string TempPanelName = "TempPanel";

        //public static AddinApplicationBase GetInstance() => Instance; 

        Result IExternalApplication.OnShutdown(UIControlledApplication application)
        {

            ViewModel = null;
            var result = Shutdown(application);
            return result;
        }

        protected virtual bool CanBeLoaded(UIControlledApplication app, out string message)
        {
            message = "";
            //if (app.IsLateAddinLoading && !AllowInSessionLoading)
            //{
            //    message = "Loading of this application mid Revit session is not allowed. Close and start again a Revit session to load the application"; //Consider culture sensitive resource usage
            //    return false;
            //}

            //Consider other checks

            return true;
        }

        Result IExternalApplication.OnStartup(UIControlledApplication application)
        {
            //try
            //{
            ViewModel = new AddinViewModel();
            ExecutingAssemblyName = ExecutingAssembly.GetName().Name;
            string cannotBeLoadedMessage = "";
            if (!CanBeLoaded(application, out cannotBeLoadedMessage))
            {
                Autodesk.Revit.UI.TaskDialog.Show("error while loading", cannotBeLoadedMessage);
                return Result.Cancelled;
            }

            Instance = this;
            //try
            //{
            //    application.CreateRibbonTab(TempTabName);
            //    application.CreateRibbonPanel(TempTabName, TempPanelName);
            //}
            //catch (Exception ex)
            //{

            //}
            //var panels = application.GetRibbonPanels(TempTabName);

            RevitUIApplication = application;

            var result = Startup(application);
            if (result != Result.Succeeded)
                return result;

            BuildRibbonControl(application);

            //}
            //catch (Exception ex)
            //{
            //    TaskDialog td = new TaskDialog("error");
            //    td.MainContent = ex.Message;
            //    td.ExpandedContent = ex.StackTrace;
            //    td.MainIcon = TaskDialogIcon.TaskDialogIconError;
            //    td.Show();
            //    //System.Windows.Forms.MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);

            //}
            return Result.Succeeded;
        }

        private void BuildRibbonControl(UIControlledApplication uic_app)
        {
            string resPath = GetResPath(uic_app.ControlledApplication);
            Dictionary<string, object> resourcesDictionary = new Dictionary<string, object>();

            // String resources
            using (FileStream fs = new FileStream(resPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ResXResourceReader rr = new ResXResourceReader(fs);
                foreach (DictionaryEntry item in rr)
                {
                    resourcesDictionary.Add((string)item.Key, item.Value);
                }
            }

            // Image resources
            string assemblyFolder = Path.GetDirectoryName(ExecutingAssembly.Location);
            string imgResPath = $@"{assemblyFolder}\{ExecutingAssemblyName}.Media.resx";
            using (FileStream fs = new FileStream(imgResPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ResXResourceReader rr = new ResXResourceReader(fs);
                foreach (DictionaryEntry item in rr)
                {
                    resourcesDictionary.Add((string)item.Key, item.Value);
                }
            }


            RevitContainers.RibbonTab[] tabs = Deserialize($@"{assemblyFolder}\{ExecutingAssemblyName}.xml");
            foreach (var tab in tabs)
            {
                var adWinTab = tab.CreateTab(uic_app, resourcesDictionary);
                adWinTab.Title = tab.Title;
            }
            //btn.LargeImage = ImageSourceFromBitmap((Bitmap)resorcesDictionary["Image"]);
            //btn.ItemText = (string)resorcesDictionary["Text"];
            //var control = AdWin.ComponentManager.Ribbon;
            //var tempTab = control.Tabs.FirstOrDefault(x => x.Name == TempTabName);
            //control.Tabs.Remove(tempTab);
        }
        private string GetResPath(ControlledApplication uic_app)
        {
            string resPath = "";
            switch (uic_app.Language)
            {
                case LanguageType.English_USA:
                    resPath = $"en\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.German:
                    resPath = $"de\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Spanish:
                    resPath = $"es\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.French:
                    resPath = $"fr\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Italian:
                    resPath = $"it\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Dutch:
                    resPath = $"du\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Chinese_Simplified:
                    resPath = $"ch\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Chinese_Traditional:
                    resPath = $"ch\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Japanese:
                    resPath = $"ja\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Korean:
                    resPath = $"ko\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Russian:
                    resPath = $"ru\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Czech:
                    resPath = $"cs\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Polish:
                    resPath = $"pl\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Hungarian:
                    resPath = $"hu\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.Brazilian_Portuguese:
                    resPath = $"pt\\{ExecutingAssemblyName}.resx";
                    break;
                case LanguageType.English_GB:
                    resPath = $"en\\{ExecutingAssemblyName}.resx";
                    break;
                default:
                    resPath = $"{ExecutingAssemblyName}.resx";
                    break;
            }

            string assemblyFolder = Path.GetDirectoryName(ExecutingAssembly.Location);
            if (File.Exists($@"{assemblyFolder}\{resPath}"))
                return $@"{assemblyFolder}\{resPath}";
            else
                return $@"{assemblyFolder}\{ExecutingAssemblyName}.resx";
        }
        private RevitContainers.RibbonTab[] Deserialize(string path)
        {
            RevitContainers.RibbonTab[] tabs = null;
            XmlSerializer formatter = new XmlSerializer(typeof(RevitContainers.RibbonTab[]));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                tabs = (RevitContainers.RibbonTab[])formatter.Deserialize(fs);
            }
            return tabs;
        }

        public abstract Result Startup(UIControlledApplication app);
        public abstract Result Shutdown(UIControlledApplication app);
    }
}
