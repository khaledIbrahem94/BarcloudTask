using BarcloudTask.Service.Interface;
using MimeKit;
using NETCore.MailKit;

namespace BarcloudTask.Service.Implementation;

public class EmailSender(IMailKitProvider _mailKitProvider, ICommonService _commonService) : IEmailSender
{
    public async Task<bool> SendEmailWithCCAsync(List<string> emails, string subject, string htmlMessage, List<string> cC, List<string> bCC, List<string> attachment)
    {
        try
        {

            string fullPathHtml = "EmailTemplate/EmailTemplate.html";

            string emailHtmlBody = File.ReadAllText(fullPathHtml).Replace("%MESSAGE_CONTENT%", htmlMessage);
            //Log Email

            MimeMessage mimeMessage = new();

            foreach (var email in emails)
            {
                mimeMessage.To.Add(new MailboxAddress(email, email));
            }


            foreach (var cc in cC ?? [])
            {
                mimeMessage.Cc.Add(new MailboxAddress(cc, cc));
            }

            foreach (var bcc in bCC ?? [])
            {
                mimeMessage.Bcc.Add(new MailboxAddress(bcc, bcc));
            }

            mimeMessage.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = emailHtmlBody };

            //Add Attachment If needed
            mimeMessage.Body = builder.ToMessageBody();
            await SendAsync(mimeMessage);
            return true;
        }
        catch (Exception e)
        {
            await _commonService.AddError(new DataBase.Models.ErrorsLog { Function = "Email Sender", Message = e.Message }).ConfigureAwait(false);
            return false;
        }
    }

    private async Task SendAsync(MimeMessage message)
    {
        try
        {
            message.From.Add(new MailboxAddress(_mailKitProvider.Options.SenderName, _mailKitProvider.Options.SenderEmail));
            using (var emailClient = _mailKitProvider.SmtpClient)
            {
                if (!emailClient.IsConnected)
                {
                    await emailClient.AuthenticateAsync(_mailKitProvider.Options.Account,
                    _mailKitProvider.Options.Password);
                    await emailClient.ConnectAsync(_mailKitProvider.Options.Server,
                    _mailKitProvider.Options.Port, _mailKitProvider.Options.Security);
                }
                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(true);
                //Log Email Sent
            }
        }
        catch (Exception)
        {
            //Log Error
        }
    }

}
