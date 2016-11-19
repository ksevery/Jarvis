namespace Jarvis.Encryptor.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CommandProcessor
    {
        private static CommandProcessor _instance;

        public static CommandProcessor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommandProcessor();
                }

                return _instance;
            }
        }

        public void ProcessCommand(string command)
        {
            switch (command)
            {
                case Constants.Help:
                    this.DisplayCommands();
                    break;
                case Constants.EncryptString:
                    this.EncryptString();
                    break;
                case Constants.DecryptStryng:
                    this.DecryptString();
                    break;
                case Constants.EncryptTxtFile:
                    this.EncryptTxtFile();
                    break;
                case Constants.DecryptTxtFile:
                    this.DecryptTxtFile();
                    break;
                case Constants.ClearConsole:
                    this.ClearConsole();
                    break;
                default:
                    Console.WriteLine(@"Unknown command. Type ""help"" for a list of commands.");
                    break;
            }
        }

        private void EncryptString()
        {
            Console.WriteLine("Enter a password to use:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter a string to encrypt:");
            string text = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Your encrypted string is:");
            string encryptedstring = Cipher.Encrypt(text, password);
            Console.WriteLine(encryptedstring);
            Console.WriteLine("");
        }

        private void DecryptString()
        {
            Console.WriteLine("Enter a password to use:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter a string to decrypt:");
            string text = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Your decrypted string is:");
            string decryptedstring = Cipher.Decrypt(text, password);
            Console.WriteLine(decryptedstring);
            Console.WriteLine("");
        }

        private void EncryptTxtFile()
        {
            Console.WriteLine("Enter file path:");
            string path = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            List<List<string>> text = new List<List<string>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    if (line == "")
                    {
                        text.Add(new List<string> { "" });
                    }
                    else
                    {
                        text.Add(new List<string>(line.Split(' ')).Select(
                            t => Cipher.Encrypt(t, password)).ToList());
                    }

                    line = reader.ReadLine();
                }
            }

            CheckDirectory("EncryptedFiles");
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string newFilePath = Path.Combine(dirName, "EncryptedFiles");
            string newFileName = "";

            if (path != null && path.Contains(@"\"))
            {
                newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            }
            else
            {
                newFileName = path;
            }

            if (!CheckFile(newFilePath, newFileName))
            {
                using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
                {
                    for (int i = 0; i < text.Count; i++)
                    {
                        file.WriteLine(string.Join(" ", text[i]));
                    }

                    Console.WriteLine($"File {newFileName} encrypted in folder EncryptedFiles.");
                }
            }
            else
            {
                Console.WriteLine("File alredy exists.");
            }

            //Console.WriteLine("Enter file path:");
            //string path = Console.ReadLine();
            //Console.WriteLine("Enter password:");
            //string password = Console.ReadLine();

            //List<string> text = new List<string>();

            //using (StreamReader reader = new StreamReader(path))
            //{
            //    string line = reader.ReadLine();

            //    while (line != null)
            //    {
            //        text.Add(StringCipher.Encrypt(line, password));

            //        line = reader.ReadLine();
            //    }
            //}

            //CheckDirectory("EncryptedFiles");
            //DirectoryInfo dir = new DirectoryInfo(".");
            //String dirName = dir.FullName;
            //string newFilePath = Path.Combine(dirName, "EncryptedFiles");
            //string newFileName = "";

            //if (path != null && path.Contains(@"\"))
            //{
            //    newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            //}
            //else
            //{
            //    newFileName = path;
            //}

            //if (!CheckFile(newFilePath, newFileName))
            //{
            //    using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
            //    {
            //        for (int i = 0; i < text.Count; i++)
            //        {
            //            file.WriteLine(string.Join(" ", text[i]));
            //        }

            //        Console.WriteLine($"File {newFileName} encrypted in folder EncryptedFiles.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("File alredy exists.");
            //}
        }

        private void DecryptTxtFile()
        {
            Console.WriteLine("Enter file path:");
            string path = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            List<List<string>> text = new List<List<string>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    if (line == "")
                    {
                        text.Add(new List<string> { "" });
                    }
                    else
                    {
                        text.Add(new List<string>(line.Split(' ')).Select(
                        t => Cipher.Decrypt(t, password)).ToList());
                    }

                    line = reader.ReadLine();
                }
            }

            CheckDirectory("DecryptedFiles");
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string newFilePath = Path.Combine(dirName, "DecryptedFiles");
            string newFileName = "";

            if (path != null && path.Contains(@"\"))
            {
                newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            }
            else
            {
                newFileName = path;
            }

            if (!CheckFile(newFilePath, newFileName))
            {
                using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
                {
                    for (int i = 0; i < text.Count; i++)
                    {
                        file.WriteLine(string.Join(" ", text[i]));
                    }

                    Console.WriteLine($"File {newFileName} decrypted in folder DecryptedFiles.");
                }
            }
            else
            {
                Console.WriteLine("File alredy exists.");
            }
            //Console.WriteLine("Enter file path:");
            //string path = Console.ReadLine();
            //Console.WriteLine("Enter password:");
            //string password = Console.ReadLine();

            //List<string> text = new List<string>();

            //using (StreamReader reader = new StreamReader(path))
            //{
            //    string line = reader.ReadLine();

            //    while (line != null)
            //    {
            //        text.Add(StringCipher.Decrypt(line, password));

            //        line = reader.ReadLine();
            //    }
            //}

            //CheckDirectory("DecryptedFiles");
            //DirectoryInfo dir = new DirectoryInfo(".");
            //String dirName = dir.FullName;
            //string newFilePath = Path.Combine(dirName, "DecryptedFiles");
            //string newFileName = "";

            //if (path != null && path.Contains(@"\"))
            //{
            //    newFileName = path.Substring(path.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            //}
            //else
            //{
            //    newFileName = path;
            //}

            //if (!CheckFile(newFilePath, newFileName))
            //{
            //    using (StreamWriter file = File.CreateText(Path.Combine(newFilePath, newFileName)))
            //    {
            //        for (int i = 0; i < text.Count; i++)
            //        {
            //            file.WriteLine(string.Join(" ", text[i]));
            //        }

            //        Console.WriteLine($"File {newFileName} decrypted in folder DecryptedFiles.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("File alredy exists.");
            //}
        }

        private void DisplayCommands()
        {
            StringBuilder commandsDescription = new StringBuilder();

            commandsDescription.AppendLine("------------------------------------------" + Environment.NewLine +
                                           "COMMANDS: " + Environment.NewLine +
                                           "encrypt string - Encrypts input string" + Environment.NewLine +
                                           "decrypt string - Decrypts input string" + Environment.NewLine +
                                           "encrypt txt file - Encrypts whole txt file" + Environment.NewLine +
                                           "decrypt txt file - Decrypts whole txt file" + Environment.NewLine +
                                           "clear - Clears console" + Environment.NewLine +
                                           "stop-encryptor" + Environment.NewLine +
                                           "------------------------------------------");

            Console.Write(commandsDescription);
        }

        private void ClearConsole()
        {
            Console.Clear();
        }

        private void CheckDirectory(string name)
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            String dirName = dir.FullName;
            string pathString = Path.Combine(dirName, name);

            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }
        }

        private bool CheckFile(string directory, string file)
        {
            string[] filePaths = Directory.GetFiles(directory, "*.txt");
            string[] fileNames = filePaths.Select(x => x.Substring(x.LastIndexOf(@"\", StringComparison.Ordinal) + 1)).ToArray();

            return fileNames.Contains(file);
        }
    }
}
