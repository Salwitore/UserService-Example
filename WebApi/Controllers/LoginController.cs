using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Configuration;
using System.Text;
using Utils.JWT;
using Data.EntityClasses;
using Microsoft.AspNetCore.Authorization;
using Business;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Controllerdaki Action metodları [Authorize] attribute’ü kullanılarak JWT ile korunmaktadır.
    public class LoginController : ControllerBase
    {

        [HttpPost("Login")]

        public IActionResult Login(string Email,string userPassword)
        {

            var user = new LoginBusiness().Login(Email, userPassword);
            if (user == null)
            {
                return BadRequest("Email veya şifre hatalı");
            }

            var token = new JWTService().GenerateToken(user);

            return Ok(token);
        }

        //SOLID **
        //Repository pattern unitofwork ekle**
        //Singleton araştır **
        //lazy loading ve loading çeşitleri araştır**
        //LoadingUser'da Tokenlarıda get etmek için ayrı include yazmam lazım**
        //LoadingUser'ı UserRepository'ye ekle**
        //Authentication kısmı değişicek.**
        //https://jasonwatmore.com/post/2022/02/18/net-6-role-based-authorization-tutorial-with-example-api#authorize-attribute-cs Authentication için kaynak.**
        //Çalışan kendi 
        //Bearer Token JWT araştır
        //Refresh token




    }
}
