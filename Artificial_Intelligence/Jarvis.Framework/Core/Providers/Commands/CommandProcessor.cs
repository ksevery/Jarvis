using System;
using System.Collections.Generic;
using Jarvis.Framework.Core.Interfaces.Commands;
using Jarvis.Framework.Core.Interfaces.Interactor;
using SecureDesktop;
using SecureDesktop.Interfaces;
using SecureDesktop.Providers;
using SecureDesktop.Providers.ClipBoardProvider;

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
            }
        }

        private void ReceiveSecuredPassword(IInteractor interactor)
        {
            SecureDesktopEngine.Instance(
                //new PasswordReceiver(),
                null,
                new ClipboardProvider()).Start();

            interactor.SendOutput("Password saved to clipboard.");
        }
    }
}
