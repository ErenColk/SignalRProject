using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDto;

namespace SignalRWebUI.Controllers
{
    public class MailController : Controller
    {



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(CreateMailDto createMailDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalR Rezervasyon","eren.colk01@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
            mimeMessage.From.Add(mailboxAddressTo);


            var bodyBuilder = new BodyBuilder();    
            bodyBuilder.TextBody = createMailDto.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = createMailDto.Subject;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com",587,false);
            smtpClient.Authenticate("eren.colk01@gmail.com", "lkzb krhq qsyg vxmt");


            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
                


            return RedirectToAction("Index","Category");
        }
    }
}
