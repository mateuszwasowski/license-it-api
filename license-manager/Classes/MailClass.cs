using System;
using System.Net;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using licensemanager.Repositories.Interfaces;
using licensemanager.Repositories;
using System.Linq;
using licensemanager.Models.DataBaseModel;

namespace licensemanager.Classes
{
    public class MailClass
    {
        public ISettingsRepository SettingsRepository { get; set; } = new SettingsRepository(new DataBaseContext());

        public SettingsDb GetSettings(){
            return SettingsRepository.Get().FirstOrDefault();
        }
        internal void SendMail(string token, string email)
        {
            var setting = SettingsRepository.Get().FirstOrDefault();

            if(setting==null){
                throw new Exception("DB settings not exist");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(setting.EmailFromName, setting.Email));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject =setting.EmailSubjectGroupInvitation;
            message.Body = new TextPart("html")
            {
                Text = setting.EmailBodyGroupInvitation.Replace("{token}",token)
            };
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s,c,h,e) => true;
                client.Connect(setting.EmailHost, setting.EmailPort, true);
                client.Authenticate(setting.Email, setting.EmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
