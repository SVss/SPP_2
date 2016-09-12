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
        public const string METHOD_TAG = "method";
        public const string THREAD_TAG = "thread";
        public const string THREAD_ID_ATTR = "id";
        public const string METHOD_NAME_ATTR = "name";
        public const string TIME_ATTR = "time";
        public const string NEW_TIME_ATTR = "new-time";
        public const string PARAMS_ATTR = "params";
        public const string PACKAGE_ATTR = "package";

        private static string XmlAttributesToSting(XmlElement node)
        {
            string result = "";
            if (node.Name == THREAD_TAG)
            {
                result += THREAD_ID_ATTR + "=" + node.Attributes[THREAD_ID_ATTR].Value + " ";
                result += TIME_ATTR + "=" + node.Attributes[TIME_ATTR].Value;
            }
            else if (node.Name == METHOD_TAG)
            {
                result += PARAMS_ATTR + "=";
                string paramsCount = "0";
                if (node.HasAttribute(PARAMS_ATTR))
                {
                    paramsCount = node.Attributes[PARAMS_ATTR].Value;
                }
                else
                {
                    node.SetAttribute(PARAMS_ATTR, "0");
                }
                result += paramsCount + " ";
                result += PACKAGE_ATTR + "=" + node.Attributes[PACKAGE_ATTR].Value + " ";
                result += TIME_ATTR + "=" + node.Attributes[TIME_ATTR].Value;
            }
            return result;
        }

        private static string GetNodeText(XmlElement xe)
        {
            string name = xe.Name;
            if (name == METHOD_TAG)
            {
                name = xe.Attributes[METHOD_NAME_ATTR].Value;
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

        public static void UpdateTreeUpFromNode(TreeNode node)
        {
            XmlElement xe = (node.Tag as XmlElement);
            if (xe == null)
            {
                return;
            }
            long newTime = Convert.ToInt64(xe.Attributes[NEW_TIME_ATTR].Value);
            long oldTime = Convert.ToInt64(xe.Attributes[TIME_ATTR].Value);
            long diffTime = newTime - oldTime;

            xe.SetAttribute(TIME_ATTR, xe.Attributes[NEW_TIME_ATTR].Value);
            xe.RemoveAttribute(NEW_TIME_ATTR);

            node.Text = GetNodeText(xe);

            do
            {
                node = node.Parent;
                xe = (node.Tag as XmlElement);
                if (xe == null)
                {
                    return;
                }
                oldTime = Convert.ToInt64(xe.Attributes[TIME_ATTR].Value);
                newTime = oldTime + diffTime;
                xe.SetAttribute(TIME_ATTR, Convert.ToString(newTime));

                node.Text = GetNodeText(xe);
            } while (node.Parent != null);
        }

    }
}
