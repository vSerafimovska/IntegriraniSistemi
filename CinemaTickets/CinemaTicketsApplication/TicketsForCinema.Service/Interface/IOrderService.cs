using System.Collections.Generic;
using TicketsForCinema.Domain;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Service.Interface {
    public interface IOrderService {

        public List<Order> getAllOrders();

        public Order getOrderDetails(BaseEntity model);

    }
}