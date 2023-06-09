using DataAccessLayer.Models;

namespace BussinessLogicLayer.DTOs
{
    public class TicketDTO
    {
        public string TicketId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; } = "Open";

        public static implicit operator TicketDTO(Ticket ticket)
        {
            return new TicketDTO
            {
                TicketId = ticket.TicketId,
                Subject = ticket.Subject,
                Description = ticket.Description,
                UserId = ticket.UserId,
                Status = ticket.Status
            };
        }
        public static explicit operator Ticket(TicketDTO dto)
        {
            return new Ticket
            {
                TicketId = dto.TicketId,
                Subject = dto.Subject,
                Description = dto.Description,
                UserId = dto.UserId,
                Status = dto.Status
            };
        }
    }
}
