using System.Collections.Generic;
using TicketsForCinema.Domain.Relations;

namespace TicketsForCinema.Domain.DTO {
    public class ShoppingCartDTO {
        public List<ProductInShoppingCart> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}