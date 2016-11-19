namespace SecureDesktop.Interfaces
{
    public interface IWriterProvider
    {
        void Write(string message);

        void WriteLine(string message);
    }
}
