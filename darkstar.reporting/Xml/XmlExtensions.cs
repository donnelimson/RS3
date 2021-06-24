using System;
using System.Xml;

namespace darkstar.reporting.Xml
{
    public static class XmlExtensions
    {
        public static XmlNode LookupAncestor(this XmlNode self, string ancestorName)
        {
            if (string.IsNullOrEmpty(ancestorName))
            {
                throw new ArgumentNullException("ancestorName");
            }

            XmlNode ancestor = self;
            while (ancestor.ParentNode.ChildNodes.Count == 1 && ancestor.Name != ancestorName)
            {
                ancestor = ancestor.ParentNode;
            }

            return ancestor;
        }
    }
}