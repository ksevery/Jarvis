using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Jarvis.Commons.Interaction.Interfaces;
using Jarvis.Data;
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

        private const string CommandNotFoundMsg = "Command not found.";
        private const string InvalidParametersMsg = "Invalid Parameters.";

        private CommandProcessor()
        {
        }

        public static CommandProcessor Instance => Lazy.Value;

        public void ProcessCommand(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            switch (commandParts[0])
            {
                case CommandConstants.AddToStartup:
                    AddToStartup(commandParams, interactor);
                    break;
                case CommandConstants.Tell:
                    TellMe(commandParts, commandParams, interactor);
                    break;
                case CommandConstants.StartModule:
                    StartModule(commandParts, commandParams, interactor);
                    break;
                case CommandConstants.Open:
                    Open(commandParts, commandParams, interactor);
                    break;
                case CommandConstants.WebSearch:
                    WebSearch(commandParams, interactor);
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void WebSearch(IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParams.Count, 1, InvalidParametersMsg);
            string qwery = string.Join("+", commandParams);
            string site = @"http://" + @"www.google.com/#hl=en&q=" + qwery;

            Process browser = new Process
            {
                StartInfo =
                {
                    FileName = "firefox.exe",
                    Arguments = site.Trim('\0'),
                    WindowStyle = ProcessWindowStyle.Maximized
                }
            };
            browser.Start();
            interactor.SendOutput($@"Seraching in web for ""{string.Join(" ", commandParams)}""");
        }

        public void Open(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "site":
                    Verification(commandParams.Count, 1, InvalidParametersMsg);
                    string site = commandParams[0];

                    Process browser = new Process
                    {
                        StartInfo =
                        {
                            FileName = "firefox.exe",
                            Arguments = site.Trim('\0'),
                            WindowStyle = ProcessWindowStyle.Maximized
                        }
                    };
                    browser.Start();
                    interactor.SendOutput($"{site} opened with Firefox.");
                    break;
                    
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        public void StartModule(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParts.Count, 2, CommandNotFoundMsg);
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
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void TellMe(IList<string> commandParts, IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParts.Count, 2, CommandNotFoundMsg);
            switch (commandParts[1])
            {
                case "random":
                    Verification(commandParts.Count, 3, CommandNotFoundMsg);
                    Verification(commandParams.Count, 2, InvalidParametersMsg);
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
                            interactor.SendOutput(CommandNotFoundMsg);
                            break;
                    }
                    break;
                case "joke":
                    interactor.SendOutput(
                        MockedDb.Instance.Jokes[
                            Utility.Instance.RandomNumber(
                                0, MockedDb.Instance.Jokes.Count - 1)]);
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }

        private void AddToStartup(IList<string> commandParams, IInteractor interactor)
        {
            Verification(commandParams.Count, 1, InvalidParametersMsg);
            switch (commandParams[0])
            {
                case "jarvis":
                    RegistryEditorModule.Instance.AddProcessToStartup("Jarvis.Client.exe");
                    interactor.SendOutput("Jarvis added to startup.");
                    break;
                default:
                    interactor.SendOutput(CommandNotFoundMsg);
                    break;
            }
        }
        
        private void Verification(int lenght, int expected, string errorMessage)
        {
            if (lenght < expected)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
