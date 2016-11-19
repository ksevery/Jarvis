using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jarvis.Framework.Core.Interfaces;
using Jarvis.Framework.Core.Interfaces.Commands;
using Jarvis.Framework.Core.Interfaces.DecisionTaker;
using Jarvis.Framework.Core.Interfaces.Interactor;
using Jarvis.Framework.Utilities;
using Jarvis.Framework.Modules.Interaction;

namespace Jarvis.Framework.Core
{
    public class JarvisEngine
    {
        private readonly IInteractor _interactor;
        private readonly IDecisionTaker _decisionTaker;
        private readonly ICommandProcessor _commandProcessor;
        //private readonly IDataBase data;

        private JarvisEngine(IInteractor interactor, IDecisionTaker decisionTaker, ICommandProcessor commandProcessor)
        {
            this._interactor = interactor;
            this._decisionTaker = decisionTaker;
            this._commandProcessor = commandProcessor;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(IInteractor interactor, IDecisionTaker decisionTaker, ICommandProcessor commandProcessor)
        {
            if (interactor == null)
            {
                throw new ArgumentNullException($"Interactor module cannot be null.");
            }

            if (decisionTaker == null)
            {
                throw new ArgumentNullException($"Decision Taker module cannot be null.");
            }

            if (commandProcessor == null)
            {
                throw new ArgumentNullException($"Command Processor module cannot be null.");
            }

            return new JarvisEngine(interactor, decisionTaker, commandProcessor);
        }

        public void Start()
        {
            _interactor.SendOutput("Hi, I am Jarvis.");

            string commandLine = _interactor.RecieveInput();

            while (commandLine != "end")
            {
                try
                {
                    var commandParts = _interactor.ParseInput(commandLine);
                    _commandProcessor.ProcessCommand(commandParts, _interactor);
                }
                catch (Exception ex)
                {
                    _interactor.SendOutput(ex.ToString());
                }
                finally
                {
                    commandLine = _interactor.RecieveInput();
                }
            }

            _interactor.SendOutput("See ya ;)");
        }
    }
}
