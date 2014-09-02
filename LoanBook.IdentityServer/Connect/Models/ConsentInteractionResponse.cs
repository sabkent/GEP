namespace LoanBook.IdentityServer.Connect.Models
{
    public class ConsentInteractionResponse : InteractionResponse
    {
        public bool IsConsent { get; set; }
        public string ConsentError { get; set; }
    }
}
