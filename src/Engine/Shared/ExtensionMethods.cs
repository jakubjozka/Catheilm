using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Engine.Models;
using Newtonsoft.Json.Linq;

namespace Engine.Shared
{
    // Parsing methods
    public static class ExtensionMethods
    {
        //XML
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

        //JSON
        public static string StringValueOf(this JObject jsonObject, string key)
        {
            return jsonObject[key].ToString();
        }

        public static string StringValueOf(this JToken jsonToken, string key)
        {
            return jsonToken[key].ToString();
        }

        public static int IntValueOf(this JToken jsonToken, string key)
        {
            return Convert.ToInt32(jsonToken[key]);
        }


        public static PlayerAttribute GetAttribute(this LivingEntity entity, string attributeKey)
        {
            return entity.Attributes.First(pa => pa.Key.Equals(attributeKey, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
