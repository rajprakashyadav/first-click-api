using PERFECT.CLICK.DAL.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PERFECT.CLICK.SERVICES.Interfaces
{
    public interface IUsersService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
