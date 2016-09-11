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

        private void OpenFile(object sender, EventArgs e)
        {
            DialogResult openResult = OpenFileDialog.ShowDialog();

            if (openResult == DialogResult.OK)
            {
                string fileName = OpenFileDialog.FileNames[0];

                // ToDO: if not opened, load XmlDocument to a new tab
                //                      and fill TreeView
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
            // ToDo: ask to save all unsaved files; CloseFile for each tab
            //                                      (abort on first [Cancel])
        }
    }
}
