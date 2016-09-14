using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using TracerLib;

namespace XMLParserWinForms
{
    static class XmlTreeHelper
    {
        public const string NewTimeAttribute = "new-time";

        private static string XmlAttributesToSting(XmlElement node)
        {
            string result = "";
            if (node.Name == TracerLib.XmlConstants.ThreadTag)
            {
                result += TracerLib.XmlConstants.ThreadIdAttribute + "=";
                result += node.Attributes[TracerLib.XmlConstants.ThreadIdAttribute].Value + " ";

                result += TracerLib.XmlConstants.TimeAttribute + "=";
                result += node.Attributes[TracerLib.XmlConstants.TimeAttribute].Value;
            }
            else if (node.Name == TracerLib.XmlConstants.MethodTag)
            {
                result += TracerLib.XmlConstants.ParamsAttribute + "=";

                string paramsCount = "0";
                if (node.HasAttribute(TracerLib.XmlConstants.ParamsAttribute))
                {
                    paramsCount = node.Attributes[TracerLib.XmlConstants.ParamsAttribute].Value;
                }
                else
                {
                    node.SetAttribute(TracerLib.XmlConstants.ParamsAttribute, "0");
                }
                result += paramsCount + " ";

                result += TracerLib.XmlConstants.PackageAttribute + "=";
                result += node.Attributes[TracerLib.XmlConstants.PackageAttribute].Value + " ";

                result += TracerLib.XmlConstants.TimeAttribute + "=";
                result += node.Attributes[TracerLib.XmlConstants.TimeAttribute].Value;
            }
            return result;
        }

        private static string GetNodeText(XmlElement xe)
        {
            string name = xe.Name;
            if (name == TracerLib.XmlConstants.MethodTag)
            {
                name = xe.Attributes[TracerLib.XmlConstants.NameAttribute].Value;
            }
            return name + " (" + XmlAttributesToSting(xe) + ")";
        }

        private static TreeNode XmlElementToTreeNode(XmlElement xe)
        {
            TreeNode result = new TreeNode();
            result.Tag = xe;    // use Tag to store XmlElement 
            result.Text = GetNodeText(xe);
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

        // Public

        public static TreeView XmlDocumentToTreeView(XmlDocument document)
        {
            TreeView result = new TreeView();
            XmlElement xe = document.FirstChild as XmlElement;
            if (xe != null)
            {
                try
                {
                    foreach (var child in xe.ChildNodes)
                    {
                        result.Nodes.Add(XmlElementToTreeNode(child as XmlElement));
                    }
                }
                catch(XmlException)
                {
                    result.Nodes.Clear();
                }
            }
            return result;
        }

        public static void UpdateTimeUpFromNode(TreeNode node)
        {
            XmlElement xe = (node.Tag as XmlElement);
            if (xe == null)
            {
                return;
            }

            long newTime = Convert.ToInt64(xe.Attributes[NewTimeAttribute].Value);
            long oldTime = Convert.ToInt64(xe.Attributes[TracerLib.XmlConstants.TimeAttribute].Value);
            long diffTime = newTime - oldTime;

            xe.SetAttribute(TracerLib.XmlConstants.TimeAttribute, xe.Attributes[NewTimeAttribute].Value);
            xe.RemoveAttribute(NewTimeAttribute);

            node.Text = GetNodeText(xe);
            do
            {
                node = node.Parent;
                xe = (node.Tag as XmlElement);
                if (xe == null)
                {
                    return;
                }
                oldTime = Convert.ToInt64(xe.Attributes[TracerLib.XmlConstants.TimeAttribute].Value);
                newTime = oldTime + diffTime;
                xe.SetAttribute(TracerLib.XmlConstants.TimeAttribute, Convert.ToString(newTime));

                node.Text = GetNodeText(xe);
            } while (node.Parent != null);
        }

    }
}
