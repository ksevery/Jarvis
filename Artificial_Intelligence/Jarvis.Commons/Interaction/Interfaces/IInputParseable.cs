namespace Jarvis.Commons.Interaction.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IInputParseable
    {
        Tuple<IList<string>, IList<string>> ParseInput(string inputLine);
    }
}
