using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Jarvis.RegistryEditor
{
    public sealed class RegistryEditorModule 
    {
        private static readonly Lazy<RegistryEditorModule> Lazy =
            new Lazy<RegistryEditorModule>(() => new RegistryEditorModule());

        private RegistryEditorModule()
        {
        }

        public static RegistryEditorModule Instance => Lazy.Value;

        public void AddProcessToStartup(string processName)
        {

            if (!File.Exists(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) + $"\\{processName}"))
            {
                File.Copy(Convert.ToString(Assembly.GetExecutingAssembly().Location),
                Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                + $"\\{processName}"),
                true);
            }

            //try
            //{
            //    RegistryKey k = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            //    //First argument is the name of the key, second is location of file, third is registryvalue of String
            //    k.SetValue(processName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\{processName}", RegistryValueKind.String);

            //    k.Close();
            //    Console.WriteLine(1);
            //}
            //catch 
            //{

            //}

            RegistryKey r = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            r.SetValue(processName,
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $"\\{processName}",
                RegistryValueKind.String);
            r.Close();
        }
    }
}
