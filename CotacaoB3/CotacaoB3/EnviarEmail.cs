using System.Net.Mail;
using System.Net;

namespace CotacaoB3
{
    public class EnviarEmail
    {
        public void EnviarMensagem(string mensagem, string emailPara, string emailDe, string senhaEmail, string senhaAPP)
        {
            MailAddress to = new MailAddress(emailPara);
            MailAddress from = new MailAddress(emailDe);

            MailMessage email = new MailMessage(from, to);
            email.Subject = "Email da Inoa!";
            email.Body = mensagem;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(emailDe, senhaAPP),
                EnableSsl = true
            };

            try
            {
                smtp.Send(email);
                Console.WriteLine("Email enviado com sucesso...");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
            }
        }
    }
}
