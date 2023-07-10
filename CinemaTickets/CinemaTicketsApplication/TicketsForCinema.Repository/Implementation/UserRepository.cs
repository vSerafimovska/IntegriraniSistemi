using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketsForCinema.Domain.Identity;
using TicketsForCinema.Repository.Interface;

namespace TicketsForCinema.Repository.Implementation {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext context;
        private DbSet<TicketsForCinemaUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context) {
            this.context = context;
            entities = context.Set<TicketsForCinemaUser>();
        }
        public IEnumerable<TicketsForCinemaUser> GetAll() {
            return entities.AsEnumerable();
        }

        public TicketsForCinemaUser Get(string id) {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.ProductInShoppingCarts")
               .Include("UserCart.ProductInShoppingCarts.CurrentProduct")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(TicketsForCinemaUser entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(TicketsForCinemaUser entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(TicketsForCinemaUser entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
