using CheckBox_Searcher.Helpers;
using CheckBox_Searcher.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CheckBox_Searcher
{
    public static class XmlHelper
    {
        public static void DeleteItem(string ItemId, string ItemElementName)
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
            XmlNode node = doc.SelectSingleNode("/Users/User[@id='" + ItemId[ItemId.Length - 1] + "']");

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
        public static void AddUser(XmlItem Item)
        {
            XPathNavigator last = new XPathDocument(SettingsHelper.xmlConncation).CreateNavigator().SelectSingleNode("/*/*[last()]");

            if (last != null)

            {
                string id=last.GetAttribute("id", "").ToString();
                try
                {
                    int NewId = Int32.Parse(id);
                    NewId++;
                    XDocument doc = XDocument.Load(SettingsHelper.xmlConncation);
                    XElement root = new XElement("User");
                    root.Add(new XAttribute("id", (NewId).ToString()));
                    root.Add(new XElement("Name", Item.Name));
                    root.Add(new XElement("Phone", Item.Phone));
                    root.Add(new XElement("Mail", Item.Mail));
                    root.Add(new XElement("Address", Item.Address));
                    doc.Element("Users").Add(root);
                    doc.Save(SettingsHelper.xmlConncation);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse");
                }
            }        
        }
        public static void EditUser(string ItemId, XmlItem Item)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsHelper.xmlConncation);
            XmlNode node = doc.SelectSingleNode("/Users/User[@id='" + ItemId[ItemId.Length - 1] + "']");

            // if found....
            if (node != null)
            {
                node.ChildNodes[0].InnerText = Item.Name;
                node.ChildNodes[1].InnerText = Item.Phone;
                node.ChildNodes[2].InnerText = Item.Mail;
                node.ChildNodes[3].InnerText = Item.Address;
                doc.Save(SettingsHelper.xmlConncation);
            }
        }
        public static XmlItem FindUser(string ItemId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SettingsHelper.xmlConncation);
            XmlNode node = doc.SelectSingleNode("/Users/User[@id='" + ItemId[ItemId.Length - 1] + "']");

            // if found....
            if (node != null)
            {
                XmlItem Item = new XmlItem(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText);
                return Item;
            }
            return null;
        }
        
    }
}
