using System;
using System.Windows.Forms;
using System.Xml;

namespace XMLParserWinForms
{
    public partial class MainForm : Form
    {
        private bool _blnDoubleClick;

        public MainForm()
        {
            InitializeComponent();
        }
        
        // Internal

        private static XmlDocument LoadFile(string fileName)
        {
            XmlDocument result;
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
                    string.Format(MessagesConsts.FileCantLoadMessage, fileName),
                    MessagesConsts.ErrorMessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            return result;
        }

        private static void LoadXmlTree(TreeView tree, XmlDocument doc)
        {
            TreeNode root = XmlTreeHelper.XmlDocumentToTreeNode(doc);
            if (root == null)
            {
                MessageBox.Show(
                    MessagesConsts.FileCantReadMessage,
                    MessagesConsts.ErrorMessageCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            tree.Nodes.Clear();
            foreach(TreeNode node in root.Nodes)
            {
                tree.Nodes.Add(node);
            }
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

        private static bool SaveFile(FileInfo info)
        {
            bool result;
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
            FileInfo info = tab?.Tag as FileInfo;
            if (info != null)
            {
                result = CanCloseFile(info);
            }
            return result;
        }

        private static bool CanCloseFile(FileInfo info)
        {
            if (info.Saved)
                return true;

            DialogResult answ = MessageBox.Show(
                string.Format(MessagesConsts.FileNotSavedMessage, info.FilePath),
                MessagesConsts.WarningMessageCaption,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Exclamation
            );

            bool result = false;
            switch (answ)
            {
                case DialogResult.Cancel:
                    break;

                case DialogResult.No:
                    result = true;
                    break;

                case DialogResult.Yes:
                    result = SaveFile(info);
                    break;
            }
            return result;
        }


        private TabPage GetTabByFile(string filePath)
        {
            foreach (TabPage tab in XmlTabsControl.TabPages)
            {
                var info = (tab.Tag as FileInfo);
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
            if (_blnDoubleClick && e.Action == TreeViewAction.Collapse)
                e.Cancel = true;
        }

        private void BeforeExpandEvent(object sender, TreeViewCancelEventArgs e)
        {
            if (_blnDoubleClick && e.Action == TreeViewAction.Expand)
                e.Cancel = true;
        }

        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            _blnDoubleClick = e.Clicks > 1;
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
            MarkAsUnsaved(tab);
        }

        private static void MarkAsUnsaved(TabPage tab)
        {
            FileInfo info = (tab.Tag as FileInfo);
            if (info != null)
            {
                info.Saved = false;
                tab.Text = info.FileName + StringConstants.UnsavedTabPostfix;
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

            FileInfo info = new FileInfo(filePath)
            {
                Document = LoadFile(filePath)
            };

            if (info.Document == null)
            {
                return;
            }

            string name = info.FileName;
            tab = new TabPage(name)
            {
                Tag = info
            };

            TreeView tree = new TreeView();
            LoadXmlTree(tree, info.Document);

            info.Saved = true;

            tree.Dock = DockStyle.Fill;
            tree.ContextMenuStrip = TreeContextMenuStrip;
            tree.Name = StringConstants.TreeViewControlName;
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
            CloseTabEvent();
        }

        private void MainForm_QuitProgram(object sender, EventArgs e)
        {
            Close();
        }

        // Tab Menu Items actions

        private void CloseTabEvent()
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
            TabControl tabControl = sender as TabControl;
            if (tabControl != null)
                SaveFileMenuItem.Enabled =
                    SaveAsFileMenuItem.Enabled =
                        CloseFileMenuItem.Enabled =
                            (tabControl.Controls.Count > 0);
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
            ContextMenuStrip menuStrip = menuItem?.Owner as ContextMenuStrip;
            TreeView result = menuStrip?.SourceControl as TreeView;

            return result;
        }

        private void ExpandAllTreeEvent(object sender, EventArgs e)
        {
            TreeView tree = GetTree(sender);
            tree?.ExpandAll();
        }

        private void CollapseAllTreeEvent(object sender, EventArgs e)
        {
            TreeView tree = GetTree(sender);
            tree?.CollapseAll();
        }

    }

    // Constants

    internal static partial class MessagesConsts
    {
        public static string WarningMessageCaption => "Warning";
        public static string ErrorMessageCaption => "Error";
        public static string InfoMessageCaption => "Information";
        public static string QuestionCaption => "Question";
        //public static string FileSavedMessage { get { return "File \"{0}\" successfully saved!"; } }
        public static string FileNotSavedMessage => "File \"{0}\" is not saved.\nDo you want to save it before closing?";
        public static string FileCantSaveMessage => "Can't save file \"{0}\".";
        public static string FileCantLoadMessage => "Can't load file \"{0}\".";
        public static string FileCantReadMessage => "Can't read file \"{0}\".";
        public static string ReloadFileQuestion => "File {0} has changed.\nDo you want to reload it?";
    }

    internal static class StringConstants
    {
        public static string NewTabName => "New Tab";
        public static string UnsavedTabPostfix => "*";
        public static string TreeViewControlName => "XmlTreeView";
    }
}
