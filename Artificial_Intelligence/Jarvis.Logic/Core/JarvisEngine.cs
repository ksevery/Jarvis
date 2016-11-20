using System;
using System.Runtime.CompilerServices;
using Jarvis.Commons.Interaction.Interfaces;
using Jarvis.Logic.Core.CommandControl;
using Jarvis.Logic.Core.Interfaces.Decisions;
using Jarvis.Logic.Core.Providers.Commands;

namespace Jarvis.Logic.Core
{
    public class JarvisEngine
    {
        private readonly IInteractor _interactor;
        private readonly IDecisionTaker _decisionTaker;
        //private readonly IDataBase data;

        private JarvisEngine(IInteractor interactor, IDecisionTaker decisionTaker)
        {
            this._interactor = interactor;
            this._decisionTaker = decisionTaker;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static JarvisEngine Instance(IInteractor interactor, IDecisionTaker decisionTaker)
        {
            if (interactor == null)
            {
                throw new ArgumentNullException($"Interactor module cannot be null.");
            }

            if (decisionTaker == null)
            {
                throw new ArgumentNullException($"Decision Taker module cannot be null.");
            }

            return new JarvisEngine(interactor, decisionTaker);
        }

        public void Start()
        {
            _interactor.SendOutput("Hi, I am Jarvis.");

            string commandLine = _interactor.RecieveInput();

            while (commandLine != "bye")
            {
                try
                {
                    var commandSegments = _interactor.ParseInput(commandLine);
                    CommandProcessor.Instance.ProcessCommand(commandSegments.Item1, commandSegments.Item2, _interactor);
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
