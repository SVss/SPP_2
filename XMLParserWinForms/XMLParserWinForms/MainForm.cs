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
        private const string WARNING_CAPTION = "Warning";
        private const string ERROR_CAPTION = "Error";
        private const string INFO_CAPTION = "Information";
        private const string FILE_SAVED = "File \"{0}\" successfully saved!";
        private const string FILE_NOT_SAVED = "File \"{0}\" is not saved.\nDo you want to save it before closing?";
        private const string FILE_CANT_SAVE = "Can't save file \"{0}\".";
        private const string FILE_CANT_LOAD = "Can't load file \"{0}\".";
        private const string FILE_CANT_READ = "Can't read file \"{0}\".";

        private bool blnDoubleClick = false;

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
                MessageBox.Show(
                    String.Format(FILE_CANT_LOAD, fileName),
                    ERROR_CAPTION,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
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
                MessageBox.Show(
                    String.Format(FILE_SAVED,
                    info.FilePath),
                    INFO_CAPTION,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            catch (XmlException)
            {
                result = false;
                MessageBox.Show(
                    String.Format(FILE_CANT_SAVE,
                    info.FilePath),
                    ERROR_CAPTION,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            return result;
        }

        private bool CloseSelectedTab()
        {
            return CloseTab(XmlTabsControl.SelectedIndex);
        }

        private bool CloseTab(int index)
        {
            bool result = false;
            if ((index >= 0) || (index < XmlTabsControl.TabCount))
            {
                TabPage tab = XmlTabsControl.TabPages[index];
                result = canCloseTab(tab);
                if (result)
                {
                    if (index > 0)
                    {
                        XmlTabsControl.SelectedIndex = (index - 1);
                    }
                    XmlTabsControl.TabPages.Remove(tab);
                }
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
                    String.Format(FILE_NOT_SAVED, info.FilePath),
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
            FileInfo info = null;
            foreach (TabPage tab in XmlTabsControl.TabPages)
            {
                info = (tab.Tag as FileInfo);
                if (info != null)
                {
                    if (info.FilePath == filePath)
                    {
                        return tab;
                    }
                }
            }

            return null;
        }


        // TreeView Actions

        private void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Collapse)
                e.Cancel = true;
        }

        private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Expand)
                e.Cancel = true;
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
                blnDoubleClick = true;
            else
                blnDoubleClick = false;
        }

        private void TreeView_EditNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool changed = EditForm.EditNodeXmlData(e.Node);
            if (changed)
            {
                TreeView tree = (sender as TreeView);
                if (tree == null)
                {
                    return;
                }
                tree.BeginUpdate();
                XmlTreeHelper.UpdateTreeUpFromNode(e.Node);
                tree.EndUpdate();

                TabPage tab = XmlTabsControl.SelectedTab;
                FileInfo info = (tab.Tag as FileInfo);
                if (info != null)
                {
                    info.Saved = false;
                    tab.Text = info.FileName + "*";
                }
            }
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
            tree.NodeMouseDoubleClick += TreeView_EditNode;

            // to prevent flickering on doubleClick
            tree.MouseDown += TreeView_MouseDown;
            tree.BeforeCollapse += TreeView_BeforeCollapse;
            tree.BeforeExpand += TreeView_BeforeExpand;

            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void MainForm_SaveFile(object sender, EventArgs e)
        {
            TabPage tab = XmlTabsControl.SelectedTab;
            if (tab == null)
            {
                return;
            }

            FileInfo info = (tab.Tag as FileInfo);
            if (info != null)
            {
                info.Saved = SaveFile(info);
                if (info.Saved)
                {
                    tab.Text = info.FileName;
                }
            }
            else
            {

            }
        }

        private void MainForm_SaveFileAs(object sender, EventArgs e)
        {
            TabPage tab = XmlTabsControl.SelectedTab;
            if (tab == null)
            {
                return;
            }

            FileInfo info = (tab.Tag as FileInfo);
            if (info != null)
            {
                info.Saved = SaveFileAs(ref info);
                if (info.Saved)
                {
                    tab.Text = info.FileName;
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
            CloseSelectedTab();
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileInfo info = null;
            foreach (TabPage tab in XmlTabsControl.TabPages)
            {
                info = (tab.Tag as FileInfo);
                if (info != null)
                {
                    if (info.Saved == false)
                    {
                        XmlTabsControl.SelectedTab = tab;
                        if (CloseSelectedTab() == false)
                        {
                            e.Cancel = true;
                            break;
                        }
                    }
                }
            }
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
