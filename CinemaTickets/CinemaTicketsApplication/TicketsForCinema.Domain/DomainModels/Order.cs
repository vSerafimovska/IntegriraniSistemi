using System.Collections.Generic;
using TicketsForCinema.Domain.Identity;
using TicketsForCinema.Domain.Relations;

namespace TicketsForCinema.Domain.DomainModels {
    public class Order : BaseEntity {
        public string UserId { get; set; }
        public TicketsForCinemaUser User { get; set; }
        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}