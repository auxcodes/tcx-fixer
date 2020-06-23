using System;
using System.IO;
using System.Runtime.CompilerServices;

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
                    ProcessFile(rootPath, tcxFile, destinationPath);
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

        static void ProcessFile(string rootPath, string sourceFile, string destinationPath)
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
            Console.WriteLine("> File updated: " + destinationFile);
            Console.WriteLine(">");
        }


    }
}
