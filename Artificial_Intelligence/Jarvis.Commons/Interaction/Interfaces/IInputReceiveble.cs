using System.Threading.Tasks;

namespace Jarvis.Commons.Interaction.Interfaces
{
    public interface IInputReceiveble
    {
        Task<string> RecieveInput();
    }
}
