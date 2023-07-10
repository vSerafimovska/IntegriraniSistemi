using System;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Domain.Relations {
    public class ProductInShoppingCart : BaseEntity {
        public Guid ProductId { get; set; }
        public virtual Product CurrentProduct { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

        public int Quantity { get; set; }
    }
}