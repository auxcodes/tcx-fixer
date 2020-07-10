using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace TcxFixerGui
{
    public partial class MainWindow : Form
    {
        private string rootDir = "";
        private readonly string nl = Environment.NewLine;
        private string[] tcxFiles = null;

        private Timer typingTimer;
        private readonly int textChangedTimeout = 1 * 1000; // 1 sec
        private bool hasBrowsed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Triggers opening of a file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog();
        }

        /// <summary>
        /// Opens a files dialog and lists selected files in output text box
        /// </summary>
        private void OpenFileDialog()
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                hasBrowsed = true;
                string fullPath = openFileDialog1.FileName;
                rootDir = Path.GetDirectoryName(fullPath);
                pathTextBox.Text = rootDir;
                tcxFiles = openFileDialog1.FileNames;
                ListFiles();
            }
        }

        /// <summary>
        /// Triggers text input timer when user types in the file path text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!hasBrowsed)
            {
                TextChangedTimer();
            }
        }

        /// <summary>
        /// Timer function for monitoring user input to the file path text box
        /// </summary>
        private void TextChangedTimer()
        {
            if (typingTimer != null)
            {
                typingTimer.Stop();
                OutputMessage(pathTextBox.Text, true);
            }

            if (typingTimer == null || typingTimer.Interval != textChangedTimeout)
            {
                typingTimer = new Timer();
                typingTimer.Tick += new EventHandler(HandleTextChangedTimeout);
                typingTimer.Interval = textChangedTimeout;
            }

            typingTimer.Start();
        }

        /// <summary>
        /// Stops timer and lists files in output text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleTextChangedTimeout(object sender, EventArgs e)
        {
            Timer timer = sender as Timer;
            timer.Stop();

            if (GetFiles(pathTextBox.Text))
            {
                ListFiles();
            }
        }

        /// <summary>
        /// Retrieves the selected files
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True if files were found successfully</returns>
        private bool GetFiles(string path)
        {
            bool result = true;

            if (!File.Exists(path))
            {
                if (Directory.Exists(path))
                {
                    rootDir = path;
                    tcxFiles = Directory.GetFiles(rootDir, "*.tcx");
                }
                else
                {
                    OutputMessage("!! File or Folder path does not appear to be vaild.", true);
                    result = false;
                }
            }
            else
            {
                rootDir = Path.GetDirectoryName(path);
                tcxFiles = new string[1] { path };
            }

            return result;
        }

        /// <summary>
        /// Writes selected tcx file paths to the output text box
        /// </summary>
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
            hasBrowsed = false;
        }

        /// <summary>
        /// Triggers processing of selected tcx files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            OutputMessage("" + nl + "-----------------------------------------------------------");

            string rootPath = rootDir;
            string destinationPath = rootPath + @"\Fixed_TCX_Files";

            Directory.CreateDirectory(destinationPath);

            OutputMessage(nl + "  Fixing Files... " + nl);
            OutputMessage("-----------------------------------------------------------");

            foreach (string tcxFile in tcxFiles)
            {
                string updatedFilePath = ProcessFile(tcxFile, destinationPath);
                RenameFile(updatedFilePath, destinationPath);
                OutputMessage("-----------------------------------------------------------");
            }

            OutputMessage(nl + "  Finished !! ");
            OutputMessage(nl +"  Output directory: ");
            OutputMessage("  " + destinationPath);
        }

        /// <summary>
        /// Writes message to output text box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reset"></param>
        private void OutputMessage(string message, bool reset = false)
        {
            if(reset)
            {
                outputTextBox.Text = "";
            }
            outputTextBox.AppendText(message);
            outputTextBox.AppendText(nl);
        }

        /// <summary>
        /// Reads file content, removes space at start of file, writes to a new file
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationPath"></param>
        /// <returns>Returns the path to the destination file</returns>
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
            {
                throw new InvalidDataException("Line does not exist in " + sourceFile);
            }

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

        /// <summary>
        /// Renames file to the tcx activity id if it contains one.
        /// </summary>
        /// <param name="tcxFilePath"></param>
        /// <param name="destinationPath"></param>
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

        /// <summary>
        /// Returns Activity Id found in the tcx file if it has one
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="path"></param>
        /// <returns>Returns activity id if one exists otherwise returns null</returns>
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
    }
}
