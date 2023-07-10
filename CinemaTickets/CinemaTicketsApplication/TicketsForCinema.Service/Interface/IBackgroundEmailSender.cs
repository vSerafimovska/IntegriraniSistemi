using System.Threading.Tasks;

namespace TicketsForCinema.Service.Interface {
    public interface IBackgroundEmailSender {
        Task DoWork();
    }
}
