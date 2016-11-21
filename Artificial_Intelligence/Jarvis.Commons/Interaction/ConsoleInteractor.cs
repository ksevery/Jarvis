﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jarvis.Commons.Interaction.Interfaces;
using System.Threading.Tasks;

namespace Jarvis.Commons.Interaction
{
    public class ConsoleInteractor : IInteractor
    {
        public Task<string> RecieveInput()
        {
            return Task.Run(() => Console.ReadLine());
        }

        public Tuple<IList<string>, IList<string>>  ParseInput(string inputLine)
        {
            IList<string> commandSegments = inputLine
                .Split(new [] { ": "}, StringSplitOptions.None)
                .ToList();

            IList<string> commandParts = commandSegments[0]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (commandSegments.Count > 1)
            {
                IList<string> commandParams = commandSegments[1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

                return new Tuple<IList<string>, IList<string>>(commandParts, commandParams);
            }

            return new Tuple<IList<string>, IList<string>>(commandParts, new List<string>());
        }

        public void SendOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
