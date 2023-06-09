using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface ITicketRepositories
    {
        public Task<Ticket> CreateTicket(Ticket ticket);

        public Task<bool> UpdateStatus(string id, string status);
    }
}
