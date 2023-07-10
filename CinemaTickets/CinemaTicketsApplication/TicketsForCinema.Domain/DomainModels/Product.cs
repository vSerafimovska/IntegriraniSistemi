using System.Collections.Generic;
using TicketsForCinema.Domain.Relations;

namespace TicketsForCinema.Domain.DomainModels {
    public class Product : BaseEntity {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int ProductRating { get; set; }

        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}