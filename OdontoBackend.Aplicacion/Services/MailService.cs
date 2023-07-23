using Microsoft.Extensions.Options;
using MimeKit;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Services.Api.Configurations;

using MailKit.Net.Smtp;
using Newtonsoft.Json;

using System.Net.Http.Json;
using System.Net.Http;
using MailKit.Security;
using Microsoft.AspNetCore.Http;

using System.Text;

using System.Diagnostics;
using System.Dynamic;

namespace OdontoBackend.Aplicacion.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettingsOptions/*, IHttpClientFactory httpClientFactory*/)
        {
            _mailSettings = mailSettingsOptions.Value;
           // _httpClient = httpClientFactory.CreateClient("MailTrapApiClient");
        }


        //Envio de mail solo cuerpo
        public async Task<bool> SendMailAsync(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    //Datos del Mail desde
                    MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderNameFrom, _mailSettings.SenderEmailFrom);
                    emailMessage.From.Add(emailFrom);
                    //Datos del Mail para
                    var mails = mailData.EmailToId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (mails.Length == 1)
                    {
                        MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                        emailMessage.To.Add(emailTo);
                    }
                    else
                    {
                        foreach (var address in mails)
                        {
                            emailMessage.To.Add(MailboxAddress.Parse(address));
                        }
                    }

                    //MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    //emailMessage.To.Add(emailTo);
                    //foreach (var address in mailData.EmailToId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    //{
                    //    MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, address);
                    //    emailMessage.To.Add(emailTo);
                    //}
                    // Receiver
                    //foreach (string mailAddress in mailData.EmailToId)
                    //    emailMessage.To.Add(MailboxAddress.Parse(mailAddress));


                    //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailData.EmailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }


        //public async Task<bool> SendMailAsync(MailDataWithAttachments mailData, CancellationToken ct = default)
        //{
        //    try
        //    {
        //        // Initialize a new instance of the MimeKit.MimeMessage class
        //        var mail = new MimeMessage();

        //        #region Sender / Receiver
        //        // Sender
        //        // Sender
        //        mail.From.Add(new MailboxAddress(_mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom));
        //        mail.Sender = new MailboxAddress(mailData.DisplayName ?? _mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom);

        //        // Receiver
        //        foreach (string mailAddress in mailData.To)
        //            mail.To.Add(MailboxAddress.Parse(mailAddress));

        //        // Set Reply to if specified in mail data
        //        if (!string.IsNullOrEmpty(mailData.ReplyTo))
        //            mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

        //        // BCC
        //        // Check if a BCC was supplied in the request
        //        if (mailData.Bcc != null)
        //        {
        //            // Get only addresses where value is not null or with whitespace. x = value of address
        //            foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
        //                mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
        //        }

        //        // CC
        //        // Check if a CC address was supplied in the request
        //        if (mailData.Cc != null)
        //        {
        //            foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
        //                mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
        //        }
        //        #endregion

        //        #region Content

        //        // Add Content to Mime Message
        //        var body = new BodyBuilder();
        //        mail.Subject = mailData.Subject;
        //        body.HtmlBody = mailData.Body;
        //        mail.Body = body.ToMessageBody();

        //        // Check if we got any attachments and add the to the builder for our message
        //        if (mailData.Attachments != null)
        //        {
        //            byte[] attachmentFileByteArray;

        //            foreach (IFormFile attachment in mailData.Attachments)
        //            {
        //                // Check if length of the file in bytes is larger than 0
        //                if (attachment.Length > 0)
        //                {
        //                    // Create a new memory stream and attach attachment to mail body
        //                    using (MemoryStream memoryStream = new MemoryStream())
        //                    {
        //                        // Copy the attachment to the stream
        //                        attachment.CopyTo(memoryStream);
        //                        attachmentFileByteArray = memoryStream.ToArray();
        //                    }
        //                    // Add the attachment from the byte array
        //                    body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
        //                }
        //            }
        //        }

        //        #endregion

        //        #region Send Mail

        //        using var smtp = new SmtpClient();

        //        if (_mailSettings.UseSSL)
        //        {
        //            //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
        //            await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
        //        }
        //        else if (_mailSettings.UseStartTls)
        //        {
        //            await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //            //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
        //        }

        //        await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);
        //        //await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        //        await smtp.SendAsync(mail, ct);
        //        await smtp.DisconnectAsync(true, ct);
        //        return true;
        //        #endregion

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    

    ////Envio de mail con cuerpo html
    //public async Task<bool> SendHTMLMailAsync(HTMLMailData htmlMailData)
    //{
    //    string filePath = Directory.GetCurrentDirectory() + "\\Templates\\Hello.html";
    //    string emailTemplateText = File.ReadAllText(filePath);

    //    var htmlBody = string.Format(emailTemplateText, htmlMailData.EmailToName, DateTime.Today.Date.ToShortDateString());

    //    var apiEmail = new
    //    {
    //        From = new { Email = _mailSettings.SenderNameFrom, Name = _mailSettings.SenderEmailFrom },
    //        To = new[] { new { Email = htmlMailData.EmailToId, Name = htmlMailData.EmailToName } },
    //        Subject = "Hello",
    //        Html = htmlBody
    //    };

    //    var httpResponse = await _httpClient.PostAsJsonAsync("send", apiEmail);

    //    var responseJson = await httpResponse.Content.ReadAsStringAsync();
    //    var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);

    //    if (response != null && response.TryGetValue("success", out object? success) && success is bool boolSuccess && boolSuccess)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    ////Envio de mail con archivo
    //public async Task<bool> SendMailWithAttachmentsAsync(MailDataWithAttachment mailDataWithAttachment)
    //{
    //    var attachments = new List<object>();
    //    if (mailDataWithAttachment.EmailAttachments != null)
    //    {
    //        foreach (var attachmentFile in mailDataWithAttachment.EmailAttachments)
    //        {
    //            if (attachmentFile.Length == 0)
    //            {
    //                continue;
    //            }

    //            using (MemoryStream memoryStream = new MemoryStream())
    //            {
    //                await attachmentFile.CopyToAsync(memoryStream);
    //                attachments.Add(new
    //                {
    //                    FileName = attachmentFile.FileName,
    //                    Content = Convert.ToBase64String(memoryStream.ToArray()),
    //                    Type = attachmentFile.ContentType,
    //                    Disposition = "attachment" // or inline
    //                });
    //            }
    //        }
    //    }

    //    var apiEmail = new
    //    {
    //        From = new { Email = _mailSettings.SenderNameFrom, Name = _mailSettings.SenderEmailFrom },
    //        To = new[] { new { Email = mailDataWithAttachment.EmailToId, Name = mailDataWithAttachment.EmailToName } },
    //        Subject = mailDataWithAttachment.EmailSubject,
    //        Text = mailDataWithAttachment.EmailBody,
    //        Attachments = attachments.ToArray()
    //    };

    //    var httpResponse = await _httpClient.PostAsJsonAsync("send", apiEmail);

    //    var responseJson = await httpResponse.Content.ReadAsStringAsync();
    //    var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);

    //    if (response != null && response.TryGetValue("success", out object? success) && success is bool boolSuccess && boolSuccess)
    //    {
    //        return true;
    //    }

    //    return false;
    //}


    public async Task<bool> SendMailWithAttachmentAsync(MailDataWithAttachments mailData, CancellationToken ct = default)
        {
            try
            {
                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(_mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom);

                // Receiver
                foreach (string mailAddress in mailData.To!)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if (!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                if (mailData.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                // CC
                // Check if a CC address was supplied in the request
                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;

                // Check if we got any attachments and add the to the builder for our message
                if (mailData.Attachments != null)
                {
                    byte[] attachmentFileByteArray;
                    foreach (IFormFile attachment in mailData.Attachments)
                    {
                        if (attachment.Length > 0)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                attachment.CopyTo(memoryStream);
                                attachmentFileByteArray = memoryStream.ToArray();
                            }
                            body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                        }
                    }
                }
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (_mailSettings.UseSSL)
                {
                    //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
                    await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_mailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
                }

                await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);
                //await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                return true;
                #endregion

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel)
        {
            //            string mailTemplate = LoadTemplate(emailTemplate);

            //            //string templateReference = @"C:\templates\report.cshtml"; // hard-coded here for simplicity
            ////TemplateKey templateKey = (TemplateKey)Engine.Razor.GetKey(templateReference);
            //            const string templateKey = "templateKey";
            //            //string preProcessedTemplate = "Hello @Model.Name!";// contents of the report.cshtml file
            //            string template = "<div>Hello @Model.Name</div>";
            //            var output = Engine.Razor.RunCompile(template, templateKey, null,null, null);// model, viewBag
            //string template = "<div>Hello @Model.Name</div>";
            //dynamic model = new ExpandoObject();
            //model.Name = "Matt";
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(currentDir);
            //Debug.WriteLine("----------" + directory.FullName);
            //// This will get the current WORKING directory (i.e. \bin\Debug)
            //string workingDirectory = Environment.CurrentDirectory;
            //// or: Directory.GetCurrentDirectory() gives the same result

            //// This will get the current PROJECT bin directory (ie ../bin/)
            //string projectDirectory = Directory.GetParent(workingDirectory).FullName;
            //Debug.WriteLine("----------" + projectDirectory);

            //// This will get the current PROJECT directory
            //string projectDirectory1 = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            //Debug.WriteLine("----------" + projectDirectory1);
            //string templateDir = directory.FullName + "\\TemplatesMail";
            //string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");
            //string output = Engine.Razor.RunCompile("C:\\Users\\CARLOS\\source\\repos\\01-ODONTO-BACKEND\\OdontoBackend.Services.Api\\TemplatesMail\\Template.cshtml", "key", null, null,null);
            //        var engine = new RazorLightEngineBuilder()
            //.UseFileSystemProject("C:/RootFolder/With/YourTemplates")
            //.UseMemoryCachingProvider()
            //.Build();

            //        var model = new { Name = "John Doe" };
            //        string output = await engine.CompileRenderAsync("Subfolder/View.cshtml", model);
            string filePath = directory.FullName + "\\TemplatesMail\\" + $"{emailTemplate}.cshtml";
            string emailTemplateText = File.ReadAllText(filePath);

            var output = string.Format(emailTemplateText, "aaa", DateTime.Today.Date.ToShortDateString());

            return output;
            //var key = new RazorEngine.Templating.NameOnlyTemplateKey("EmailTemplate", RazorEngine.Templating.ResolveType.Global, null);
            //RazorEngine.Engine.Razor.AddTemplate(key, new RazorEngine.Templating.LoadedTemplateSource("Ala ma kota"));
            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //RazorEngine.Engine.Razor.RunCompile(key, sw);
            //string s = sb.ToString();
            //return s;
            //IRazorEngine razorEngine = new RazorEngine();
            //IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

            //return modifiedMailTemplate.Run(emailTemplateModel);
        }

        public string LoadTemplate(string emailTemplate)
        {
            //string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            //Debug.WriteLine("----------" + baseDir);

            string currentDir = Environment.CurrentDirectory;

            DirectoryInfo directory = new DirectoryInfo(currentDir);
            //Debug.WriteLine("----------" + directory.FullName);
            //// This will get the current WORKING directory (i.e. \bin\Debug)
            //string workingDirectory = Environment.CurrentDirectory;
            //// or: Directory.GetCurrentDirectory() gives the same result

            //// This will get the current PROJECT bin directory (ie ../bin/)
            //string projectDirectory = Directory.GetParent(workingDirectory).FullName;
            //Debug.WriteLine("----------" + projectDirectory);

            //// This will get the current PROJECT directory
            //string projectDirectory1 = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            //Debug.WriteLine("----------" + projectDirectory1);
            string templateDir = directory.FullName + "\\TemplatesMail";
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }

        public async Task<bool> SendMailAsync(MailDataAll mailData, CancellationToken ct = default)
        {
            try
            {
                // Initialize a new instance of the MimeKit.MimeMessage class
                var mail = new MimeMessage();

                #region Sender / Receiver
                // Sender
                mail.From.Add(new MailboxAddress(_mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom));
                mail.Sender = new MailboxAddress(mailData.DisplayName ?? _mailSettings.SenderNameFrom, mailData.From ?? _mailSettings.SenderEmailFrom);
                // Receiver
                foreach (string mailAddress in mailData.To)
                    mail.To.Add(MailboxAddress.Parse(mailAddress));

                // Set Reply to if specified in mail data
                if (!string.IsNullOrEmpty(mailData.ReplyTo))
                    mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

                // BCC
                // Check if a BCC was supplied in the request
                if (mailData.Bcc != null)
                {
                    // Get only addresses where value is not null or with whitespace. x = value of address
                    foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                // CC
                // Check if a CC address was supplied in the request
                if (mailData.Cc != null)
                {
                    foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }
                #endregion

                #region Content

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = mailData.Subject;
                body.HtmlBody = mailData.Body;
                mail.Body = body.ToMessageBody();

                #endregion

                #region Send Mail

                using var smtp = new SmtpClient();

                if (_mailSettings.UseSSL)
                {
                    //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
                    await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_mailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    //await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
                }

                await smtp.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);
                //await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);

                #endregion

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
