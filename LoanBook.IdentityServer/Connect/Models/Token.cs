using LoanBook.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace LoanBook.IdentityServer.Connect.Models
{
    public sealed class Token
    {
        public Token(string tokenType)
        {
            Type = tokenType;
            CreationTime = DateTime.UtcNow;
        }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public DateTime CreationTime { get; set; }
        public int Lifetime { get; set; }
        public string Type { get; set; }
        public Client Client { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
