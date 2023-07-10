using System;

namespace TicketsForCinema.Domain.DomainModels {
    public class EmailMessage : BaseEntity {
        public string Mailto { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Boolean Status { get; set; }
    }
}