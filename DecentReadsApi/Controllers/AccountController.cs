using DecentReadsApi.Entities;
using DecentReadsApi.Models;
using DecentReadsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DecentReadsApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DecentReadsDbContext dbContext;

        public AccountController(IAccountService accountService, IHttpContextAccessor httpContextAccessor, DecentReadsDbContext dbContext)
        {
            this.accountService = accountService;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }


        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var user = accountService.VerifyUser(dto);
            string token = accountService.GenerateJwt(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            
            return Ok(token);
        }


      

        
       


        private void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            var cookieOptionsNotHttpOnly = new CookieOptions
            {
                HttpOnly = false,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshTokenExist", "true", cookieOptionsNotHttpOnly);


            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            dbContext.SaveChanges();
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;

        }
        public static string GetClaimValue(HttpContext httpContext, string valueType)
        {
            if (string.IsNullOrEmpty(valueType)) return null;
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var valueObj = identity == null ? null : identity.Claims.FirstOrDefault(x => x.Type == valueType);
            return valueObj == null ? null : valueObj.Value;
        }

        [HttpGet("getusername")]
        public ActionResult GetUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            string userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            return Ok($"id: {userId} name: {userName} role: {userRole}");
            /*var name = GetClaimValue(HttpContext, "Name");
            return Ok(name);*/
        }



        [HttpGet("refresh")]
        public  ActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            /*            var userId = int.Parse(GetClaimValue(HttpContext, "NameIdentifier"));
            */
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user == null )
            {
                return Forbid();
            }
            else  if(user.TokenExpires < DateTime.Now)
            {
                return Unauthorized();
            }
            
            string token = accountService.GenerateJwt(user);
            return Ok(token);
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var refreshToken = Request.Cookies["refreshToken"];
            accountService.Logout(refreshToken,  userId);


            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(-1),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", "", cookieOptions);

            var cookieOptionsNotHttpOnly = new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.Now.AddDays(1),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshTokenExist", "false", cookieOptionsNotHttpOnly);




            return Ok();
        }

    }
}
