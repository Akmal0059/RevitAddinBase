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
        public string Id => $"CustomCtrl_%CustomCtrl_%{AddinApplicationBase.TempTabName}%{AddinApplicationBase.TempPanelName}%{CommandName}";

        public abstract AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false);

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
        protected object GetResx(Dictionary<string, object> resources, string key)
        {
            string dictKey = $"{CommandName}{key}";
            if (resources.ContainsKey(dictKey))
                return resources[dictKey];
            else
                return null;
        }

    }
}
