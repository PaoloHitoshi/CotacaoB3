namespace CotacaoB3
{
    public class Config
    {
        public class EmailCredentials
        {
            public string EmailUser { get; set; }
            public string EmailPassword { get; set; }
        }

        public EmailCredentials emailCredentials { get; set; }
        public string TokenAPI { get; set; }
        public string PasswordAPPGmail { get; set; }
        public Config(string emailUser, string emailPassword, string tokenAPI, string passwordAPPGmail)
        {
            emailCredentials = new EmailCredentials();
            emailCredentials.EmailUser = emailUser;
            emailCredentials.EmailPassword = emailPassword;
            TokenAPI = tokenAPI;
            PasswordAPPGmail = passwordAPPGmail;
        }
    }
}
