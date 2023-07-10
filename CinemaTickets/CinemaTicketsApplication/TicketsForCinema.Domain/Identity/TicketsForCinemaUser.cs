using Microsoft.AspNetCore.Identity;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Domain.Identity {
    public class TicketsForCinemaUser : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
    }
}