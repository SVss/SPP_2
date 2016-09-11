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
        public MainForm()
        {
            InitializeComponent();
        }


        // File Menu Items actions

        private void OpenFile(object sender, EventArgs e)
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
                catch (XmlException ex)
                {
                    MessageBox.Show("Can't load file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                // ToDo: load document to TreeView
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            // ToDO...
        }

        private void SaveFileAs(object sender, EventArgs e)
        {
            // ToDo...
        }

        private void CloseFile(object sender, EventArgs e)
        {
            // ToDo: ask to save unsaved file [Yes/No/Cancel]; close tab on [Yes/No]
        }

        private void QuitProgram(object sender, EventArgs e)
        {
            this.Close();
        }


        // Tab Menu Items actions

        private void NewTab(object sender, EventArgs e)
        {
            TabPage tab = new TabPage("New tab");
            XmlTabsControl.TabPages.Add(tab);
            XmlTabsControl.SelectTab(tab);
        }

        private void CloseTab(object sender, EventArgs e)
        {
            // ToDo: ask for savig if not saved            
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ToDo: ask to save all unsaved files; CloseFile for each tab
            //                                      (abort on first [Cancel])
        }


        // TabsControl Events

        private void TabChanged(object sender, TabControlEventArgs e)
        {
            // ToDo: make enabled when file is opened
            SaveFileMenuItem.Enabled = SaveAsFileMenuItem.Enabled = CloseFileMenuItem.Enabled = (true);
        }

        private void XmlTabsControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            
        }
    }
}
