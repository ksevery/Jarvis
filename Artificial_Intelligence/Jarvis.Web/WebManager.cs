using System;
using System.Collections.Generic;
using System.Diagnostics;
using Jarvis.Commons.Interaction.Interfaces;

namespace Jarvis.Web
{
    public sealed class WebManager
    {
        private static readonly Lazy<WebManager> Lazy =
           new Lazy<WebManager>(() => new WebManager());

        private WebManager()
        {
        }

        public static WebManager Instance => Lazy.Value;

        public void OpenSite(IList<string> commandParams, IInteractor interactor)
        {
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
        }

        public void WebSearch(IList<string> commandParams, IInteractor interactor)
        {
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

        public void ConnectToLocalServer(IList<string> commandParams, IInteractor interactor)
        {
            
        }

        public void OpenLocalServer(IList<string> commandParams, IInteractor interactor)
        {
            
        }
    }
}
