using LoanBook.IdentityServer.Authentication;

namespace LoanBook.IdentityServer.Connect.Models
{
    public class LoginInteractionResponse : InteractionResponse
    {
        public SignInMessage SignInMessage { get; set; }

        public bool IsLogin { get { return SignInMessage != null; } }
    }
}
