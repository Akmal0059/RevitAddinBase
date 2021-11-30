using UI = Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase.RevitControls
{
    public class StackItem : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            UI.RibbonPanel panel = app.GetRibbonPanels(tabText).FirstOrDefault(x => x.Name == panelText);
            List<UI.RibbonItem> StackedItems = new List<UI.RibbonItem>();
            if (panel == null)
                panel = app.CreateRibbonPanel(tabText, panelText);
            if (Items.Count == 2)
                StackedItems = panel.AddStackedItems(Items[0].GetData(resources), Items[1].GetData(resources)).ToList();
            else if (Items.Count == 3)
                StackedItems = panel.AddStackedItems(Items[0].GetData(resources), Items[1].GetData(resources), Items[2].GetData(resources)).ToList();

            for (int i = 0; i < StackedItems.Count; i++)
            {
                UI.RibbonItem stackedItem = StackedItems[i];
                if(stackedItem is UI.PulldownButton pdb)
                {
                    foreach (var pullDownItem in (Items[i] as PullButton).Items)
                        pdb.AddPushButton(pullDownItem.GetData(resources) as UI.PushButtonData);
                }
                else if (stackedItem is UI.SplitButton split)
                {
                    foreach (var pullDownItem in (Items[i] as PullButton).Items)
                        split.AddPushButton(pullDownItem.GetData(resources) as UI.PushButtonData);
                }
            }

            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == tabText);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == panelText).Source;

            AdWin.RibbonRowPanel ribbon = source.Items.Last() as AdWin.RibbonRowPanel;
            int itemBaseIndex = -1;
            for (int i = 0; i < ribbon.Items.Count; i++)
            {
                AdWin.RibbonItem item = ribbon.Items[i];
                if (item is AdWin.RibbonButton ribbonBtn && !(item is AdWin.RibbonListButton))
                {
                    itemBaseIndex++;
                    SetUpRibbonItem(ribbonBtn, Items[itemBaseIndex], resources, isStacked);
                }
                else if(item is AdWin.RibbonListButton listBtn)
                {
                    itemBaseIndex++;
                    SetUpRibbonItem(listBtn, Items[itemBaseIndex], resources, isStacked);
                    for (int j = 0; j < listBtn.Items.Count; j++)
                    {
                        AdWin.RibbonButton rBtn = listBtn.Items[j] as AdWin.RibbonButton;
                        SetUpRibbonItem(rBtn, (Items[itemBaseIndex] as ButtonListBase).Items[j], resources, isStacked);
                    }
                }
            }
            //var firstItem = Items[0].CreateRibbon(app, resources, tabText, panelText, true);
            //ribbon.Items.Add(firstItem);
            //for (int i = 1; i < Items.Count; i++)
            //{
            //    ribbon.Items.Add(new AdWin.RibbonRowBreak() { MinHeight = 0 });
            //    var ribbonItem = Items[i].CreateRibbon(app, resources, tabText, panelText, true);
            //    ribbon.Items.Add(ribbonItem);
            //}

            return ribbon;
        }

        private void SetUpRibbonItem(AdWin.RibbonItem ribbonItem, RibbonItemBase itemBase, Dictionary<string, object> resources, bool isStacked)
        {
            ribbonItem.LargeImage = GetImageSource((Bitmap)itemBase.GetResx(resources, "_Button_image"));
            ribbonItem.Image = GetImageSource((Bitmap)itemBase.GetResx(resources, "_Button_image"));
            //ribbon.Description = (string)GetResx(resources, "_Button_long_description");
            ribbonItem.IsToolTipEnabled = true;

            object hideTextRes = itemBase.GetResx(resources, "_Hide_text");
            ribbonItem.ShowText = hideTextRes == null ? true : !(bool)hideTextRes;
            ribbonItem.ToolTip = new AdWin.RibbonToolTip()
            {
                Title = (string)itemBase.GetResx(resources, "_Button_caption"),
                Image = GetImageSource((Bitmap)itemBase.GetResx(resources, "_Button_tooltip_image")),
                //Content = (string)GetResx(resources, "_Button_tooltip_text"),
            };

            if (isStacked)
            {
                if (ribbonItem is AdWin.RibbonButton rb)
                {
                    rb.Orientation = System.Windows.Controls.Orientation.Horizontal;
                }
                ribbonItem.Size = AdWin.RibbonItemSize.Standard;
            }
        }
    }
}
