using BTL_KTPM.Application.Catalog.Users.Dtos;
using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Users
{
    public interface IManagerUser
    {
        Task<List<UserViewModel>> GetAllUser();
        Task<int> Create(CreateUserRequest request);
        Task<int> Update(UpdateUserRequest request);
        Task<int> Delete(int UserId);
        Task<UserViewModel> GetById(int userId);
        Task<UserViewModel> Login(string user,string pass);
    }
}
