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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator1FileMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.SaveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator2FileMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.ExitFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XmlTabsControl = new System.Windows.Forms.TabControl();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.TabMenuItem});
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
            this.OpenFileMenuItem.Click += new System.EventHandler(this.OpenFile);
            // 
            // Separator1FileMenuItem
            // 
            this.Separator1FileMenuItem.Name = "Separator1FileMenuItem";
            this.Separator1FileMenuItem.Size = new System.Drawing.Size(190, 6);
            // 
            // SaveFileMenuItem
            // 
            this.SaveFileMenuItem.Name = "SaveFileMenuItem";
            this.SaveFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SaveFileMenuItem.Text = "Save";
            this.SaveFileMenuItem.Click += new System.EventHandler(this.SaveFile);
            // 
            // SaveAsFileMenuItem
            // 
            this.SaveAsFileMenuItem.Name = "SaveAsFileMenuItem";
            this.SaveAsFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.SaveAsFileMenuItem.Text = "Save as...";
            this.SaveAsFileMenuItem.Click += new System.EventHandler(this.SaveFileAs);
            // 
            // CloseFileMenuItem
            // 
            this.CloseFileMenuItem.Name = "CloseFileMenuItem";
            this.CloseFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.CloseFileMenuItem.Size = new System.Drawing.Size(193, 22);
            this.CloseFileMenuItem.Text = "Close";
            this.CloseFileMenuItem.Click += new System.EventHandler(this.CloseFile);
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
            this.ExitFileMenuItem.Click += new System.EventHandler(this.QuitProgram);
            // 
            // TabMenuItem
            // 
            this.TabMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabMenuItem,
            this.CloseTabMenuItem});
            this.TabMenuItem.Name = "TabMenuItem";
            this.TabMenuItem.Size = new System.Drawing.Size(39, 20);
            this.TabMenuItem.Text = "Tab";
            // 
            // NewTabMenuItem
            // 
            this.NewTabMenuItem.Name = "NewTabMenuItem";
            this.NewTabMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.NewTabMenuItem.Size = new System.Drawing.Size(181, 22);
            this.NewTabMenuItem.Text = "New";
            this.NewTabMenuItem.Click += new System.EventHandler(this.NewTab);
            // 
            // CloseTabMenuItem
            // 
            this.CloseTabMenuItem.Name = "CloseTabMenuItem";
            this.CloseTabMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F4)));
            this.CloseTabMenuItem.Size = new System.Drawing.Size(181, 22);
            this.CloseTabMenuItem.Text = "Close";
            this.CloseTabMenuItem.Click += new System.EventHandler(this.CloseTab);
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
            this.XmlTabsControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabChanged);
            this.XmlTabsControl.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.XmlTabsControl_ControlRemoved);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openDialog";
            this.OpenFileDialog.Filter = "XML-files|*.xml";
            this.OpenFileDialog.Title = "Select file to open";
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
            this.Text = "XML Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem TabMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTabMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseTabMenuItem;

    }
}

