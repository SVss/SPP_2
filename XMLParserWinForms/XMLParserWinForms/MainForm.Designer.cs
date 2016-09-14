namespace XMLParserWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator1FileMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.SaveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator2FileMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.ExitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XmlTabsControl = new System.Windows.Forms.TabControl();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ExpandAllTreeContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllTreeContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainMenu.SuspendLayout();
            this.TreeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(457, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenuItem,
            this.Separator1FileMenuItem,
            this.SaveFileMenuItem,
            this.SaveAsFileMenuItem,
            this.CloseFileMenuItem,
            this.Separator2FileMenuItem,
            this.ExitFileMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "File";
            // 
            // OpenFileMenuItem
            // 
            this.OpenFileMenuItem.Name = "OpenFileMenuItem";
            this.OpenFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.OpenFileMenuItem.Text = "Open";
            this.OpenFileMenuItem.Click += new System.EventHandler(this.OpenFileEvent);
            // 
            // Separator1FileMenuItem
            // 
            this.Separator1FileMenuItem.Name = "Separator1FileMenuItem";
            this.Separator1FileMenuItem.Size = new System.Drawing.Size(190, 6);
            // 
            // SaveFileMenuItem
            // 
            this.SaveFileMenuItem.Enabled = false;
            this.SaveFileMenuItem.Name = "SaveFileMenuItem";
            this.SaveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SaveFileMenuItem.Text = "Save";
            this.SaveFileMenuItem.Click += new System.EventHandler(this.SaveFileEvent);
            // 
            // SaveAsFileMenuItem
            // 
            this.SaveAsFileMenuItem.Enabled = false;
            this.SaveAsFileMenuItem.Name = "SaveAsFileMenuItem";
            this.SaveAsFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SaveAsFileMenuItem.Text = "Save as...";
            this.SaveAsFileMenuItem.Click += new System.EventHandler(this.SaveFileAsEvent);
            // 
            // CloseFileMenuItem
            // 
            this.CloseFileMenuItem.Enabled = false;
            this.CloseFileMenuItem.Name = "CloseFileMenuItem";
            this.CloseFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.CloseFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.CloseFileMenuItem.Text = "Close";
            this.CloseFileMenuItem.Click += new System.EventHandler(this.CloseFileEvent);
            // 
            // Separator2FileMenuItem
            // 
            this.Separator2FileMenuItem.Name = "Separator2FileMenuItem";
            this.Separator2FileMenuItem.Size = new System.Drawing.Size(190, 6);
            // 
            // ExitFileMenuItem
            // 
            this.ExitFileMenuItem.Name = "ExitFileMenuItem";
            this.ExitFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.ExitFileMenuItem.Text = "Exit";
            this.ExitFileMenuItem.Click += new System.EventHandler(this.MainForm_QuitProgram);
            // 
            // XmlTabsControl
            // 
            this.XmlTabsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XmlTabsControl.Location = new System.Drawing.Point(0, 24);
            this.XmlTabsControl.Name = "XmlTabsControl";
            this.XmlTabsControl.Padding = new System.Drawing.Point(0, 0);
            this.XmlTabsControl.SelectedIndex = 0;
            this.XmlTabsControl.Size = new System.Drawing.Size(457, 388);
            this.XmlTabsControl.TabIndex = 1;
            this.XmlTabsControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.XmlTabControlAddedEvent);
            this.XmlTabsControl.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.XmlTabControlRemovedEvent);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openDialog";
            this.OpenFileDialog.Filter = "XML-files|*.xml";
            this.OpenFileDialog.Title = "Select file to open";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "*.xml";
            this.SaveFileDialog.Filter = "XML file|*.xml";
            // 
            // ExpandAllTreeContextMenuItem
            // 
            this.ExpandAllTreeContextMenuItem.Name = "ExpandAllTreeContextMenuItem";
            this.ExpandAllTreeContextMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExpandAllTreeContextMenuItem.Text = "Expand All";
            this.ExpandAllTreeContextMenuItem.Click += new System.EventHandler(this.ExpandAllTreeEvent);
            // 
            // CollapseAllTreeContextMenuItem
            // 
            this.CollapseAllTreeContextMenuItem.Name = "CollapseAllTreeContextMenuItem";
            this.CollapseAllTreeContextMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CollapseAllTreeContextMenuItem.Text = "Collapse All";
            this.CollapseAllTreeContextMenuItem.Click += new System.EventHandler(this.CollapseAllTreeEvent);
            // 
            // TreeContextMenuStrip
            // 
            this.TreeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllTreeContextMenuItem,
            this.CollapseAllTreeContextMenuItem});
            this.TreeContextMenuStrip.Name = "TreeContextMenu";
            this.TreeContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 412);
            this.Controls.Add(this.XmlTabsControl);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "TracerXML Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingEvent);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.TreeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator Separator1FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator Separator2FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitFileMenuItem;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TabControl XmlTabsControl;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem ExpandAllTreeContextMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CollapseAllTreeContextMenuItem;
        private System.Windows.Forms.ContextMenuStrip TreeContextMenuStrip;

    }
}

