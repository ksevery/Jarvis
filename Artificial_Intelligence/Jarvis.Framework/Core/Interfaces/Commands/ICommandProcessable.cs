using System.Collections.Generic;
using Jarvis.Framework.Core.Interfaces.Interactor;
using Jarvis.Framework.Core.Providers.Commands;

namespace Jarvis.Framework.Core.Interfaces.Commands
{
    public interface ICommandProcessable
    {
        void ProcessCommand(IList<string> commandParts, IInteractor interactor);
    }
}
