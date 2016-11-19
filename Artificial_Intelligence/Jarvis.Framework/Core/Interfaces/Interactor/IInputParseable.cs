using System.Collections.Generic;

namespace Jarvis.Framework.Core.Interfaces.Interactor
{
    public interface IInputParseable
    {
        IList<string> ParseInput(string inputLine);
    }
}
