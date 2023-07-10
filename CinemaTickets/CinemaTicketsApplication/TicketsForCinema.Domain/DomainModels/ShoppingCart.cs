using System.Collections.Generic;
using TicketsForCinema.Domain.Identity;
using TicketsForCinema.Domain.Relations;

namespace TicketsForCinema.Domain.DomainModels {
    public class ShoppingCart : BaseEntity {
        public string OwnerId { get; set; }
        public virtual TicketsForCinemaUser Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
    }
}