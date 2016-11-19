namespace Jarvis.SecureDesktop
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    using Interfaces;

    public class SecureDesktopEngine
    {
        private readonly IPasswordReceiver _passwordReceiver;
        private readonly IClipboardProvider _clipboardProvider;

        private SecureDesktopEngine(IPasswordReceiver passwordReceiver, IClipboardProvider clipboardProvider)
        {
            this._passwordReceiver = passwordReceiver;
            this._clipboardProvider = clipboardProvider;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SecureDesktopEngine Instance(IPasswordReceiver passwordReceiver, IClipboardProvider clipboardProvider)
        {
            if (passwordReceiver == null)
            {
                throw new ArgumentNullException($"Password Receiver cannot be null.");
            }

            if (clipboardProvider == null)
            {
                throw new ArgumentNullException($"Clipboard provider cannot be null.");
            }

            return new SecureDesktopEngine(passwordReceiver, clipboardProvider);
        }

        public void Start()
        {
            var password = _passwordReceiver.ReceivePassword();

            _clipboardProvider.SetTextToClipboard(DataFormats.Text, password);
        }
    }
}
