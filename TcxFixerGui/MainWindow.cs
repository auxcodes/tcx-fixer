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
using System.Xml;

namespace TcxFixerGui
{
    public partial class MainWindow : Form
    {
        private string rootDir = "";
        private string nl = Environment.NewLine;
        private string[] tcxFiles = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            if(selectFolderRadio.Checked)
            {
                FolderDialog();
                return;
            }
            if(selectFilesRadio.Checked)
            {
                FileDialog();
            }
        }

        private void FolderDialog()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string fullPath = folderBrowserDialog1.SelectedPath;
                rootDir = fullPath;
                pathTextBox.Text = fullPath;
                tcxFiles = Directory.GetFiles(rootDir, "*.tcx");
                ListFiles();
            }
        }

        private void FileDialog()
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fullPath = openFileDialog1.FileName;
                rootDir = Path.GetDirectoryName(fullPath);
                pathTextBox.Text = fullPath;
                tcxFiles = openFileDialog1.FileNames;
                ListFiles();
            }
        }

        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            // handle changes by user
            if (pathTextBox.Text != rootDir)
            {
                GetFiles(pathTextBox.Text);
                ListFiles();
            }
        }

        private void GetFiles(string path)
        {
            if (!File.Exists(path))
            {
                if (Directory.Exists(path))
                {
                    rootDir = path;
                    tcxFiles = Directory.GetFiles(rootDir, "*.tcx");
                }
                else
                {
                    MessageBox.Show("Something went wrong when trying to open:\n" + path + "\n File or Folder may not have been a vaild.",
                                    "Open Failed!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                rootDir = Path.GetDirectoryName(path);
                tcxFiles = new string[1] { path };
            }         
        }

        private void ListFiles()
        {
            if (Path.IsPathRooted(rootDir))
            {
                string fileOrFiles = tcxFiles.Length == 1 ? "file" : "files";
                OutputMessage("-- " + tcxFiles.Length + " .tcx " + fileOrFiles + " selected from: " + nl + "-- " + rootDir + nl + " ", true);
                foreach (string tcxFile in tcxFiles)
                {
                    OutputMessage(tcxFile);
                }
            }
        }

        private void SelectFolderRadio_CheckedChanged(object sender, EventArgs e)
        {
            selectFileButton.Text = "Select Folder";
            pathTextBox.Text = @"C:\Path\To\Folder...";
        }

        private void SelectFilesRadio_CheckedChanged(object sender, EventArgs e)
        {
            selectFileButton.Text = "Select Files";
            pathTextBox.Text = @"C:\Path\To\Files.tcx";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            OutputMessage("" + nl + "-----------------------------------------------------------");

            string rootPath = rootDir;
            string destinationPath = rootPath + @"\Fixed_TCX_Files";

            Directory.CreateDirectory(destinationPath);
            
            OutputMessage("  Output directory: ");
            OutputMessage("  " + destinationPath);
            OutputMessage("-----------------------------------------------------------");

            foreach (string tcxFile in tcxFiles)
            {
                string updatedFilePath = ProcessFile(tcxFile, destinationPath);
                RenameFile(updatedFilePath, destinationPath);
                OutputMessage("-----------------------------------------------------------");
            }
        }

        private void OutputMessage(string message, bool reset = false)
        {
            if(reset)
            {
                outputTextBox.Text = "";
            }
            outputTextBox.AppendText(message);
            outputTextBox.AppendText(nl);
        }

        private string ProcessFile(string sourceFile, string destinationPath)
        {
            OutputMessage("  Processing: " + Path.GetFileName(sourceFile));

            int line_to_edit = 1;
            string destinationFile = destinationPath + @"\" + Path.GetFileName(sourceFile);

            // Read the appropriate line from the file.
            string lineToWrite = null;
            using (StreamReader reader = new StreamReader(sourceFile))
            {
                for (int i = 0; i <= line_to_edit; ++i)
                {
                    if (i == line_to_edit)
                    {
                        lineToWrite = reader.ReadLine().TrimStart();
                    }
                }
            }

            if (lineToWrite == null)
                throw new InvalidDataException("Line does not exist in " + sourceFile);

            // Read from the target file and write to a new file.
            int line_number = 1;
            string line = null;
            using (StreamReader reader = new StreamReader(sourceFile))
            using (StreamWriter writer = new StreamWriter(destinationFile))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line_number == line_to_edit)
                    {
                        writer.WriteLine(lineToWrite);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                    line_number++;
                }
            }
            return destinationFile;
        }

        private string ActivityId(string xmlFile, string path)
        {
            string result = "";

            string currentNodeValue = null;
            XmlReaderSettings settings = new XmlReaderSettings() { IgnoreWhitespace = true };
            using (var reader = XmlReader.Create(xmlFile, settings))
            {
                if (reader.ReadToFollowing("Id"))
                {
                    currentNodeValue = reader.ReadInnerXml();
                }
            }
            if (!string.IsNullOrEmpty(currentNodeValue))
            {
                OutputMessage("  Activity Id: " + currentNodeValue);
                result = path + @"\" + currentNodeValue.Replace(":", "");
            }
            else
            {
                OutputMessage("  Activity Id: NOT FOUND");
                result = null;
            }

            return result;
        }

        private void RenameFile(string tcxFilePath, string destinationPath)
        {
            string activitIdName = ActivityId(tcxFilePath, destinationPath);
            if (activitIdName != null)
            {
                FileInfo fi = new FileInfo(activitIdName + ".tcx");
                if (fi.Exists)
                {
                    string time = DateTime.Now.TimeOfDay.ToString().Replace(":", "");
                    time = "_" + time.Replace(".", "");
                    activitIdName += time + ".tcx";
                }
                else
                {
                    activitIdName += ".tcx";
                }
                File.Move(tcxFilePath, activitIdName);
                OutputMessage("  File updated: " + activitIdName);
            }
            else
            {
                OutputMessage("  File updated: " + tcxFilePath);
            }
        }


    }
}
