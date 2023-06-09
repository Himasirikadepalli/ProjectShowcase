
using BussinessLogicLayer.DTOs;

namespace BussinessLogicLayer.Services
{
    public interface ITicketServices
    {
        public Task<TicketDTO> CreateTicket(TicketDTO ticket);

        public Task<bool> UpdateStatus(string id, string status);
    }
}
