using Jarvis.Framework.Core;
using Jarvis.Framework.Core.Providers.Commands;
using Jarvis.Framework.Core.Providers.DecisionTaking;
using Jarvis.Framework.Modules.Interaction;

namespace Jarvis.Client
{
    class Launcher
    {
        static void Main()
        {
            Engine.Instance(
                new Interactor(),
                new DecisionTaker(),
                new CommandProcessor())
                .Start();
        }
    }
}
