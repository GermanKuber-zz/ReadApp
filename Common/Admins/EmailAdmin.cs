using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;

namespace Common.Admins
{
    public class EmailAdmin
    {
        public async Task SendEmailAsync(string email, string subject,string body)
        {
      

            var msg = new EmailMessage();
            msg.To.Add(new EmailRecipient(email));
            msg.Subject = $"Quisiera compartirte la siguiente noticia : {subject}";
            msg.Body = body;
            await EmailManager.ShowComposeNewEmailAsync(msg);
        }
    }
}
