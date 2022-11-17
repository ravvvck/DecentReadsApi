using DecentReadsApi.Entities;
using DecentReadsApi.Exceptions;
using DecentReadsApi.Models;
using DecentReadsApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DecentReadsApi.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(User user);
        User VerifyUser(LoginDto dto);
        void Logout(string refreshToken, int userId);
    }

    public class AccountService : IAccountService
    {
        private readonly GoodreadsDbContext context;
        private readonly Microsoft.AspNetCore.Identity.IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(GoodreadsDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        
        public User VerifyUser(LoginDto dto)
        {
            var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.Username == dto.Username);
            if (user == null)
            {
                throw new ForbidException();
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new ForbidException();
            }
            return user;
        }

           public string GenerateJwt(User user)
        {
            var verifiedUser = user;

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, verifiedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{verifiedUser.Username}"),
                new Claim(ClaimTypes.Role, $"{verifiedUser.Role.Name}")
            };

            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            

            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Username = dto.Username,
                RoleId = dto.RoleId,
                PasswordHash = dto.Password
            };
            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            var user = context.Users.FirstOrDefault(a => a.Username == dto.Username);
           /* if (user != null)
            {
                throw new AlreadyExistException("Name taken");

            }*/
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        public void Logout(string refreshToken, int userId)
        {


            var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.RefreshToken == refreshToken & u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
           /* Request.Cookies.Delete("darkTheme");
            Response.Cookies["refreshTokenExist"].Expires = DateTime.Now.AddDays(-1);*/

            user.RefreshToken = String.Empty;
            context.SaveChanges();

            
        }


    }
}
