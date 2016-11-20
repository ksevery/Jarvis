namespace Jarvis.Client
{
    using Logic.Core;
    using Logic.Core.Providers.Decisions;
    using Commons.Interaction;

    class Launcher
    {
        static void Main()
        {
            JarvisEngine.Instance(
                new Interactor(), 
                new DecisionTaker())
                .Start();
        }
    }
}
