﻿namespace Jarvis.SecureDesktop.Interfaces
{
    public interface IClipboardProvider
    {
        void SetTextToClipboard(string format, object data);
    }
}
