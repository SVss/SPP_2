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
        private string FILE_CANT_SAVE = "Can't save file.";
        private string FILE_CANT_LOAD = "Can't load file.";
        private string FILE_CANT_READ = "Can't read file.";

        public MainForm()
        {
            InitializeComponent();
        }


        // Internal functions

        private XmlDocument LoadFile(string fileName)
        {
            XmlDocument result = null;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fileName);
                result = doc;
            }
            catch (XmlException)
            {
                result = null;
                MessageBox.Show(FILE_CANT_LOAD, ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private bool SaveFileAs(ref FileInfo info)
        {
            bool result = false;
            DialogResult answ = SaveFileDialog.ShowDialog();
            if (answ == DialogResult.OK)
            {
                string filePath = SaveFileDialog.FileNames[0];
                info.FilePath = filePath;
                result = SaveFile(info);
            }
            return result;
        }

        private bool SaveFile(FileInfo info)
        {
            bool result = false;
            try
            {
                info.Document.Save(info.FilePath);
                result = true;
            }
            catch (XmlException)
            {
                result = false;
                MessageBox.Show(FILE_CANT_SAVE, ERROR_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
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


        private TabPage GetTabByFile(string filePath)
        {
            TabPage result = null;
            FileInfo info = null;
            foreach (var tab in XmlTabsControl.Controls)
            {
                result = (tab as TabPage);
                if (result != null)
                {
                    info = (result.Tag as FileInfo);
                    if (info != null)
                    {
                        if (info.FilePath == filePath)
                        {
                            return result;
                        }
                    }
                }
            }

            return null;
        }

        // File Menu Items actions

        private void MainForm_OpenFile(object sender, EventArgs e)
        {
            DialogResult openResult = OpenFileDialog.ShowDialog();
            if (openResult != DialogResult.OK)
            {
                return;
            }
            
            string filePath = OpenFileDialog.FileNames[0];
            TabPage tab = GetTabByFile(filePath);
            if (tab != null)
            {
                XmlTabsControl.SelectedTab = tab;
                return;
            }

            FileInfo info = new FileInfo(filePath);
            info.Document = LoadFile(filePath);
            if (info.Document == null)
            {
                return;
            }

            string name = info.FileName;
            tab = new TabPage(name);
            tab.Tag = info;

            TreeView tree = new TreeView();
            if (XmlTreeHelper.XmlDocumentToTreeNodes(ref tree, info.Document) == false)
            {
                MessageBox.Show(
                    FILE_CANT_READ,
                    ERROR_CAPTION,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            info.Saved = true;

            tree.Dock = DockStyle.Fill;
            tab.Controls.Add(tree);
            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void MainForm_SaveFile(object sender, EventArgs e)
        {
            if (XmlTabsControl.SelectedTab == null)
            {
                return;
            }

            FileInfo info = (XmlTabsControl.SelectedTab.Tag as FileInfo);
            if (info != null)
            {
                SaveFile(info);
            }
            else
            {

            }
        }

        private void MainForm_SaveFileAs(object sender, EventArgs e)
        {
            if (XmlTabsControl.SelectedTab == null)
            {
                return;
            }

            FileInfo info = (XmlTabsControl.SelectedTab.Tag as FileInfo);
            if (info != null)
            {
                if (SaveFileAs(ref info))
                {
                    XmlTabsControl.SelectedTab.Text = info.FileName;
                }
            }
            else
            {

            }
        }

        private void MainForm_CloseFile(object sender, EventArgs e)
        {
            MainForm_CloseTab(sender, e);
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

            TabPage tab = XmlTabsControl.SelectedTab;
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

        private void setMenuItems(object sender)
        {
            SaveFileMenuItem.Enabled =
                SaveAsFileMenuItem.Enabled =
                CloseFileMenuItem.Enabled =
                ((sender as TabControl).Controls.Count > 0);
        }

        private void XmlTabsControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            setMenuItems(sender);
        }

        private void XmlTabsControl_ControlAdded(object sender, ControlEventArgs e)
        {
            setMenuItems(sender);
        }
    }
}
