using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Users.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Users
{
    public class ManagerUser : IManagerUser
    {
        private readonly BTL_KTPMDbContext _dbContext;
        public ManagerUser(BTL_KTPMDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Create(CreateUserRequest request)
        {
            var user = new User()
            {
                UserName = request.UserName,
                Password = request.Password,
                Dob = request.Dob,
                Sex = request.Sex,
                Address = request.Address,
                Name = request.Name,
                Roles = "user"
            };
            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.id;
        }

        public async Task<int> Delete(int UserId)
        {
            var user = await _dbContext.users.Where(x => x.id == UserId).FirstOrDefaultAsync();
            if (user == null) throw new BTL_KTPMException("Can not find product");
            _dbContext.users.Remove(user);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserViewModel>> GetAllUser()
        {
            var data = await _dbContext.users.Select(x => new UserViewModel()
            {
                id = x.id,
                UserName = x.UserName,
                Password = x.Password,
                Name = x.Name,
                Roles= x.Roles,
                Address = x.Address,
                Sex= x.Sex,
                Dob= x.Dob
                
            }).ToListAsync();
            return data;
        }

        public async Task<UserViewModel> GetById(int userId)
        {
            var user = await _dbContext.users.Where(x => x.id == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                var userViewModel = new UserViewModel()
                {
                    id = user.id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Name = user.Name,
                    Roles = user.Roles,
                    Address = user.Address,
                    Sex = user.Sex,
                    Dob = user.Dob,
                };
                return userViewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(UpdateUserRequest request)
        {
            var user = await _dbContext.users.Where(x => x.id == request.id).FirstOrDefaultAsync();
            if (user == null) throw new BTL_KTPMException("Can not find product");
            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Name = request.Name;
            user.Dob = request.Dob;
            user.Sex = request.Sex;
            user.Address = request.Address;
            user.Roles = "user";
            _dbContext.users.Update(user);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
