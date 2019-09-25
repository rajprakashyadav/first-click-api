using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PERFECT.CLICK.DAL.Models.DataModel;
using PERFECT.CLICK.DAL.Models.ViewModel;
using PERFECT.CLICK.DAL.UnitOfWork;
using PERFECT.CLICK.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PERFECT.CLICK.SERVICES.Services
{
    public class UsersService : IUsersService
    {
        // users hardcoded for simplicity, once we are ready with db users, need to remove this
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FullName = "Admin User", MobileNumber="9090909090", UserName = "admin", Password = "admin", Role = AppRoles.Admin }
        };
        private readonly AppSettings _appSettings;
        private IUnitOfWork _unitofWork;

        public UsersService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitofWork = unitOfWork;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.UserName == username.Trim() && x.Password == password.Trim());

            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            // return user without password
            if (user != null)
                user.Password = null;

            return user;
        }
    }
}
