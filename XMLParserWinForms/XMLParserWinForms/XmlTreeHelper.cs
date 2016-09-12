using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace XMLParserWinForms
{
    static class XmlTreeHelper
    {
        private static string XmlAttributesToSting(XmlElement node)
        {
            string result = "";
            if (node.Name == "thread")
            {
                result += "id=" + node.Attributes["id"].Value + " ";
                result += "time=" + node.Attributes["time"].Value;
            }
            else if (node.Name == "method")
            {
                result += "Params=";
                string paramsCount = "0";
                if (node.HasAttribute("params"))
                {
                    paramsCount = node.Attributes["params"].Value;
                }
                result += paramsCount + " ";
                result += "Package=" + node.Attributes["package"] + " ";
                result += "time=" + node.Attributes["time"];
            }
            return result;
        }

        public static TreeNode XmlElementToTreeNode(XmlElement xe)
        {
            TreeNode result = new TreeNode();
            result.Tag = xe;    // use Tag to store XmlElement ref

            string name = xe.Name;
            if (name == "method")
            {
                name = xe.Attributes["name"].Value;
            }

            result.Text = name + " (" + XmlAttributesToSting(xe) + ")";
            try
            {
                foreach (var child in xe.ChildNodes)
                {
                    result.Nodes.Add(XmlElementToTreeNode(child as XmlElement));
                }
            }
            catch (XmlException)
            {
                result = null;
            }
            return result;
        }


        public static bool XmlDocumentToTreeNodes(ref TreeView tree, XmlDocument document)
        {
            bool result = false;
            XmlElement xe = document.FirstChild as XmlElement;
            if (xe != null)
            {
                try
                {
                    foreach (var child in xe.ChildNodes)
                    {
                        tree.Nodes.Add(XmlElementToTreeNode(child as XmlElement));
                    }
                    result = true;
                }
                catch(XmlException)
                {
                    result = false;
                    tree.Nodes.Clear();
                }
            }
            return result;
        }
    }
}
