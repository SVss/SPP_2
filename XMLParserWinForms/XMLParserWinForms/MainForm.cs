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
    public partial class MainForm : Form
    {
        private string WARNING_CAPTION = "Warning";
        private string ERROR_CAPTION = "Error";
        private string FILE_NOT_SAVED = "File is not saved.\nDo you want to save it before closing?";
        private string FILE_EXISTS_REWRITE = "File already exists.\nDo you want to rewrite it?";
        private string FILE_CANT_SAVE = "Can't save file.";

        public MainForm()
        {
            InitializeComponent();
        }


        // Internal functions

        private bool SaveFileAs(FileInfo info)
        {
            bool result = false;
            DialogResult answ = SaveFileDialog.ShowDialog();
            if (answ == DialogResult.OK)
            {
                string fileName = SaveFileDialog.FileNames[0];
                if (System.IO.File.Exists(fileName))
                {
                    result = canRewriteFile();
                }

                info.FileName = fileName;
                result = SaveFile(info);
            }
            return result;
        }

        private bool SaveFile(FileInfo info)
        {
            bool result = false;
            try
            {
                info.Document.Save(info.FileName);
                result = true;
            }
            catch (XmlException)
            {
                result = false;
                MessageBox.Show(FILE_CANT_SAVE, ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private bool canRewriteFile()
        {
            DialogResult answ = MessageBox.Show(
                FILE_EXISTS_REWRITE,
                WARNING_CAPTION,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
            return (answ == DialogResult.Yes);
        }

        private bool canCloseTab(TabPage tab)
        {
            bool result = true;
            if (tab != null)
            {
                FileInfo info = (tab.Tag as FileInfo);
                if (info != null)
                {
                    result = canCloseFile(info);
                }
            }
            return result;
        }

        private bool canCloseFile(FileInfo info)
        {
            bool result = info.Saved;
            if (!result)
            {
                DialogResult answ = MessageBox.Show(
                    FILE_NOT_SAVED,
                    WARNING_CAPTION,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation
                    );

                switch (answ)
                {
                    case DialogResult.Cancel:
                        result = false;
                        break;

                    case DialogResult.No:
                        result = true;
                        break;

                    case DialogResult.Yes:
                        result = SaveFile(info);
                        break;
                }
            }
            return result;
        }


        // File Menu Items actions

        private void MainForm_OpenFile(object sender, EventArgs e)
        {
            DialogResult openResult = OpenFileDialog.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                string fileName = OpenFileDialog.FileNames[0];

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(fileName);
                }
                catch (XmlException)
                {
                    MessageBox.Show("Can't load file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                // ToDo: load document to TreeView
            }
        }

        private void MainForm_SaveFile(object sender, EventArgs e)
        {
            // ToDO...
        }

        private void MainForm_SaveFileAs(object sender, EventArgs e)
        {
            // ToDo...
        }

        private void MainForm_CloseFile(object sender, EventArgs e)
        {
            // ToDo: ask to save unsaved file [Yes/No/Cancel]; close tab on [Yes/No]
        }

        private void MainForm_QuitProgram(object sender, EventArgs e)
        {
            this.Close();
        }


        // Tab Menu Items actions

        private void MainForm_NewTab(object sender, EventArgs e)
        {
            TabPage tab = new TabPage("New tab");
            tab.Tag = null;

            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void MainForm_CloseTab(object sender, EventArgs e)
        {
            int index = XmlTabsControl.SelectedIndex;
            if ((index < 0) || (index > XmlTabsControl.TabCount))
                return;

            TabPage tab = (XmlTabsControl.Controls[index] as TabPage);
            if (canCloseTab(tab))
            {
                if (index > 0)
                {
                    XmlTabsControl.SelectedIndex = (index - 1);
                }

                XmlTabsControl.Controls.Remove(tab);
            }
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ToDo: ask to save all unsaved files; CloseFile for each tab
            //                                      (abort on first [Cancel])
        }


        // TabsControl Events

        private void XmlTabsControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            SaveFileMenuItem.Enabled =
                SaveAsFileMenuItem.Enabled =
                CloseFileMenuItem.Enabled =
                (XmlTabsControl.TabCount > 0);
        }
    }
}
