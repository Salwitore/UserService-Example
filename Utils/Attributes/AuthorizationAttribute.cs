using Castle.Core.Internal;
using Data.Enums;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils.JWT;

namespace Utils.Attributes
{   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {

        private string[] ResultMessages = 
            { 
            "Invalid Token",
            "Invalid Role",
            "Token is null or empty"
            };

       

        private readonly Roles roles;

        public AuthorizationAttribute(Roles roles)
        {
            this.roles = roles;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            //Burda kaldım 
            //Swaggerda token oluşturup istek yolladığımda request header'ında Authorization: Bearer "Token" yazıldığından dolayı tryGetValue'da tokenı çekerken Bearer'ıda çekmiş oluyor. o yüzden token'da Bearer silinmeli
            string tokenS = token.ToString();
            tokenS = tokenS.Substring(7);
            token = (StringValues)tokenS;

            var jwtService = new JWTService();

            try
            { 
                if(!token.IsNullOrEmpty())
                { 
                    if (!jwtService.IsTokenValid(token))
                    {
                        ResponseErrorHeader(context, token, ResultMessages[0]);//Token geçerli değilse hata döndürüldü.
                        return;
                    }


                    var claimList = jwtService.GetTokenClaims(token);//Token claim listesi claimList'e atandı.
                    var roleClaim = claimList.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault(); //Token claim listesinde ClaimType'ı Role olan claim roleClaim'e atandı
                    var Role = roleClaim.Value; //roleClaim'deki Role enum'nun value'su Role'e atandı.
                    if (!Role.Equals(roles.ToString())) 
                    {
                        ResponseErrorHeader(context, token, ResultMessages[1]);//Attribute'dan aldığım roles claim'den aldığım role'e eşit değilse Hata döndürüldü.
                        return;
                    }
                }
                else
                {
                    ResponseErrorHeader(context, token, ResultMessages[2]);//Token boş ise hata döndürüldü.
                    return;
                }
            }
            catch (Exception ex)
            {
                ResponseErrorHeader(context, token, ex.Message);
                return;
            }
        }

        /// <summary>
        /// response header'a ve context.result'a gerekli hata bilgilerini ekler.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <param name="ResultMessage"></param>
        private void ResponseErrorHeader(AuthorizationFilterContext context , StringValues token,string ResultMessage)
        {
            context.HttpContext.Response.StatusCode = 401;
            context.HttpContext.Response.Headers.Add("Token",token);
            context.HttpContext.Response.Headers.Add("AuthStatus", "Unauthorized");

            context.Result = new JsonResult("Unauthorized")
            {
                Value = new
                {
                    Status = "Error",
                    Message = ResultMessage
                }
            };

        } 

        

    }
}
