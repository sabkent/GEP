namespace LoanBook.Correspondence.Infrastructure.MailServers.Configuration
{
    using System.Configuration;

    public class MailServersConfig : ConfigurationSection
    {
        [ConfigurationProperty(Properties.Name)]
        public string Name
        {
            get
            {
                return base[Properties.Name] as string;
            }
            set
            {
                base[Properties.Name] = value;
            }
        }

        public class Properties
        {
            public const string Name = "name";
        }
    }
}
