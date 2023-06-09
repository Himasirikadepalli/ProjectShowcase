using DataAccessLayer.Models;
using BussinessLogicLayer.DTOs;
using BussinessLogicLayer.Services;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class AppController : Controller
    {
        private readonly ITicketServices ticketServices;
        private readonly IProjectServices projectServices;
        private readonly ILogger<AppController> logger;

        //public AppController(ITicketServices ticketServices, IProjectServices projectService)
        public AppController(ITicketServices ticketServices, IProjectServices projectServices, ILogger<AppController> logger)
        {
            this.ticketServices = ticketServices;
            this.projectServices = projectServices;
            this.logger = logger;
        }

        [HttpPost("createProject")]
        [Consumes("application/json")]
        public IActionResult CreateProject([FromBody] ProjectDTO projectdto)
        {
            try
            {
                var res = projectServices.CreateProject(projectdto);
                if (res == null)
                {
                    return StatusCode(500);
                }
                logger.LogInformation("Project created Successfully.");
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while creating a project.");
                return StatusCode(500);
            }
        }

        [HttpPost("createTicket")]
        [Consumes("application/json")]
        public IActionResult CreateTicket([FromBody] TicketDTO ticketdto)
        {
            try
            {
                var res = ticketServices.CreateTicket(ticketdto);
                if (res == null)
                {
                    return StatusCode(500);
                }
                logger.LogInformation("Ticket created Successfully.");
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while creating a ticket.");
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("TicketStatusUpdate/{Id}")]
        public IActionResult UpdateStatus(string Id, string status)
        {
            try
            {
                var res = ticketServices.UpdateStatus(Id, status);
                if (res == null)
                {
                    return StatusCode(500);
                }
                logger.LogInformation("Status updated Successfully.");
                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while updating the status.");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetProjectById/{ProjectId}")]
        public IActionResult GetProjectById(string ProjectId) 
        {
            try
            {
                var prj = projectServices.GetProjectById(ProjectId);
                if (prj == null)
                {
                    return StatusCode(500);
                }
                logger.LogInformation("Project retrieved by id Successfully.");
                return Ok(prj);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while retrieving by id.");
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("SearchProject/{keyword}")]
        public IActionResult SearchProject(string keyword)
        {
            try
            {
                var project = projectServices.SearchProject(keyword);
                logger.LogInformation("Project retrieved by keyword Successfully.");
                return Ok(project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while retrieving by keyword.");
                return StatusCode(500);
            }
        }

    }
}

