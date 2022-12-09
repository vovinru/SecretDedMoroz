using System;
using System.Net;
using System.Net.Mail;

namespace SecretDedMorozLibrary
{
    public static class PostSend
    {

        /// <summary>
        /// отправка письма по почте
        /// </summary>
        /// <param name="addressSender">почта отправителя</param>
        /// <param name="passwordSender">пароль отправлителя</param>
        /// <param name="smtpName">имя клиента почты</param>
        /// <param name="smtpPort">порт</param>
        /// <param name="addressRecipient">почта получателя</param>
        /// <param name="subject">тема письма</param>
        /// <param name="body">текст письма</param>
        public static void SendMesage(string addressSender, string passwordSender, string smtpName, int smtpPort,
            string addressRecipient, string subject, string body)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(addressSender);
            // кому отправляем
            MailAddress to = new MailAddress(addressRecipient);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = body;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(smtpName, smtpPort);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(addressSender, passwordSender);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
