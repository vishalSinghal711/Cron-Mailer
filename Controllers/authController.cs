using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBusyness.Behaviour;
using OnBusyness.Model;
using OnBusyness.Services.EmailService;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OnBusyness.Behaviour.CompaignContract;

namespace JwtWebApiTutorial.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaignController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public CompaignController(IConfiguration configuration, IEmailService emailService) {
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult<object> sendMailToNonLoyal() {
            return new CompaignContract().sendMailToNonLoyal(_emailService);
        }

        [HttpPost]
        public ActionResult<object> addFestival([FromBody] TblFestival tblFestival) {
            return new CompaignContract().addFestival(tblFestival);
        }
        [HttpPost]
        public ActionResult<object> addProduct([FromBody] TblOurProduct tblOurProduct) {
            return new CompaignContract().addProduct(tblOurProduct);
        }
        [HttpPost]
        public ActionResult<object> addContact([FromBody] TblContact tblContact) {
            return new CompaignContract().addContact(tblContact);
        }
        [HttpPost]
        public ActionResult<object> addLoyalContact([FromBody] TblLoyalContact tblLoyalContact) {
            return new CompaignContract().addLoyalContact(tblLoyalContact);
        }
        [HttpPost]
        public ActionResult<object> addContactProductIntrest([FromBody] BindingDTO bindingDTO) {
            return new CompaignContract().addContactProductIntrest(bindingDTO);
        }
        [HttpPost]
        public ActionResult<object> addLoyalContactProductIntrest([FromBody] BindingDTO bindingDTO) {
            return new CompaignContract().addLoyalContactProductIntrest(bindingDTO);
        }
    }
}