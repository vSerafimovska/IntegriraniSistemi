using System.Collections.Generic;
using TicketsForCinema.Domain;
using TicketsForCinema.Domain.DomainModels;

namespace TicketsForCinema.Repository.Interface {
    public interface IOrderRepository {

        public List<Order> GetAllOrders();

        public Order getOrderDetails(BaseEntity model);

    }
}