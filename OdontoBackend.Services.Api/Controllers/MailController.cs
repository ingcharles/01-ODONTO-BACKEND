using Microsoft.AspNetCore.Mvc;
using OdontoBackend.Aplicacion.Services.Contracts;
using OdontoBackend.Aplication.Entities.Commands;
using System.Collections.Generic;

namespace OdontoBackend.Services.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        //injecting the IMailService into the constructor
        public MailController(IMailService _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        [Route("SendMailAsync")]
        public async Task<IActionResult> SendMailAsync(MailData mailData)
        {
            bool result = await _mailService.SendMailAsync(mailData);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }

        //[HttpPost]
        //[Route("SendMailAsync")]
        //public async Task<IActionResult> SendMailAsync(MailDataWithByteAttachments mailData)
        //{
        //    bool result = await _mailService.SendMailAsync(mailData, new CancellationToken());
        //    if (result)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        //    }
        //}
        [HttpPost]
        [Route("SendMailWithAttachmentAsync")]
        public async Task<IActionResult> SendMailWithAttachmentAsync([FromForm] MailDataWithAttachments mailData)
        {
            bool result = await _mailService.SendMailWithAttachmentAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail with attachment has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail with attachment could not be sent.");
            }
        }
        //[HttpPost]
        //[Route("SendHTMLMailAsync")]
        //public async Task<IActionResult> SendHTMLMailAsync(HTMLMailData htmlMailData)
        //{
        //    bool result = await _mailService.SendHTMLMailAsync(htmlMailData);
        //    if (result)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        //    }
        //}
        //[HttpPost]
        //[Route("SendMailWithAttachmentsAsync")]
        //public async Task<IActionResult> SendMailWithAttachmentsAsync([FromForm] MailDataWithAttachment mailDataWithAttachment)
        //{
        //    bool result = await _mailService.SendMailWithAttachmentsAsync(mailDataWithAttachment);
        //    if (result)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        //    }
        //}


        [HttpPost]
        [Route("SendEmailUsingTemplate")]
        public async Task<IActionResult> SendEmailUsingTemplate(WelcomeMail welcomeMail)
        {
            // Create MailData object
            List<string> a = new List<string>();
            a.Add(welcomeMail.Email!);
            MailDataAll mailData = new MailDataAll(a,
                "Welcome to the MailKit Demo",
            _mailService.GetEmailTemplate<WelcomeMail>("Template2", welcomeMail));


            bool sendResult = await _mailService.SendMailAsync(mailData, new CancellationToken());

            if (sendResult)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent using template.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }

}
