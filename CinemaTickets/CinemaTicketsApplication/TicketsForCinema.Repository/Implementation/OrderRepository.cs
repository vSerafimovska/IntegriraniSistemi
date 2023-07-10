using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketsForCinema.Domain;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Repository.Interface;

namespace TicketsForCinema.Repository.Implementation {
    public class OrderRepository : IOrderRepository {

        private readonly ApplicationDbContext _context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context) {
            _context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders() {
            return entities
                .Include(z => z.ProductInOrders)
                .Include(z => z.User)
                .Include("ProductInOrders.Product")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(BaseEntity model) {
            return entities
                .Include(z => z.ProductInOrders)
                .Include(z => z.User)
                .Include("ProductInOrders.Product")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }

    }
}