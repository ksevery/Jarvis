using System.Runtime.CompilerServices;
using Jarvis.Commons.Interaction.Interfaces;

namespace Jarvis.Encryptor
{
    using System;
    using System.IO;

    using Commands;

    public sealed class EncryptorModule
    {
        private static  Lazy<EncryptorModule> Lazy =
            new Lazy<EncryptorModule>(() => new EncryptorModule());

        private EncryptorModule()
        {
        }

        public static EncryptorModule Instance
        {
            get
            {
                return Lazy.Value;
            }
        } 

        public void Start(IInteractor interactor)
        {
            interactor.SendOutput("Encryptor started. Enter command:");
            var command = interactor.RecieveInput();

            while (command != "stop encryptor")
            {
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        CommandProcessor.Instance(interactor).ProcessCommand(command);
                    }
                    catch (FileNotFoundException)
                    {
                        interactor.SendOutput("File not found.");
                    }
                    catch (Exception ex)
                    {
                        interactor.SendOutput(ex.ToString());
                    }
                }
                else
                {
                    interactor.SendOutput(@"Unknown command. Type ""help"" for a list of commands.");
                }

                interactor.SendOutput("Enter command:");
                command = interactor.RecieveInput();
            }

            interactor.SendOutput("Encryptor stoped.");
        }
    }
}
