using BussinessLogicLayer.DTOs;
using BussinessLogicLayer.Exceptions;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BussinessLogicLayer.Services
{
    public class TicketServices : ITicketServices
    {
        private readonly ITicketRepositories _repositories;
        public TicketServices(ITicketRepositories repositories)
        {
            _repositories = repositories;
        }

        public async Task<bool> UpdateStatus(string id, string status)
        {
            return await _repositories.UpdateStatus(id, status);
        }

        public async Task<TicketDTO> CreateTicket(TicketDTO ticket)
        {
            char[] delimiters = new char[] { ',', ' ', '\r', '\n', '\t' };
            int WordCnt = ticket.Subject.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            if (WordCnt > 10)
                throw new BadRequestException("Subject exceeds 10 words");

            int WordCntDes = ticket.Description.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            if (WordCntDes > 50)
                throw new BadRequestException("Description exceeds 50 words");

            if (ticket.Status != "Open" || ticket.Status != "Closed")
                throw new BadRequestException("Status MisMatch");

            return await _repositories.CreateTicket((Ticket)ticket);
        }
    }
}
