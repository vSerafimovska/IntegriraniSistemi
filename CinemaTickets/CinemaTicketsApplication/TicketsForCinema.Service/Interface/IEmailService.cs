using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Service.Interface {
    public interface IEmailService {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
