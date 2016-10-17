using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserWinForms
{
    public partial class EditForm : Form
    {
        private static readonly EditForm Instanse = new EditForm();
        private XmlElement _xmlElement;

        private readonly Regex _identifierRegex = new Regex(@"[a-z_]\w*", RegexOptions.IgnoreCase);
        private readonly Regex _numberRegex = new Regex(@"(0|[1-9]\d*)");

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

            result |= !Matches(Instanse.NameTextBox.Text, _identifierRegex);
            result |= !Matches(Instanse.PackageTextBox.Text, _identifierRegex);

            result |= !Matches(Instanse.ParamsTextBox.Text, _numberRegex);
            result |= !Matches(Instanse.TimeTextBox.Text, _numberRegex);

            return result;
        }

        private bool DataNotChanged()
        {
            bool result = true;
            result &= (NameTextBox.Text.Trim() == _xmlElement.Attributes[TracerLib.XmlConstants.NameAttribute].Value);
            result &= (ParamsTextBox.Text.Trim() == _xmlElement.Attributes[TracerLib.XmlConstants.ParamsAttribute].Value);
            result &= (PackageTextBox.Text.Trim() == _xmlElement.Attributes[TracerLib.XmlConstants.PackageAttribute].Value);
            result &= (TimeTextBox.Text.Trim() == _xmlElement.Attributes[TracerLib.XmlConstants.TimeAttribute].Value);
            return result;
        }

        private void LoadXmlData(XmlElement xe)
        {
            _xmlElement = xe;
            NameTextBox.Text = xe.Attributes[TracerLib.XmlConstants.NameAttribute].Value;
            ParamsTextBox.Text = xe.Attributes[TracerLib.XmlConstants.ParamsAttribute].Value;
            PackageTextBox.Text = xe.Attributes[TracerLib.XmlConstants.PackageAttribute].Value;
            TimeTextBox.Text = xe.Attributes[TracerLib.XmlConstants.TimeAttribute].Value;
        }

        private void UpdateXmlData(XmlElement xe)
        {
            xe.SetAttribute(TracerLib.XmlConstants.NameAttribute, Instanse.NameTextBox.Text.Trim());
            xe.SetAttribute(TracerLib.XmlConstants.ParamsAttribute, Instanse.ParamsTextBox.Text.Trim());
            xe.SetAttribute(TracerLib.XmlConstants.PackageAttribute, Instanse.PackageTextBox.Text.Trim());
            // add new time
            xe.SetAttribute(XmlTreeHelper.NewTimeAttribute, Instanse.TimeTextBox.Text);
        }

        private void ClearXmlData()
        {
            _xmlElement = null;
            Instanse.NameTextBox.Clear();
            Instanse.ParamsTextBox.Clear();
            Instanse.PackageTextBox.Clear();
            Instanse.TimeTextBox.Clear();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadXmlData(_xmlElement);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (HasErrors())
            {
                MessageBox.Show(
                    MessagesConsts.HasErrorMessage,
                    MessagesConsts.WarningMessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                DialogResult = DataNotChanged() ? DialogResult.Cancel : DialogResult.OK;
                Close();
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
            Instanse.LoadXmlData(xe);

            DialogResult answ = Instanse.ShowDialog();
            if (answ == DialogResult.OK)
            {
                Instanse.UpdateXmlData(xe);
                result = true;
            }

            Instanse.ClearXmlData();
            return result;
        }
    }

    internal static partial class MessagesConsts
    {
        public static string HasErrorMessage => "There are some errors in data.\nCan't accept.";
    }
}
