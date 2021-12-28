using CheckBox_Searcher.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CheckBox_Searcher
{
    public static class  XmlHelper
    {
        public static void DeleteItem(string ItemId,string ItemElementName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsHelper.xmlConncation);
            int charLocation = ItemElementName.IndexOf(":", StringComparison.Ordinal);
            if (charLocation > 0)
            {
                XmlNode node = doc.SelectSingleNode("/Users/User[@id='" + ItemId[ItemId.Length - 1] + "']/" + ItemElementName.Substring(0, charLocation));
                // if found....
                if (node != null)
                {
                    //delete the info in the item
                    node.InnerText = "";
                    // verify the new XML structure
                    string newXML = doc.OuterXml;

                    // save to file or whatever....
                    doc.Save(SettingsHelper.xmlConncation);
                }
            }
        }
        public static void DeleteUser(string ItemId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsHelper.xmlConncation);
            XmlNode node = doc.SelectSingleNode("/Users/User[@id='"+ItemId[ItemId.Length-1]+"']");

            // if found....
            if (node != null)
            {
                // get its parent node
                XmlNode parent = node.ParentNode;

                // remove the child node
                parent.RemoveChild(node);

                // verify the new XML structure
                string newXML = doc.OuterXml;

                // save to file or whatever....
                doc.Save(SettingsHelper.xmlConncation);
            }
        }
    }
}
