using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Admin", "taolakhoi2@gmail.com"));  // Sửa lại đúng email
        email.To.Add(new MailboxAddress("", to));
        email.Subject = subject;
        email.Body = new TextPart("html") { Text = body };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("taolakhoi2@gmail.com", "eszk tboe thfr zpiw"); // App Password (Viết hoa không dấu)
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi gửi email: {ex.Message}");
        }
    }
}