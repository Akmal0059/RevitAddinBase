using UI = Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;
using System.Drawing;
using System.IO;

namespace RevitAddinBase.RevitControls
{
    public class PullButton : ButtonListBase
    {
        public string IconPath {  get; set; }

        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            var rapiPullDown = CreateRevitApiSplitButton(app, resources, tabText, panelText);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == tabText);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == panelText).Source;

            AdWin.RibbonSplitButton ribbon = source.Items.FirstOrDefault(x => x.Id == GetId(tabText, panelText)) as AdWin.RibbonSplitButton;
            ribbon.Image = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            ribbon.LargeImage = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            ribbon.IsSplit = false;
            for (int i = 0; i < ribbon.Items.Count; i++)
            {
                AdWin.RibbonButton item = ribbon.Items[i] as AdWin.RibbonButton;
                item.LargeImage = GetImageSource((Bitmap)Items[i].GetResx(resources, "_Button_image"));
                item.Image = GetImageSource((Bitmap)Items[i].GetResx(resources, "_Button_image"));
                //ribbon.Description = (string)GetResx(resources, "_Button_long_description");
                item.IsToolTipEnabled = true;

                object hideTextRes = Items[i].GetResx(resources, "_Hide_text");
                item.ShowText = hideTextRes == null ? true : !(bool)hideTextRes;
                item.ToolTip = new AdWin.RibbonToolTip()
                {
                    Title = (string)Items[i].GetResx(resources, "_Button_caption"),
                    Image = GetImageSource((Bitmap)Items[i].GetResx(resources, "_Button_tooltip_image")),
                    //Content = (string)GetResx(resources, "_Button_tooltip_text"),
                };

                if (isStacked)
                {
                    item.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    item.Size = AdWin.RibbonItemSize.Standard;
                }
            }
            foreach(AdWin.RibbonButton item in ribbon.Items)
            {
                

            }
            //foreach (var item in Items)
            //    ribbon.Items.Add(item.CreateRibbon(app, resources, tabText, panelText));

            //ribbon.CurrentChanged += Ribbon_CurrentChanged;

            if (isStacked)
            {
                ribbon.Orientation = System.Windows.Controls.Orientation.Horizontal;
                ribbon.Size = AdWin.RibbonItemSize.Standard;
            }
            return ribbon;
        }

        public override UI.RibbonItemData GetData(Dictionary<string, object> resources)
        {
            string name = CommandName;
            Text = (string)GetResx(resources, "_Button_caption");
            UI.PulldownButtonData pulldownData = new UI.PulldownButtonData(name, Text);
            return pulldownData;
        }

        private UI.PulldownButton CreateRevitApiSplitButton(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText)
        {
            var panel = app.GetRibbonPanels(tabText).FirstOrDefault(x => x.Name == panelText);
            if (panel == null)
                panel = app.CreateRibbonPanel(tabText, panelText);

            string name = CommandName;
            Text = (string)GetResx(resources, "_Button_caption");
            UI.PulldownButtonData pulldownData = new UI.PulldownButtonData(name, Text);
            var btn = panel.AddItem(pulldownData) as UI.PulldownButton;
            string uriStr = (string)GetResx(resources, "_Help_file_name");
            if (!string.IsNullOrWhiteSpace(uriStr))
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string helpFilePath = $@"{appDataDir}\Inpad\Revit\HelpFiles\{uriStr}";
                if (File.Exists(helpFilePath))
                    btn.SetContextualHelp(new UI.ContextualHelp(UI.ContextualHelpType.Url, helpFilePath));
            }

            foreach(var item in Items)
            {
                UI.PushButton pushButton = btn.AddPushButton(item.GetData(resources) as UI.PushButtonData);
            }

            return btn;
        }
    }
}
