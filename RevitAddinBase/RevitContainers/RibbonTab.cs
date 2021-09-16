using RevitAddinBase.RevitControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RevitAddinBase.RevitContainers
{
    #region Included types
    [XmlInclude(typeof(ToggleButton))]
    [XmlInclude(typeof(PushButton))]
    [XmlInclude(typeof(ArrowButton))]
    [XmlInclude(typeof(Textbox))]
    [XmlInclude(typeof(Label))]
    [XmlInclude(typeof(RadioGroup))]
    [XmlInclude(typeof(SplitButton))]
    [XmlInclude(typeof(StackItem))]
    [XmlInclude(typeof(PullButton))]
    [XmlInclude(typeof(ComboBox))]
    [XmlInclude(typeof(RibbonPanel))]
    #endregion
    public class RibbonTab
    {
        public string Name { get; set; }
        public List<RibbonPanel> Panels { get; set; }

        public RibbonTab()
        {

        }

        public RibbonTab(string name)
        {
            Name = name;
            Panels = new List<RibbonPanel>();
        }

        public void Serialize(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(RibbonTab));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);
            }
        }
        public static RibbonTab Deserialize(string path)
        {
            RibbonTab panel = null;
            XmlSerializer formatter = new XmlSerializer(typeof(RibbonTab));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                panel = (RibbonTab)formatter.Deserialize(fs);
            }
            return panel;
        }
    }
}
