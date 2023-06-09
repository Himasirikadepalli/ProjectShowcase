using BussinessLogicLayer.DTOs;
using BussinessLogicLayer.Exceptions;
using BussinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class TicketTest
    {
        private Mock<ITicketRepositories> _mockTicketRepository;
        private TicketServices _ticketService;
        private List<Ticket> _ticket;

        [SetUp]
        public void Setup()
        {
            _ticket = new List<Ticket>();

            _mockTicketRepository = new Mock<ITicketRepositories>();
            //_mockProjectRepository.Setup(r => r.GetAllEpics()).ReturnsAsync (_project);
            _mockTicketRepository.Setup(r => r.CreateTicket(It.IsAny<Ticket>())).ReturnsAsync((Ticket e) => {
                _ticket.Add(e);
                return e;
            });

            _ticketService = new TicketServices(_mockTicketRepository.Object);
        }

        [Test]
        public void SubjectExceeds10Words()
        {
            TicketDTO ticketDTO = new TicketDTO
            {
                TicketId = "2121",
                Subject = "title Of Project in in in in in in in in in in ",
                Description = "descriptionsOfProject",
                UserId = "superProject",
                Status = "Open"
            };

            var ex = Assert.ThrowsAsync<BadRequestException>(() => _ticketService.CreateTicket(ticketDTO));
            Assert.That(ex.Message, Is.EqualTo("Subject exceeds 10 words"));
        }

        [Test]
        public void DescExceeds50Words()
        {
            TicketDTO ticketDTO = new TicketDTO
            {
                TicketId = "2121",
                Subject = "title Of Project",
                Description = "l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l " +
                "l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l l ",
                UserId = "superProject",
                Status = "Open"
            };

            var ex = Assert.ThrowsAsync<BadRequestException>(() => _ticketService.CreateTicket(ticketDTO));
            Assert.That(ex.Message, Is.EqualTo("Description exceeds 50 words"));
        }

        [Test]
        public void TicketStatusMisMatch()
        {
            TicketDTO ticketDTO = new TicketDTO
            {
                TicketId = "2121",
                Subject = "title Of Project",
                Description = "descriptionsOfProject",
                UserId = "superProject",
                Status = "InProgress"
            };

            var ex = Assert.ThrowsAsync<BadRequestException>(() => _ticketService.CreateTicket(ticketDTO));
            Assert.That(ex.Message, Is.EqualTo("Status MisMatch"));
        }
    }
}
