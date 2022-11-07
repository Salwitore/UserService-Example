using Business;
using Data.EntityClasses;
using Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorization(Roles.Manager)]
    public class ManagerController : ControllerBase
    {

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
                    return BadRequest("Verdiğiniz Id'ye ait Ticket bulunamadı!!");
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
                    return BadRequest("Verdiğiniz herhangi bir Id'ye ait user veya ticket bulunamadı!");
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
