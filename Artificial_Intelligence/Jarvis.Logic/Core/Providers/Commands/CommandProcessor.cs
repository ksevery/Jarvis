using System;
using System.Collections.Generic;
using System.Globalization;
using Jarvis.Commons.Interaction.Interfaces;
using Jarvis.Encryptor;
using Jarvis.Logic.Utilities;
using Jarvis.RegistryEditor;
using Jarvis.SecureDesktop;

namespace Jarvis.Logic.Core.Providers.Commands
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private const string NotFoundCommandMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        private CommandProcessor()
        {
        }

        public static CommandProcessor Instance => Lazy.Value;

        public void ProcessCommand(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            switch (commandParts[0])
            {
                case CommandConstants.AddJarvisToStartup:
                    AddJarvisToStartup(interactor);
                    break;
                case CommandConstants.Tell:
                    TellMe(commandParts, commandParams, interactor);
                    break;
                case CommandConstants.StartModule:
                    StartModule(commandParts, commandParams, interactor);
                    break;
                default:
                    interactor.SendOutput(NotFoundCommandMsg);
                    break;
            }
        }

        public void StartModule(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParts.Count, 2, NotFoundCommandMsg);
            switch (commandParts[1])
            {
                case ModuleName.SecureDesktop:
                    SecureDesktopModule.Instance.Start();
                    interactor.SendOutput("Password saved to clipboard.");
                    break;
                case ModuleName.Encryptor:
                    EncryptorModule.Instance.Start(interactor);
                    break;
                default:
                    interactor.SendOutput(NotFoundCommandMsg);
                    break;
            }
        }

        private void TellMe(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParts.Count, 3, NotFoundCommandMsg);
            Verification(commandParams.Count, 2, InvalidParametersMsg);
            switch (commandParts[1])
            {
                case "random":
                    switch (commandParts[2])
                    {
                        case "number":
                            interactor.SendOutput(
                                Utility.Instance.RandomNumber(
                                int.Parse(commandParams[0]),
                                int.Parse(commandParams[1]))
                                .ToString());
                            break;
                        case "string":
                            interactor.SendOutput(
                            Utility.Instance.RandomString(
                                int.Parse(commandParams[0]),
                                int.Parse(commandParams[1])));
                            break;
                        case "date":
                            interactor.SendOutput(
                            Utility.Instance.RandomDateTime(
                                DateTime.Parse(commandParams[0]),
                                DateTime.Parse(commandParams[1]))
                                .ToString(CultureInfo.CurrentCulture));
                            break;
                        default:
                            interactor.SendOutput(NotFoundCommandMsg);
                            break;
                    }
                    break;
                case "joke":
                    break;
                default:
                    interactor.SendOutput(NotFoundCommandMsg);
                    break;
            }
        }

        private void AddJarvisToStartup(IInteractor interactor)
        {
            RegistryEditorModule.Instance.AddProcessToStartup("Jarvis.Client.exe");
            interactor.SendOutput("Jarvis added to startup.");
        }

        private void Verification(int lenght, int expected, string errorMessage)
        {
            if (lenght != expected)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
