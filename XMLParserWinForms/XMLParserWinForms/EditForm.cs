using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserWinForms
{
    public partial class EditForm : Form
    {
        private static EditForm pInstanse = new EditForm();
        private XmlElement xmlElement = null;

        private Regex IDENTIFIER_REGEX = new Regex(@"[a-z_]\w*", RegexOptions.IgnoreCase);
        private Regex NUMBER_REGEX = new Regex(@"(0|[1-9]\d*)");

        private EditForm()
        {
            InitializeComponent();
        }

        private bool Matches(string s, Regex rx)
        {
            string text = s.Trim();
            return (rx.Match(text).Value == text);
        }

        private bool HasErrors()
        {
            bool result = false;

            result |= !Matches(pInstanse.NameTextBox.Text, IDENTIFIER_REGEX);
            result |= !Matches(pInstanse.PackageTextBox.Text, IDENTIFIER_REGEX);

            result |= !Matches(pInstanse.ParamsTextBox.Text, NUMBER_REGEX);
            result |= !Matches(pInstanse.TimeTextBox.Text, NUMBER_REGEX);

            return result;
        }

        private bool DataNotChanged()
        {
            bool result = true;
            result &= (NameTextBox.Text.Trim() == xmlElement.Attributes[TracerLib.XmlConstants.NameAttribute].Value);
            result &= (ParamsTextBox.Text.Trim() == xmlElement.Attributes[TracerLib.XmlConstants.ParamsAttribute].Value);
            result &= (PackageTextBox.Text.Trim() == xmlElement.Attributes[TracerLib.XmlConstants.PackageAttribute].Value);
            result &= (TimeTextBox.Text.Trim() == xmlElement.Attributes[TracerLib.XmlConstants.TimeAttribute].Value);
            return result;
        }

        private void LoadXmlData(XmlElement xe)
        {
            xmlElement = xe;
            NameTextBox.Text = xe.Attributes[TracerLib.XmlConstants.NameAttribute].Value;
            ParamsTextBox.Text = xe.Attributes[TracerLib.XmlConstants.ParamsAttribute].Value;
            PackageTextBox.Text = xe.Attributes[TracerLib.XmlConstants.PackageAttribute].Value;
            TimeTextBox.Text = xe.Attributes[TracerLib.XmlConstants.TimeAttribute].Value;
        }

        private void UpdateXmlData(XmlElement xe)
        {
            xe.SetAttribute(TracerLib.XmlConstants.NameAttribute, pInstanse.NameTextBox.Text.Trim());
            xe.SetAttribute(TracerLib.XmlConstants.ParamsAttribute, pInstanse.ParamsTextBox.Text.Trim());
            xe.SetAttribute(TracerLib.XmlConstants.PackageAttribute, pInstanse.PackageTextBox.Text.Trim());
            // add new time
            xe.SetAttribute(XmlTreeHelper.NewTimeAttribute, pInstanse.TimeTextBox.Text);
        }

        private void ClearXmlData()
        {
            xmlElement = null;
            pInstanse.NameTextBox.Clear();
            pInstanse.ParamsTextBox.Clear();
            pInstanse.PackageTextBox.Clear();
            pInstanse.TimeTextBox.Clear();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadXmlData(xmlElement);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (HasErrors())
            {
                MessageBox.Show(
                    "There are some errors in data.\nCan't accept.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                if (DataNotChanged())
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
                this.Close();
            }
        }

        // Public

        public static bool EditNodeXmlData(TreeNode node)
        {
            XmlElement xe = (node.Tag as XmlElement);
            if (xe == null)
            {
                return false;
            }

            if (xe.Name == TracerLib.XmlConstants.ThreadTag)
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
