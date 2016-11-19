using System.Collections.Generic;
using Jarvis.Framework.Core.Interfaces.Commands;
using Jarvis.Framework.Core.Interfaces.Interactor;
using Jarvis.SecureDesktop;
using Jarvis.SecureDesktop.Providers;
using Jarvis.SecureDesktop.Providers.ClipBoardProvider;
using Jarvis.Encryptor;

namespace Jarvis.Framework.Core.Providers.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        public void ProcessCommand(IList<string> commandParts, IInteractor interactor)
        {
            switch (commandParts[0])
            {
                case CommandConstants.SecurePassword:
                    ReceiveSecuredPassword(interactor);
                    break;
                case CommandConstants.StartEncryptor:
                    StartEncryptor();
                    break;
            }
        }

        private void ReceiveSecuredPassword(IInteractor interactor)
        {
            SecureDesktopEngine.Instance(
                new PasswordReceiver(),
                new ClipboardProvider()).Start();

            interactor.SendOutput("Password saved to clipboard.");
        }

        private void StartEncryptor()
        {
            EncryptorEngine.Instance.Start();
        }
    }
}
