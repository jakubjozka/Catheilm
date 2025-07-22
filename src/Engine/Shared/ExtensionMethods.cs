using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine.Shared
{
    public static class ExtensionMethods
    {
        public static int AttributesAsInt(this XmlNode node, string attributeName)
        {
            return Convert.ToInt32(node.AttributesAsString(attributeName));
        }

        public static string AttributesAsString(this XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"Attribute '{attributeName}' not found in node '{node.Name}'");
            }

            return attribute.Value;
        }
    }
}
