using System.Threading.Tasks;
using OnBusyness.Services.EmailService.Models;

namespace OnBusyness.Services.EmailService
{
    public interface IEmailService
    {
       public Task<bool> SendEmail(EmailDto request);
    }
}