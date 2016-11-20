using System;
using System.Collections.Generic;
using Jarvis.Commons.Interaction.Interfaces;
using Jarvis.Encryptor;
using Jarvis.RegistryEditor;
using Jarvis.SecureDesktop;

namespace Jarvis.Logic.Core.Providers.Commands
{
    public sealed class CommandProcessor
    {
        private static readonly Lazy<CommandProcessor> Lazy =
            new Lazy<CommandProcessor>(() => new CommandProcessor());

        private CommandProcessor()
        {
        }

        public static CommandProcessor Instance => Lazy.Value;

        public void ProcessCommand(IList<string> commandParts, IInteractor interactor)
        {
            switch (commandParts[0])
            {
                case CommandConstants.SecurePassword:
                    ReceiveSecuredPassword(interactor);
                    break;
                case CommandConstants.StartEncryptor:
                    StartEncryptor(interactor);
                    break;
                case CommandConstants.AddJarvisToStartup:
                    AddJarvisToStartup(interactor);
                    break;
            }
        }

        private void ReceiveSecuredPassword(IInteractor interactor)
        {
            SecureDesktopModule.Instance.Start();
            interactor.SendOutput("Password saved to clipboard.");
        }

        private void StartEncryptor(IInteractor interactor)
        {
            EncryptorModule.Instance.Start(interactor);
        }

        private void AddJarvisToStartup(IInteractor interactor)
        {
            RegistryEditorModule.Instance.AddProcessToStartup("Jarvis.Client.exe");
            interactor.SendOutput("Jarvis added to startup.");
        }
    }
}
