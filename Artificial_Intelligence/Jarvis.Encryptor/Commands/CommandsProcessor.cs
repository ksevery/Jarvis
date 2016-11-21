using Jarvis.Commons.Interaction.Interfaces;

namespace Jarvis.Encryptor.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CommandProcessor
    {
        private readonly IInteractor _interactor;

        private CommandProcessor(IInteractor interactor)
        {
            this._interactor = interactor;
        }

        public static CommandProcessor Instance(IInteractor interactor)
        {
            return new CommandProcessor(interactor);
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
                    _interactor.SendOutput(@"Unknown command. Type ""help"" for a list of commands.");
                    break;
            }
        }

        private async void EncryptString()
        {
            _interactor.SendOutput("Enter a password to use:");
            string password = await _interactor.RecieveInput();
            _interactor.SendOutput("Enter a string to encrypt:");
            string text = await _interactor.RecieveInput();
            _interactor.SendOutput(Environment.NewLine);

            _interactor.SendOutput("Your encrypted string is:");
            string encryptedstring = Cipher.Encrypt(text, password);
            _interactor.SendOutput(encryptedstring);
            _interactor.SendOutput("");
        }

        private async void DecryptString()
        {
            _interactor.SendOutput("Enter a password to use:");
            string password = await _interactor.RecieveInput();
            _interactor.SendOutput("Enter a string to decrypt:");
            string text = await _interactor.RecieveInput();
            _interactor.SendOutput("");

            _interactor.SendOutput("Your decrypted string is:");
            string decryptedstring = Cipher.Decrypt(text, password);
            _interactor.SendOutput(decryptedstring);
            _interactor.SendOutput("");
        }

        private async void EncryptTxtFile()
        {
            _interactor.SendOutput("Enter file path:");
            string path = await _interactor.RecieveInput();
            _interactor.SendOutput("Enter password:");
            string password = await _interactor.RecieveInput();

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

                    _interactor.SendOutput($"File {newFileName} encrypted in folder EncryptedFiles.");
                }
            }
            else
            {
                _interactor.SendOutput("File alredy exists.");
            }

            //_interactor.SendOutput("Enter file path:");
            //string path = _interactor.RecieveInput();
            //_interactor.SendOutput("Enter password:");
            //string password = _interactor.RecieveInput();

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

            //        _interactor.SendOutput($"File {newFileName} encrypted in folder EncryptedFiles.");
            //    }
            //}
            //else
            //{
            //    _interactor.SendOutput("File alredy exists.");
            //}
        }

        private async void DecryptTxtFile()
        {
            _interactor.SendOutput("Enter file path:");
            string path = await _interactor.RecieveInput();
            _interactor.SendOutput("Enter password:");
            string password = await _interactor.RecieveInput();

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

                    _interactor.SendOutput($"File {newFileName} decrypted in folder DecryptedFiles.");
                }
            }
            else
            {
                _interactor.SendOutput("File alredy exists.");
            }
            //_interactor.SendOutput("Enter file path:");
            //string path = _interactor.RecieveInput();
            //_interactor.SendOutput("Enter password:");
            //string password = _interactor.RecieveInput();

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

            //        _interactor.SendOutput($"File {newFileName} decrypted in folder DecryptedFiles.");
            //    }
            //}
            //else
            //{
            //    _interactor.SendOutput("File alredy exists.");
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
            _interactor.SendOutput(commandsDescription.ToString());
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
