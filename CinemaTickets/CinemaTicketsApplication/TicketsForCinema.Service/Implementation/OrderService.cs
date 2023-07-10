using System.Collections.Generic;
using TicketsForCinema.Domain;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Repository.Interface;
using TicketsForCinema.Service.Interface;

namespace TicketsForCinema.Service.Implementation {
    public class OrderService : IOrderService {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

        public List<Order> getAllOrders() {
            return _orderRepository.GetAllOrders();
        }

        public Order getOrderDetails(BaseEntity model) {
            return _orderRepository.getOrderDetails(model);
        }

    }
}