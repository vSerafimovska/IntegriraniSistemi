using System;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Domain.DTO {
    public class AddToShoppingCartDTO {
        public Product SelectedProduct { get; set; }
        public Guid SelectedProductId { get; set; }
        public int Quantity { get; set; }
    }
}
