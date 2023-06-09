using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TicketRepositories : ITicketRepositories
    {
        private AppDbContext _context;
        public TicketRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            if (await _context.SaveChangesAsync() == 0)
                return null;

            return ticket;
        }

        public async Task<bool> UpdateStatus(string id, string status)
        {
            Ticket ticket = await _context.Tickets.SingleOrDefaultAsync(t => t.TicketId.Equals(id));
            if (ticket == null)
                return false;
            ticket.Status = status;
            return true;
        }
    }
}
