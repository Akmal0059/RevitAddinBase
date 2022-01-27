using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;
using System.Xml.Serialization;
using Autodesk.Revit.UI;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

namespace RevitAddinBase.RevitControls
{
    public abstract class RibbonItemBase
    {
        [XmlIgnore]
        public AdWin.RibbonItem RevitRibbonItem { get; set; }
        public string CommandName { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ContextualHelp { get; set; }
        public string AvailabilityClassName { get; set; }
        public bool IsSlideOut { get; set; }
        public string Text { get; set; }
        public abstract AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false);

        public abstract RibbonItemData GetData(Dictionary<string, object> resources);
        protected static ImageSource GetImageSource(string path)
        {
            if (path == null)
                return null;

            var bitmap = new Bitmap(path);
            var imageSource = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                imageSource.BeginInit();
                imageSource.StreamSource = memory;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }
            return imageSource;
        }
        protected static ImageSource GetImageSource(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            var imageSource = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                imageSource.BeginInit();
                imageSource.StreamSource = memory;
                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                imageSource.EndInit();
            }
            return imageSource;
        }
        public object GetResx(Dictionary<string, object> resources, string key)
        {
            string dictKey = $"{CommandName}{key}";
            if (resources.ContainsKey(dictKey))
                return resources[dictKey];
            else
                return null;
        }
        public string GetId(string tabText, string panelText) => $"CustomCtrl_%CustomCtrl_%{tabText}%{panelText}%{CommandName}";

    }
}
