using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserWinForms
{
    public partial class EditForm : Form
    {
        private static EditForm pInstanse = new EditForm();

        private EditForm()
        {
            InitializeComponent();
        }

        private bool HasErrors()
        {
            return false;
        }

        private void LoadXmlData(XmlElement xe)
        {
            pInstanse.NameTextBox.Text = xe.Attributes[XmlTreeHelper.METHOD_NAME_ATTR].Value;
            pInstanse.ParamsTextBox.Text = xe.Attributes[XmlTreeHelper.PARAMS_ATTR].Value;
            pInstanse.PackageTextBox.Text = xe.Attributes[XmlTreeHelper.PACKAGE_ATTR].Value;
            pInstanse.TimeTextBox.Text = xe.Attributes[XmlTreeHelper.TIME_ATTR].Value;
        }

        private void UpdateXmlData(XmlElement xe)
        {
            xe.SetAttribute(XmlTreeHelper.METHOD_NAME_ATTR, pInstanse.NameTextBox.Text);
            xe.SetAttribute(XmlTreeHelper.PARAMS_ATTR, pInstanse.ParamsTextBox.Text);
            xe.SetAttribute(XmlTreeHelper.PACKAGE_ATTR, pInstanse.PackageTextBox.Text);
            // add new time
            xe.SetAttribute(XmlTreeHelper.NEW_TIME_ATTR, pInstanse.TimeTextBox.Text);
        }

        private void ClearXmlData()
        {
            pInstanse.NameTextBox.Clear();
            pInstanse.ParamsTextBox.Clear();
            pInstanse.PackageTextBox.Clear();
            pInstanse.TimeTextBox.Clear();
        }

        public static bool EditNodeXmlData(TreeNode node)
        {
            XmlElement xe = (node.Tag as XmlElement);
            if (xe == null)
            {
                return false;
            }

            if (xe.Name == XmlTreeHelper.THREAD_TAG)
            {
                return false;
            }

            bool result = false;
            pInstanse.LoadXmlData(xe);

            DialogResult answ = pInstanse.ShowDialog();
            if (answ == DialogResult.OK)
            {
                pInstanse.UpdateXmlData(xe);
                result = true;
            }

            pInstanse.ClearXmlData();
            return result;
        }
    }
}
