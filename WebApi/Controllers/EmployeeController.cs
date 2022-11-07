using Business;
using Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorization(Roles.Employee)]
    public class EmployeeController : ControllerBase
    {
        //Employee kendisine verilen ticket yapılınca yapıldı olarak işaretlicek
        //(ticket.state true olucak)
        //(ticket.true olduğu anda ilgili yöneticiye mail gidicek)


        [HttpPatch("TicketComplete")]

        public IActionResult TicketComplete(int ticketId)
        {
            try
            {
                var ticket = new TicketBusiness().TicketComplete(ticketId);

                if (ticket == null)
                {
                    return BadRequest("Ticket bulunamadı");
                }
                //Yöneticiye mail gönder burda olur

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

    }
}
