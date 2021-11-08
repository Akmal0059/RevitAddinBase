using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
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

        private void BuildRibbonControl(UIControlledApplication uic_app)
        {
            string resPath = GetResPath(uic_app.ControlledApplication);
            Dictionary<string, object> resourcesDictionary = new Dictionary<string, object>();

            // String resources
            using (FileStream fs = new FileStream(resPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ResourceReader rr = new ResourceReader(fs);
                foreach (DictionaryEntry item in rr)
                {
                    resourcesDictionary.Add((string)item.Key, item.Value);
                }
            }

            // Image resources
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string imgResPath = $@"{assemblyFolder}\InpadPlugins.Images.resources";
            using (FileStream fs = new FileStream(imgResPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ResXResourceReader rr = new ResXResourceReader(fs);
                foreach (DictionaryEntry item in rr)
                {
                    resourcesDictionary.Add((string)item.Key, item.Value);
                }
            }


            RevitContainers.RibbonTab[] tabs = Deserialize($@"{assemblyFolder}\InpadPlugins.xml");
            foreach (var tab in tabs)
            {
                var adWinTab = tab.CreateTab(uic_app, resourcesDictionary);
                adWinTab.Title = tab.Title;
                AdWin.ComponentManager.Ribbon.Tabs.Add(adWinTab);
            }
            //btn.LargeImage = ImageSourceFromBitmap((Bitmap)resorcesDictionary["Image"]);
            //btn.ItemText = (string)resorcesDictionary["Text"];

        }
        private string GetResPath(ControlledApplication uic_app)
        {
            string resPath = "";
            switch (uic_app.Language)
            {
                case LanguageType.English_USA:
                    resPath = "en\\InpadPlugins.resources";
                    break;
                case LanguageType.German:
                    resPath = "de\\InpadPlugins.resources";
                    break;
                case LanguageType.Spanish:
                    resPath = "es\\InpadPlugins.resources";
                    break;
                case LanguageType.French:
                    resPath = "fr\\InpadPlugins.resources";
                    break;
                case LanguageType.Italian:
                    resPath = "it\\InpadPlugins.resources";
                    break;
                case LanguageType.Dutch:
                    resPath = "du\\InpadPlugins.resources";
                    break;
                case LanguageType.Chinese_Simplified:
                    resPath = "ch\\InpadPlugins.resources";
                    break;
                case LanguageType.Chinese_Traditional:
                    resPath = "ch\\InpadPlugins.resources";
                    break;
                case LanguageType.Japanese:
                    resPath = "ja\\InpadPlugins.resources";
                    break;
                case LanguageType.Korean:
                    resPath = "ko\\InpadPlugins.resources";
                    break;
                case LanguageType.Russian:
                    resPath = "ru\\InpadPlugins.resources";
                    break;
                case LanguageType.Czech:
                    resPath = "cs\\InpadPlugins.resources";
                    break;
                case LanguageType.Polish:
                    resPath = "pl\\InpadPlugins.resources";
                    break;
                case LanguageType.Hungarian:
                    resPath = "hu\\InpadPlugins.resources";
                    break;
                case LanguageType.Brazilian_Portuguese:
                    resPath = "pt\\InpadPlugins.resources";
                    break;
                case LanguageType.English_GB:
                    resPath = "en\\InpadPlugins.resources";
                    break;
                default:
                    resPath = "InpadPlugins.resources";
                    break;
            }

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (File.Exists($@"{assemblyFolder}\{resPath}"))
                return $@"{assemblyFolder}\{resPath}";
            else
                return $@"{assemblyFolder}\InpadPlugins.resources";
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
