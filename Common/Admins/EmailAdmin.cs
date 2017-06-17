using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;

namespace Common.Admins
{
    public class EmailAdmin
    {
        public async Task SendEmailAsync(Contact contact, string subject,string body)
        {
            if (contact == null || contact.Emails.Count == 0)
                return;

            var msg = new EmailMessage();
            msg.To.Add(new EmailRecipient(contact.Emails[0].Address));
            msg.Subject = $"Quisiera compartirte la siguiente noticia : {subject}";
            msg.Body = body;
            await EmailManager.ShowComposeNewEmailAsync(msg);
        }
    }
}
