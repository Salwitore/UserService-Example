using Business;
using Data;
using Data.EntityClasses;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.Attributes;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorization(Roles.Admin)]
    public class AdminController : ControllerBase
    {

        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpPost("AddUser")]//Admin
        public IActionResult AddUser(User user)
        {
            try
            {
                new UserBusiness().AddUser(user);
                return Created("", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetUser")]
        public IActionResult GetUser(int userId)
        {
            try
            {
                return Ok(new UserBusiness().GetUser(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(new UserBusiness().GetUsers());
        }

        [HttpDelete("DeleteUser")]//Admin
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                var user = new UserBusiness().DeleteUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPatch("UpdateUser")]//Admin
        public IActionResult UpdateUser(User updatedUser, int userId)
        {
            try
            {
                var user = new UserBusiness().UpdateUser(updatedUser, userId);
                if (user == null)
                {
                    return BadRequest("Verdiðiniz Id'ye ait kullanýcý bulunamadý!!");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpGet("LoadingUser")]//Manager,Employee

        public IActionResult LoadingUser(int userId)
        {
            try
            {
                var user = new UserBusiness().LoadingUser(userId);
                if (user == null)
                {
                    return BadRequest("userId geçersiz!");
                }
                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddTicket")]//Manager
        public IActionResult AddTicket(Ticket ticket)
        {
            try
            {
                new TicketBusiness().AddTicket(ticket);
                return Created("", ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTicket")]//Manager

        public IActionResult GetTicket(int ticketId)
        {
            try
            {
                return Ok(new TicketBusiness().GetTicket(ticketId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("GetTickets")]//Manager
        public IActionResult GetTickets()
        {
            return Ok(new TicketBusiness().GetTickets());
        }

        [HttpDelete("DeleteTickets")]//Manager
        public IActionResult DeleteTicket(int ticketId)
        {
            try
            {
                return Ok(new TicketBusiness().DeleteTicket(ticketId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("UpdateTicket")]//Manager
        public IActionResult UpdateTicket(Ticket updatedTicket, int ticketId)
        {
            try
            {
                var ticket = new TicketBusiness().UpdateTicket(updatedTicket, ticketId);
                if (ticket == null)
                {
                    return BadRequest("Verdiðiniz Id'ye ait Ticket bulunamadý!!");
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("MatchUserTicket")]//Manager
        public IActionResult MatchUserTicket(UserTicket userTicket)
        {
            try
            {
                var userticket = new UserTicketBusiness().MatchUserTicket(userTicket);
                if (userticket == null)
                {
                    return BadRequest("Verdiðiniz herhangi bir Id'ye ait user veya ticket bulunamadý!");
                }
                return Created("", userticket);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }







    }
}