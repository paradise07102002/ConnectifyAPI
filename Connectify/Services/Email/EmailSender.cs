using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Create a new email message
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Admin", "taolakhoi2@gmail.com"));
        email.To.Add(new MailboxAddress("", to));
        email.Subject = subject;
        email.Body = new TextPart("html") { Text = body };

        using var smtp = new SmtpClient();
        try
        {
            //get EMAIL_ADDRESS and EMAIL_PASSWORD from appsettings

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string email_address = configuration["EmailSettings:EmailAddress"];
            string password_address = configuration["EmailSettings:EmailPassword"];

            // Connect to SMTP server with TLS encryption
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Authenticate using environment variables or secure configuration
            await smtp.AuthenticateAsync(email_address, password_address);

            // Send the email
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}