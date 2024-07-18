namespace BarcloudTask.Service.Interface;

public interface IEmailSender
{
    Task<bool> SendEmailWithCCAsync(List<string> emails, string subject, string htmlMessage, List<string> cC, List<string> bCC, List<string> attachment);
}
