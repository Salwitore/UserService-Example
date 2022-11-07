using Data.EntityClasses;
using DataAccess;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Business;
using Data.Enums;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace Utils.JWT
{
    public class JWTService
    {

        #region Members
        private int ExpireMinutes { get; set; } = 10080; //7 days.
        private string SecretKey { get; set; } = "TW9zaGVFqwecmV6gfxUHJpdsdfmF0zcvZUtleQ==";
        private string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        #endregion 


        /// <summary>
        /// Tokenın geçerliliğini kontrol eder.
        /// </summary>
        /// <param name="token">Daha önce oluşturulmuş token</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token null yada boş");//burası değişebilir.
            }

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(); //Token geçerlilik parametreleri

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //Security token tutucu

            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler
                    .ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
                // Validate Token, Kompakt Serileştirilmiş Formatta JWS veya JWE olarak kodlanmış bir 'JSON Web Tokenını' (JWT) okur ve doğrular.
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Token generate eder.
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GenerateToken(User model)
        {
            if (model == null || model.UserId == 0 || model.Email == null || model.Role == Roles.None || model.UserPassword == null)
            {
                throw new ArgumentException("Token oluşturmaya yönelik argümanlar geçerli değil."); //burası değişebilir.
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,model.UserName),
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(ClaimTypes.Role,model.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier,model.UserId.ToString()),
            };

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor //Bir Security token oluşturmak için kullanılan bazı bilgileri içerir.
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(ExpireMinutes)), //ne kadar süre geçerli olucak
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithm) // Security imzası
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();//Security token oluşturucu
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        /// <summary>
        /// Token claimlerini gönderir
        /// </summary>
        /// <param name="token">Daha önce oluşturulmuş token</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token null veya boş.");//burası değişebilir
            }
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler
                    .ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Secretkey'i symmetric key haline getirir ve symmetrickeyi döner
        /// </summary>
        /// <returns></returns>
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        /// <summary>
        /// Token geçerlilik parametrelerini gönderir.
        /// </summary>
        /// <returns></returns>
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

    }
}
