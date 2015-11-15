namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Net;
    using System.Net.Mail;
    using System.Security.Cryptography.X509Certificates;

    public interface ISmtpClient
    {
        // Summary:
        //     Specify which certificates should be used to establish the Secure Sockets
        //     Layer (SSL) connection.
        //
        // Returns:
        //     An System.Security.Cryptography.X509Certificates.X509CertificateCollection,
        //     holding one or more client certificates. The default value is derived from
        //     the mail configuration attributes in a configuration file.
        X509CertificateCollection ClientCertificates { get; }

        //
        // Summary:
        //     Gets or sets the credentials used to authenticate the sender.
        //
        // Returns:
        //     An System.Net.ICredentialsByHost that represents the credentials to use for
        //     authentication; or null if no credentials have been specified.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     You cannot change the value of this property when an email is being sent.
        ICredentialsByHost Credentials { get; set; }

        //
        // Summary:
        //     Specifies how outgoing email messages will be handled.
        //
        // Returns:
        //     An System.Net.Mail.SmtpDeliveryMethod that indicates how email messages are
        //     delivered.
        SmtpDeliveryMethod DeliveryMethod { get; set; }

        //
        // Summary:
        //     Specify whether the System.Net.Mail.SmtpClient uses Secure Sockets Layer
        //     (SSL) to encrypt the connection.
        //
        // Returns:
        //     true if the System.Net.Mail.SmtpClient uses SSL; otherwise, false. The default
        //     is false.
        bool EnableSsl { get; set; }

        //
        // Summary:
        //     Gets or sets the name or IP address of the host used for SMTP transactions.
        //
        // Returns:
        //     A System.String that contains the name or IP address of the computer to use
        //     for SMTP transactions.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The value specified for a set operation is null.
        //
        //   System.ArgumentException:
        //     The value specified for a set operation is equal to System.String.Empty ("").
        //
        //   System.InvalidOperationException:
        //     You cannot change the value of this property when an email is being sent.
        string Host { get; set; }

        //
        // Summary:
        //     Gets or sets the folder where applications save mail messages to be processed
        //     by the local SMTP server.
        //
        // Returns:
        //     A System.String that specifies the pickup directory for mail messages.
        string PickupDirectoryLocation { get; set; }

        //
        // Summary:
        //     Gets or sets the port used for SMTP transactions.
        //
        // Returns:
        //     An System.Int32 that contains the port number on the SMTP host. The default
        //     value is 25.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The value specified for a set operation is less than or equal to zero.
        //
        //   System.InvalidOperationException:
        //     You cannot change the value of this property when an email is being sent.
        int Port { get; set; }

        //
        // Summary:
        //     Gets the network connection used to transmit the e-mail message.
        //
        // Returns:
        //     A System.Net.ServicePoint that connects to the System.Net.Mail.SmtpClient.Host
        //     property used for SMTP.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     System.Net.Mail.SmtpClient.Host is null or the empty string ("").-or-System.Net.Mail.SmtpClient.Port
        //     is zero.
        ServicePoint ServicePoint { get; }

        //
        // Summary:
        //     Gets or sets the Service Provider Name (SPN) to use for authentication when
        //     using extended protection.
        //
        // Returns:
        //     A System.String that specifies the SPN to use for extended protection. The
        //     default value for this SPN is of the form "SMTPSVC/<host>" where <host> is
        //     the hostname of the SMTP mail server.
        string TargetName { get; set; }

        //
        // Summary:
        //     Gets or sets a value that specifies the amount of time after which a synchronous
        //     Overload:System.Net.Mail.SmtpClient.Send call times out.
        //
        // Returns:
        //     An System.Int32 that specifies the time-out value in milliseconds. The default
        //     value is 100,000 (100 seconds).
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     The value specified for a set operation was less than zero.
        //
        //   System.InvalidOperationException:
        //     You cannot change the value of this property when an email is being sent.
        int Timeout { get; set; }

        //
        // Summary:
        //     Gets or sets a System.Boolean value that controls whether the System.Net.CredentialCache.DefaultCredentials
        //     are sent with requests.
        //
        // Returns:
        //     true if the default credentials are used; otherwise false. The default value
        //     is false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     You cannot change the value of this property when an e-mail is being sent.
        bool UseDefaultCredentials { get; set; }

        // Summary:
        //     Occurs when an asynchronous e-mail send operation completes.
        event SendCompletedEventHandler SendCompleted;

        //
        // Summary:
        //     Sends the specified message to an SMTP server for delivery.
        //
        // Parameters:
        //   message:
        //     A System.Net.Mail.MailMessage that contains the message to send.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     message is null.
        //
        //   System.InvalidOperationException:
        //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
        //     call in progress.-or- System.Net.Mail.MailMessage.From is null.-or- There
        //     are no recipients specified in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC,
        //     and System.Net.Mail.MailMessage.Bcc properties.-or- System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is null.-or-System.Net.Mail.SmtpClient.DeliveryMethod property is set to
        //     System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is equal to the empty string ("").-or- System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
        //     is zero, a negative number, or greater than 65,535.
        //
        //   System.ObjectDisposedException:
        //     This object has been disposed.
        //
        //   System.Net.Mail.SmtpException:
        //     The connection to the SMTP server failed.-or-Authentication failed.-or-The
        //     operation timed out.-or-System.Net.Mail.SmtpClient.EnableSsl is set to true
        //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
        //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis.-or-System.Net.Mail.SmtpClient.EnableSsl
        //     is set to true, but the SMTP mail server did not advertise STARTTLS in the
        //     response to the EHLO command.
        //
        //   System.Net.Mail.SmtpFailedRecipientsException:
        //     The message could not be delivered to one or more of the recipients in System.Net.Mail.MailMessage.To,
        //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
        void Send(MailMessage message);

        //
        // Summary:
        //     Sends the specified e-mail message to an SMTP server for delivery. The message
        //     sender, recipients, subject, and message body are specified using System.String
        //     objects.
        //
        // Parameters:
        //   from:
        //     A System.String that contains the address information of the message sender.
        //
        //   recipients:
        //     A System.String that contains the addresses that the message is sent to.
        //
        //   subject:
        //     A System.String that contains the subject line for the message.
        //
        //   body:
        //     A System.String that contains the message body.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     from is null.-or-recipients is null.
        //
        //   System.ArgumentException:
        //     from is System.String.Empty.-or-recipients is System.String.Empty.
        //
        //   System.InvalidOperationException:
        //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
        //     call in progress.-or-System.Net.Mail.SmtpClient.DeliveryMethod property is
        //     set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is null.-or-System.Net.Mail.SmtpClient.DeliveryMethod property is set to
        //     System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is equal to the empty string ("").-or- System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
        //     is zero, a negative number, or greater than 65,535.
        //
        //   System.ObjectDisposedException:
        //     This object has been disposed.
        //
        //   System.Net.Mail.SmtpException:
        //     The connection to the SMTP server failed.-or-Authentication failed.-or-The
        //     operation timed out.-or- System.Net.Mail.SmtpClient.EnableSsl is set to true
        //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
        //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis.-or-System.Net.Mail.SmtpClient.EnableSsl
        //     is set to true, but the SMTP mail server did not advertise STARTTLS in the
        //     response to the EHLO command.
        //
        //   System.Net.Mail.SmtpFailedRecipientsException:
        //     The message could not be delivered to one or more of the recipients in recipients.
        void Send(string from, string recipients, string subject, string body);

        //
        // Summary:
        //     Sends the specified e-mail message to an SMTP server for delivery. This method
        //     does not block the calling thread and allows the caller to pass an object
        //     to the method that is invoked when the operation completes.
        //
        // Parameters:
        //   message:
        //     A System.Net.Mail.MailMessage that contains the message to send.
        //
        //   userToken:
        //     A user-defined object that is passed to the method invoked when the asynchronous
        //     operation completes.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     message is null.-or-System.Net.Mail.MailMessage.From is null.
        //
        //   System.InvalidOperationException:
        //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
        //     call in progress.-or- There are no recipients specified in System.Net.Mail.MailMessage.To,
        //     System.Net.Mail.MailMessage.CC, and System.Net.Mail.MailMessage.Bcc properties.-or-
        //     System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network
        //     and System.Net.Mail.SmtpClient.Host is null.-or-System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is equal to the empty string ("").-or- System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
        //     is zero, a negative number, or greater than 65,535.
        //
        //   System.ObjectDisposedException:
        //     This object has been disposed.
        //
        //   System.Net.Mail.SmtpException:
        //     The connection to the SMTP server failed.-or-Authentication failed.-or-The
        //     operation timed out.-or- System.Net.Mail.SmtpClient.EnableSsl is set to true
        //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
        //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis.-or-System.Net.Mail.SmtpClient.EnableSsl
        //     is set to true, but the SMTP mail server did not advertise STARTTLS in the
        //     response to the EHLO command.
        //
        //   System.Net.Mail.SmtpFailedRecipientsException:
        //     The message could not be delivered to one or more of the recipients in System.Net.Mail.MailMessage.To,
        //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
        void SendAsync(MailMessage message, object userToken);

        //
        // Summary:
        //     Sends an e-mail message to an SMTP server for delivery. The message sender,
        //     recipients, subject, and message body are specified using System.String objects.
        //     This method does not block the calling thread and allows the caller to pass
        //     an object to the method that is invoked when the operation completes.
        //
        // Parameters:
        //   from:
        //     A System.String that contains the address information of the message sender.
        //
        //   recipients:
        //     A System.String that contains the address that the message is sent to.
        //
        //   subject:
        //     A System.String that contains the subject line for the message.
        //
        //   body:
        //     A System.String that contains the message body.
        //
        //   userToken:
        //     A user-defined object that is passed to the method invoked when the asynchronous
        //     operation completes.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     from is null.-or-recipient is null.
        //
        //   System.ArgumentException:
        //     from is System.String.Empty.-or-recipient is System.String.Empty.
        //
        //   System.InvalidOperationException:
        //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
        //     call in progress.-or- System.Net.Mail.SmtpClient.DeliveryMethod property
        //     is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is null.-or-System.Net.Mail.SmtpClient.DeliveryMethod property is set to
        //     System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
        //     is equal to the empty string ("").-or- System.Net.Mail.SmtpClient.DeliveryMethod
        //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
        //     is zero, a negative number, or greater than 65,535.
        //
        //   System.ObjectDisposedException:
        //     This object has been disposed.
        //
        //   System.Net.Mail.SmtpException:
        //     The connection to the SMTP server failed.-or-Authentication failed.-or-The
        //     operation timed out.-or- System.Net.Mail.SmtpClient.EnableSsl is set to true
        //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
        //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis.-or-System.Net.Mail.SmtpClient.EnableSsl
        //     is set to true, but the SMTP mail server did not advertise STARTTLS in the
        //     response to the EHLO command.
        //
        //   System.Net.Mail.SmtpFailedRecipientsException:
        //     The message could not be delivered to one or more of the recipients in recipients.
        void SendAsync(string from, string recipients, string subject, string body, object userToken);

        //
        // Summary:
        //     Cancels an asynchronous operation to send an e-mail message.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     This object has been disposed.
        void SendAsyncCancel();
    }
}