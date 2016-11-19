using System.Runtime.CompilerServices;

namespace Jarvis.Encryptor
{
    using System;
    using System.IO;

    using Commands;

    public sealed class EncryptorEngine
    {
        private static readonly Lazy<EncryptorEngine> Lazy =
            new Lazy<EncryptorEngine>(() => new EncryptorEngine());

        private EncryptorEngine()
        {
        }

        public static EncryptorEngine Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return Lazy.Value; }
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = "Encryptor";

            Console.WriteLine("Enter command:");
            var command = Console.ReadLine();

            while (command != "stop-encryptor")
            {
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        CommandProcessor.Instance.ProcessCommand(command);
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("File not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine(@"Unknown command. Type ""help"" for a list of commands.");
                }

                Console.WriteLine("Enter command:");
                command = Console.ReadLine();
            }
        }
    }
}
