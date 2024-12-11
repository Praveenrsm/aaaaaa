using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmTradeDataLayer.Repository
{
    public interface IUserRepository
    {
        void Updateuser(User user);
        void Adduser(User user);
        string Login(User user);
        User GetUserById(Guid userId);
        IEnumerable<User> Getusers(int pageNumber, int pageSize);
        void DeleteUser(Guid userId);
    }
}
