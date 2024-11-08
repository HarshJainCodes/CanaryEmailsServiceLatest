using CanaryEmailsService.Core;
using Microsoft.AspNetCore.Mvc;

namespace CanaryEmailsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendEmailController : ControllerBase
    {
        private ILogger<SendEmailController> _logger;
        private IEmailSender _emailSender;

        public SendEmailController(ILogger<SendEmailController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task SendEmail()
        {
            await _emailSender.SendEmail("harshjain17may@gmail.com", "-Welcome-", "--Welcome to CanaryType--");
        }
    }
}
