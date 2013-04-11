using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;

namespace Cookbook.Recipes.Core.CustomValidation.MessageRepository
{
    public class XmlMessageRepository : IMessageRepository
    {
        #region IMessageRepositoryMembers
        public IDictionary<string, string> GetMessages (string locale)
        {
            XmlDocument xDoc = new XmlDocument ();
            xDoc.Load (Assembly.GetExecutingAssembly ().GetManifestResourceStream ("CustomValidation.Messages.xml"));
            XmlNodeList resources = xDoc.SelectNodes ("messages/" + locale + "/message");
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string> ();

            foreach (XmlNode node in resources) {
                dictionary.Add (node.Attributes ["key"].Value, node.InnerText);
            }

            return dictionary;
        }
        #endregion
    }
}

