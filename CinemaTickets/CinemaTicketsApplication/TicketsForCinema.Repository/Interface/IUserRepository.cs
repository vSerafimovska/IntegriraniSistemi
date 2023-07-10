using System.Collections.Generic;
using TicketsForCinema.Domain.Identity;

namespace TicketsForCinema.Repository.Interface {
    public interface IUserRepository {

        IEnumerable<TicketsForCinemaUser> GetAll();

        TicketsForCinemaUser Get(string id);

        void Insert(TicketsForCinemaUser entity);

        void Update(TicketsForCinemaUser entity);

        void Delete(TicketsForCinemaUser entity);

    }
}