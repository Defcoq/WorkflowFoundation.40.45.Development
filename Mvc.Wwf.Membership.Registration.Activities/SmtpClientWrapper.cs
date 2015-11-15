namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Net.Mail;

    /// <summary>
    /// Wrapper class for SmtpClient
    /// </summary>
    public class SmtpClientWrapper : SmtpClient, ISmtpClient
    {
    }
}