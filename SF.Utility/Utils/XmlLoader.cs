using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SF.Utility.Utils
{
    public class XmlLoader
    {
        //========================================================= //

        #region 获取XmlDocument对象

        /// <summary>
        /// 根据XML文件内容获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFileContent"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDocByXmlContent(string xmlFileContent)
        {
            if (string.IsNullOrEmpty(xmlFileContent))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.LoadXml(xmlFileContent);
            }
            catch
            {
                xDoc = null;
            }

            return xDoc;
        }

        /// <summary>
        /// 根据XML文件路径获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDocByFilePath(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlFilePath);
            }
            catch
            {
                throw new Exception(string.Format("请确认该XML文件格式正确，路径为：{0}", xmlFilePath));
            }

            return xDoc;
        }

        #endregion

        //========================================================= //


        //========================================================= //

        #region 获取XML节点（或节点列表）

        /// <summary>
        /// 获取父节点下指定节点名称的第一个子节点
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static XmlNode GetFirstChildNodeByName(XmlNode parentXmlNode, string childNodeName)
        {
            var childXmlNodes = GetChildNodesByName(parentXmlNode, childNodeName);
            if (childXmlNodes != null && childXmlNodes.Count > 0)
            {
                return childXmlNodes[0];
            }

            return null;
        }

        /// <summary>
        /// 获取父节点下指定节点名称的子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodesByName(XmlNode parentXmlNode, string nodeName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(nodeName))
            {
                return null;
            }

            return GetChildNodesByXPathExpr(parentXmlNode, string.Format(".//{0}", nodeName));
        }

        /// <summary>
        /// 获取父节点下满足xpathExpr表达式的XML子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="xpathExpr"></param>
        /// <returns></returns>   
        public static XmlNodeList GetChildNodesByXPathExpr(XmlNode parentXmlNode, string xpathExpr)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(xpathExpr))
            {
                return null;
            }

            return parentXmlNode.SelectNodes(xpathExpr);
        }

        /// <summary>
        /// 获取父节点下的第一个子节点
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <returns></returns>
        public static XmlNode GetFirstChildNode(XmlNode parentXmlNode)
        {
            var childXmlNodes = GetChildNodes(parentXmlNode);
            if (childXmlNodes != null && childXmlNodes.Count > 0)
            {
                return childXmlNodes[0];
            }

            return null;
        }

        /// <summary>
        /// 获取父节点的子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodes(XmlNode parentXmlNode)
        {
            return parentXmlNode == null ? null : parentXmlNode.ChildNodes;
        }

        #endregion
        //========================================================= //


        //========================================================= //

        #region 读取节点属性值

        /// <summary>
        /// 读取某个XML节点的属性值（根据属性名）
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string ReadAttrValue(XmlNode xmlNode, string attrName)
        {
            var xmlElement = xmlNode as XmlElement;

            return xmlElement == null ? null : xmlElement.GetAttribute(attrName);
        }

        public static bool ReadAttrBoolValue(XmlNode xmlNode, string attrName)
        {
            var xmlElement = xmlNode as XmlElement;

            return xmlElement == null ? false : ("true"==xmlElement.GetAttribute(attrName));
        }

        public static int ReadAttrIntValue(XmlNode xmlNode, string attrName, int defval=0)
        {
            var xmlElement = xmlNode as XmlElement;
            if (xmlElement == null || string.IsNullOrWhiteSpace(xmlElement.GetAttribute(attrName))) 
                return defval;

            return int.Parse(xmlElement.GetAttribute(attrName));
        }

        public static double ReadAttrDoubleValue(XmlNode xmlNode, string attrName, double defval=0.0D)
        {
            var xmlElement = xmlNode as XmlElement;

            if (xmlElement == null || string.IsNullOrWhiteSpace(xmlElement.GetAttribute(attrName)))
                return defval;

            return double.Parse(xmlElement.GetAttribute(attrName));
        }

        public static long ReadAttrLongValue(XmlNode xmlNode, string attrName, long defval=0L)
        {
            var xmlElement = xmlNode as XmlElement;
            if (xmlElement == null || string.IsNullOrWhiteSpace(xmlElement.GetAttribute(attrName)))
                return defval;

            return long.Parse(xmlElement.GetAttribute(attrName));
        }

        /// <summary>
        /// 读取父节点下指定节点名和属性名的第一个子节点的属性值
        /// </summary>
        /// <param name="parentXmlNode">XML父节点</param>
        /// <param name="childNodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string ReadFirstAttrValue(XmlNode parentXmlNode, string childNodeName, string attrName)
        {
            var attrVals = ReadAttrValues(parentXmlNode, childNodeName, attrName);
            return (attrVals == null || attrVals.Length == 0) ? null : attrVals[0];
        }

        /// <summary>
        /// 读取父节点下指定节点名和属性名的所有子节点的该属性值的数组
        /// </summary>
        /// <param name="parentXmlNode">XML文档</param>
        /// <param name="childNodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string[] ReadAttrValues(XmlNode parentXmlNode, string childNodeName, string attrName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(childNodeName) || string.IsNullOrEmpty(attrName))
            {
                return null;
            }

            var xpathExpr = string.Format("//{0}[@{1}]", childNodeName, attrName);
            var nodes = GetChildNodesByXPathExpr(parentXmlNode, xpathExpr);
            if (nodes != null && nodes.Count > 0)
            {
                var nodeCount = nodes.Count;
                var attrVals = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    attrVals[i] = ((XmlElement)nodes[i]).GetAttribute(attrName);
                }

                return attrVals;
            }

            return null;
        }

        #endregion

        //========================================================= //


        //========================================================= //

        #region 读取父节点下的子节点的文本内容

        /// <summary>
        /// 读取父节点下指定节点名的第一个子节点的文本
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static string ReadFirstChildNodeTextByName(XmlNode parentXmlNode, string childNodeName)
        {
            var childNodeTexts = ReadChildNodeTextsByName(parentXmlNode, childNodeName);
            if (childNodeTexts != null && childNodeTexts.Length > 0)
            {
                return childNodeTexts[0];
            }

            return null;
        }

        /// <summary>
        /// 读取父节点下指定节点名的所有子节点的文本数组
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static string[] ReadChildNodeTextsByName(XmlNode parentXmlNode, string childNodeName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(childNodeName))
            {
                return null;
            }

            var xpathExpr = string.Format(".//{0}", childNodeName);
            var childNodes = GetChildNodesByXPathExpr(parentXmlNode, xpathExpr);
            if (childNodes != null && childNodes.Count > 0)
            {
                var nodeCount = childNodes.Count;
                var nodeTexts = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodeTexts[i] = childNodes[i].InnerText;
                }

                return nodeTexts;
            }

            return null;
        }

        /// <summary>
        /// 读取父节点下的第一个子节点的文本
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <returns></returns>
        public static string ReadFirstChildNodeText(XmlNode parentXmlNode)
        {
            var childNodeTexts = ReadChildNodeTexts(parentXmlNode);
            if (childNodeTexts != null && childNodeTexts.Length > 0)
            {
                return childNodeTexts[0];
            }

            return null;
        }

        /// <summary>
        /// 读取父节点下的所有子节点的文本数组
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <returns></returns>
        public static string[] ReadChildNodeTexts(XmlNode parentXmlNode)
        {
            if (parentXmlNode == null)
            {
                return null;
            }

            var childNodes = GetChildNodes(parentXmlNode);
            if (childNodes != null && childNodes.Count > 0)
            {
                var nodeCount = childNodes.Count;
                var nodeTexts = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodeTexts[i] = childNodes[i].InnerText;
                }

                return nodeTexts;
            }

            return null;
        }

        /// <summary>
        /// 读取XML节点文本
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static string ReadNodeText(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                return null;
            }

            return xmlNode.InnerText;
        }

        #endregion

        //========================================================= //
    }
}
