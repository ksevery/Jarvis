using System;
using System.Collections.Generic;
using System.Linq;
using Jarvis.Framework.Core.Interfaces.Interactor;

namespace Jarvis.Framework.Modules.Interaction
{
    public class Interactor : IInteractor
    {
        public string RecieveInput()
        {
            return Console.ReadLine();
        }

        public IList<string> ParseInput(string inputLine)
        {
            IList<string> inputParams = inputLine
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            return inputParams;
        }

        public void SendOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
