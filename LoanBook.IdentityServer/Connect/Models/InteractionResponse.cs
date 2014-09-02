namespace LoanBook.IdentityServer.Connect.Models
{
    public abstract class InteractionResponse
    {
        public AuthorizeError AuthorizeError { get; set; }
        public bool IsError { get { return AuthorizeError != null; } }
    }
}
