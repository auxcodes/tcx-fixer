using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcxFixerGui
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            // open file dialog
            DialogResult result = openFileDialog1.ShowDialog();            // perform file/folder action on OK click
            if (result == DialogResult.OK)
            {
                string fullPath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(fullPath);
                bool isTcxFile = Path.GetExtension(fullPath) == ".tcx" ? true : false;                // check for valid file or directory
                if (isTcxFile)
                {
                    pathTextBox.Text = fileName;
                    MessageBox.Show("File open:\n" + fullPath,
                                    "Opened file",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
                else
                {
                    // user tried to load a dodgy file
                    MessageBox.Show("Something went wrong when trying to open:\n" + fullPath + "\n File may not have been a vaild file type?",
                                    "Not a tcx file",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
