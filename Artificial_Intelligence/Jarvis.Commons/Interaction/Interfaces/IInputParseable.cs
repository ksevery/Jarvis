namespace Jarvis.Commons.Interaction.Interfaces
{
    using System.Collections.Generic;

    public interface IInputParseable
    {
        IList<string> ParseInput(string inputLine);
    }
}
