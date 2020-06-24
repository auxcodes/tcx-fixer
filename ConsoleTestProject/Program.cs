using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

// TODO: get file ID from tcx if one exists make it the files name

namespace StravaTcxFileFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string rootPath = Directory.GetCurrentDirectory();
                string destinationPath = rootPath + @"\Fixed_TCX_Files";
                Directory.CreateDirectory(destinationPath);
                string[] tcxFiles = Directory.GetFiles(rootPath, "*.tcx");
                Console.WriteLine("|");
                Console.WriteLine("| Found {0} tcx files in {1}", tcxFiles.Length, rootPath);
                Console.WriteLine("|");
                foreach (string tcxFile in tcxFiles)
                {
                    string updatedFilePath = ProcessFile(rootPath, tcxFile, destinationPath);
                    RenameFile(updatedFilePath, destinationPath);
                    Console.WriteLine("> -----------------------------------------------------------");
                }
                Console.WriteLine("|");
                Console.WriteLine("| All files fixed :D");
                Console.WriteLine("|");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static string ProcessFile(string rootPath, string sourceFile, string destinationPath)
        {
            Console.WriteLine("> Processing: " + Path.GetFileName(sourceFile));
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

        static string ActivityId(string xmlFile, string path)
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
                Console.WriteLine("> Activity Id: " + currentNodeValue);
                result = path + @"\" + currentNodeValue.Replace(":", "");
            }
            else
            {
                Console.WriteLine("> Activity Id: NOT FOUND");
                result = null;
            }

            return result;
        }

        static void RenameFile(string tcxFilePath, string destinationPath)
        {
            string activitIdName = ActivityId(tcxFilePath, destinationPath);
            if (activitIdName != null)
            {
                FileInfo fi = new FileInfo(activitIdName + ".tcx");
                if (fi.Exists)
                {
                    string time = DateTime.Now.TimeOfDay.ToString().Replace(":", "");
                    time = "_" + time.Replace(".", "");
                    activitIdName = activitIdName + time + ".tcx";
                }
                else
                {
                    activitIdName = activitIdName + ".tcx";
                }
                File.Move(tcxFilePath, activitIdName);
                Console.WriteLine("> File updated: " + activitIdName);
            }
            else
            {
                Console.WriteLine("> File updated: " + tcxFilePath);
            }
        }
    }
}
