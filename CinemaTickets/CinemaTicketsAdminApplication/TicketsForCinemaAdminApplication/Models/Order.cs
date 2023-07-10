using System;
using System.Collections.Generic;

namespace TicketsForCinemaAdminApplication.Models {
    public class Order {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public TicketsForCinemaApplicationUser User { get; set; }
        public ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
