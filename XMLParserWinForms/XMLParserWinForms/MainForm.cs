using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserWinForms
{
    public partial class MainForm : Form
    {
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
                    String.Format(MessagesConsts.FileCantLoadMessage, fileName),
                    MessagesConsts.ErrorMessageCaption,
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
                //MessageBox.Show(
                //    String.Format(MessagesConsts.FileSavedMessage, info.FilePath),
                //    MessagesConsts.InfoMessageCaption,
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Information
                //    );
            }
            catch (XmlException)
            {
                result = false;
                MessageBox.Show(
                    String.Format(MessagesConsts.FileCantSaveMessage, info.FilePath),
                    MessagesConsts.ErrorMessageCaption,
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
                result = CanCloseTab(tab);
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

        private bool CanCloseTab(TabPage tab)
        {
            bool result = true;
            if (tab != null)
            {
                FileInfo info = (tab.Tag as FileInfo);
                if (info != null)
                {
                    result = CanCloseFile(info);
                }
            }
            return result;
        }

        private bool CanCloseFile(FileInfo info)
        {
            bool result = info.Saved;
            if (!result)
            {
                DialogResult answ = MessageBox.Show(
                    String.Format(MessagesConsts.FileNotSavedMessage, info.FilePath),
                    MessagesConsts.WarningMessageCaption,
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

        private void BeforeCollapseEvent(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Collapse)
                e.Cancel = true;
        }

        private void BeforeExpandEvent(object sender, TreeViewCancelEventArgs e)
        {
            if (blnDoubleClick == true && e.Action == TreeViewAction.Expand)
                e.Cancel = true;
        }

        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
                blnDoubleClick = true;
            else
                blnDoubleClick = false;
        }

        private void EditNodeEvent(object sender, TreeNodeMouseClickEventArgs e)
        {
            bool changed = EditForm.EditNodeXmlData(e.Node);
            if (changed == false)
            {
                return;
            }

            TreeView tree = (sender as TreeView);
            if (tree == null)
            {
                return;
            }

            tree.BeginUpdate();
            XmlTreeHelper.UpdateTimeUpFromNode(e.Node);
            tree.EndUpdate();

            TabPage tab = XmlTabsControl.SelectedTab;
            FileInfo info = (tab.Tag as FileInfo);
            if (info != null)
            {
                info.Saved = false;
                tab.Text = info.FileName + "*";
            }
        }

        // File Menu Items actions

        private void OpenFileEvent(object sender, EventArgs e)
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

            TreeView tree  = XmlTreeHelper.XmlDocumentToTreeView(info.Document);
            if (tree.Nodes.Count == 0)
            {
                MessageBox.Show(
                    MessagesConsts.FileCantReadMessage,
                    MessagesConsts.ErrorMessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            info.Saved = true;

            tree.Dock = DockStyle.Fill;
            tree.ContextMenuStrip = TreeContextMenuStrip;
            tab.Controls.Add(tree);
            tree.NodeMouseDoubleClick += EditNodeEvent;

            // to prevent flickering on doubleClick
            tree.MouseDown += MouseDownEvent;
            tree.BeforeCollapse += BeforeCollapseEvent;
            tree.BeforeExpand += BeforeExpandEvent;

            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void SaveFileEvent(object sender, EventArgs e)
        {
            TabPage tab = XmlTabsControl.SelectedTab;
            if (tab == null)
            {
                return;
            }

            FileInfo info = (tab.Tag as FileInfo);
            if (info == null)
            {
            }
            else
            {
                info.Saved = SaveFile(info);
                if (info.Saved)
                {
                    tab.Text = info.FileName;
                }
            }
        }

        private void SaveFileAsEvent(object sender, EventArgs e)
        {
            TabPage tab = XmlTabsControl.SelectedTab;
            if (tab == null)
            {
                return;
            }

            FileInfo info = (tab.Tag as FileInfo);
            if (info == null)
            {
            }
            else
            {
                info.Saved = SaveFileAs(ref info);
                if (info.Saved)
                {
                    tab.Text = info.FileName;
                }
            }
        }

        private void CloseFileEvent(object sender, EventArgs e)
        {
            CloseTabEvent(sender, e);
        }

        private void MainForm_QuitProgram(object sender, EventArgs e)
        {
            this.Close();
        }

        // Tab Menu Items actions

        private void NewTabEvent(object sender, EventArgs e)
        {
            TabPage tab = new TabPage("New tab");
            tab.Tag = null;

            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void CloseTabEvent(object sender, EventArgs e)
        {
            CloseSelectedTab();
        }
        
        private void FormClosingEvent(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage tab in XmlTabsControl.TabPages)
            {
                XmlTabsControl.SelectedTab = tab;
                if (CloseSelectedTab() == false)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        // TabsControl Events

        private void UpdateFileMenuItems(object sender)
        {
            SaveFileMenuItem.Enabled =
                SaveAsFileMenuItem.Enabled =
                CloseFileMenuItem.Enabled =
                ((sender as TabControl).Controls.Count > 0);
        }

        private void XmlTabControlRemovedEvent(object sender, ControlEventArgs e)
        {
            UpdateFileMenuItems(sender);
        }

        private void XmlTabControlAddedEvent(object sender, ControlEventArgs e)
        {
            UpdateFileMenuItems(sender);
        }

        // Context menu for TreeView

        private TreeView GetTree(object sender)
        {
            ToolStripMenuItem menuItem = (sender as ToolStripMenuItem);
            if (menuItem == null)
            {
                return null;
            }
            ContextMenuStrip menuStrip = (menuItem.Owner as ContextMenuStrip);
            if (menuStrip == null)
            {
                return null;
            }
            TreeView result = (menuStrip.SourceControl as TreeView);

            return result;
        }

        private void ExpandAllTreeEvent(object sender, EventArgs e)
        {
            TreeView tree = GetTree(sender);
            if (tree != null)
            {
                tree.ExpandAll();
            }
        }

        private void CollapseAllTreeEvent(object sender, EventArgs e)
        {
            TreeView tree = GetTree(sender);
            if (tree != null)
            {
                tree.CollapseAll();
            }
        }

    }

    // Constants

    internal static class MessagesConsts
    {
        public static string WarningMessageCaption { get { return "Warning"; } }
        public static string ErrorMessageCaption { get { return "Error"; } }
        public static string InfoMessageCaption { get { return "Information"; } }
        //public static string FileSavedMessage { get { return "File \"{0}\" successfully saved!"; } }
        public static string FileNotSavedMessage { get { return "File \"{0}\" is not saved.\nDo you want to save it before closing?"; } }
        public static string FileCantSaveMessage { get { return "Can't save file \"{0}\"."; } }
        public static string FileCantLoadMessage { get { return "Can't load file \"{0}\"."; } }
        public static string FileCantReadMessage { get { return "Can't read file \"{0}\"."; } }
    }

    internal static class StringConstants
    {
        public static string NewTabName { get { return "New Tab"; } }
        public static string UnsavedTabPostfix { get { return "*"; } }
    }
}
