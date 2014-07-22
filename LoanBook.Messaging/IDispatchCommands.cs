namespace LoanBook.Messaging
{
    public interface IDispatchCommands
    {
        void Send<T>(T command) where T : ICommand;
    }
}