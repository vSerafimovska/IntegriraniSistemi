using System.Linq;
using System.Threading.Tasks;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Repository.Interface;
using TicketsForCinema.Service.Interface;

namespace TicketsForCinema.Service.Implementation {
    public class BackgroundEmailSender : IBackgroundEmailSender {

        private readonly IEmailService _emailService;
        private readonly IRepository<EmailMessage> _mailRepository;

        public BackgroundEmailSender(IEmailService emailService, IRepository<EmailMessage> mailRepository) {
            _emailService = emailService;
            _mailRepository = mailRepository;
        }
        public async Task DoWork() {
            await _emailService.SendEmailAsync(_mailRepository.GetAll().Where(z => !z.Status).ToList());
        }
    }
}
