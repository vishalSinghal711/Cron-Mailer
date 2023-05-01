using System;
using System.Threading.Tasks;
using JobsOptions;
using Microsoft.Extensions.Options;
using OnBusyness.Behaviour;
using OnBusyness.Services.EmailService;
using Quartz;


[DisallowConcurrentExecution]
public class EmailSenderJob : IJob {

    private readonly IEmailService _emailService;
    private readonly IOptions<MailSenderJobOptions> _options;


    public EmailSenderJob(IEmailService emailService, IOptions<MailSenderJobOptions> options) {
        _emailService = emailService;
        if( options.Value.amountOfDays == null ) {
            throw new ArgumentException("amount of days cant be null");
        }
        else {
            _options = options;
        }
    }

    public async Task Execute(IJobExecutionContext context) {
        new CompaignContract().sendMailToNonLoyal(_emailService);
    }
}