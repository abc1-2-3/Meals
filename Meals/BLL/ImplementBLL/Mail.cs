using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Meals.BLL.InterfaceBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentFTP;
using System.IO;
using System.Text;
using Meals.Models.DTO;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Meals.BLL.ImplementBLL
{
    public class Mail : IMail
    {
        private readonly ILogger<Mail> _logger;
        private readonly IConfiguration Configuration;
        public Mail(IConfiguration configuration,ILogger<Mail> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }
        public ResultObj EMail(EMail mail)
        {
            ResultObj result = new ResultObj();
            try
            {
                var MailAdress = Configuration.GetValue<string>("Mail:FromMail");
                var Connect = Configuration.GetValue<string>("Mail:Connect");
                var Authenticate = Configuration.GetValue<string>("Mail:Authenticate");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("來自", MailAdress));

                // 添加收件者
                message.To.Add(new MailboxAddress("XXX", mail.ToMail));

                // 設定郵件標題
                message.Subject = mail.Subject;

                // 使用 BodyBuilder 建立郵件內容
                var bodyBuilder = new BodyBuilder();

                // 設定文字內容
                bodyBuilder.TextBody = mail.Text;

                // 設定 HTML 內容
                //bodyBuilder.HtmlBody = "<p> HTML 內容 </p>";

                // 設定附件
                
                //bodyBuilder.Attachments.Add(mail.AppendixPath);
                

                // 設定郵件內容
                message.Body = bodyBuilder.ToMessageBody();

                
                //////寄信
                using (var client = new SmtpClient())
                {
                    client.Connect(Connect, 587, false);
                    client.Authenticate(MailAdress, Authenticate);


                    client.Send(message);
                    client.Disconnect(true);
                }
                
                result.Result = true;
                result.Message = "寄信成功";
            }catch(Exception ex)
            {
                result.Message = "失敗";
                _logger.LogError(ex.StackTrace);
            }
            return result;
        }

    }
}
