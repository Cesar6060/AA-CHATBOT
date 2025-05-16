namespace AAU.Components.Classes;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

public class EmailConfig
{
              public static void ConfigureEmail()
              {
                     var smtpClient = new SmtpClient("smtp.gmail.com")
                     {
                            Port = 587, 
                            Credentials = new NetworkCredential("angelsamongus561@gmail.com", "zpkz ebwj vdby wqbj"),
                            EnableSsl = true
                     };
                     Email.DefaultSender = new SmtpSender(smtpClient);
              }
              
              public async Task SendEmailAsync(string from, string to, string subject, string body)
              {
                     var email = await Email
                            .From(from)
                            .To(to, "AAU User")
                            .Subject(subject)
                            .Body(body, isHtml: false)
                            .SendAsync();

                     if (email.Successful)
                     {
                            Console.WriteLine("Email sent successfully!");
                     }
                     else
                     {
                            Console.WriteLine("Failed to send email.");
                            foreach (var error in email.ErrorMessages)
                            {
                                   Console.WriteLine(error);
                            }
                     }
              }
       }