// created manually! to use this file, make a secrets.json in the base folder of this project and specify the value of 3 keys Email, Password & Host
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace HireStreamDotNetProject.Utils {
    public class EmailConfig
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
}

public class EmailService
{
    private readonly EmailConfig _emailConfig;

    public EmailService()
    {
        // Load credentials from JSON file
        string json = File.ReadAllText("secrets.json");
        _emailConfig = JsonSerializer.Deserialize<EmailConfig>(json);
        System.Console.WriteLine($"email: {_emailConfig.Email} | password: {_emailConfig.Password} | host: {_emailConfig.Host}");
        Console.WriteLine($"Email: {_emailConfig.Email} | Password: {_emailConfig.Password} | {_emailConfig.Host}");
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Hire Stream DotNet", _emailConfig.Email));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        message.Body = new TextPart("html") { Text = body };

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailConfig.Host, 587, false);
                await client.AuthenticateAsync(_emailConfig.Email, _emailConfig.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email!!\n {ex.Message}");
            }
        }
    }
}

}