using System;
using TicketsForCinema.Domain.DTO;

namespace TicketsForCinema.Service.Interface {
    public interface IShoppingCartService {

        ShoppingCartDTO getShoppingCartInfo(string userId);

        bool deleteProductFromSoppingCart(string userId, Guid productId);

        bool order(string userId);

    }
}