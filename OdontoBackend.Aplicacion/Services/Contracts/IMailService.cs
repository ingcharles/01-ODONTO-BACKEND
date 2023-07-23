
using OdontoBackend.Aplication.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services.Contracts
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(MailData mailData);
        Task<bool> SendMailAsync(MailDataAll mailData, CancellationToken ct = default);

        //Task<bool> SendHTMLMailAsync(HTMLMailData htmlMailData);
        //Task<bool> SendMailWithAttachmentsAsync(MailDataWithAttachment mailDataWithAttachment);
        Task<bool> SendMailWithAttachmentAsync(MailDataWithAttachments mailData, CancellationToken ct = default);
        string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
        //Task<bool> SendMailAsync(MailDataWithAttachments mailData, CancellationToken ct);

    }
}
