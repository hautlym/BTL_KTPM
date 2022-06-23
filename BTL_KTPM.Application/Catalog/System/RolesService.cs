using BTL_KTPM.Application.Catalog.System.Dtos;
using BTL_KTPM.Data.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.System
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<AppRoles> _roleManager;
        public RolesService(RoleManager<AppRoles> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<RolesViewModel>> GetAll()
        {
            var roles = await _roleManager.Roles
                   .Select(x => new RolesViewModel()
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Description = x.Description
                   }).ToListAsync();

            return roles;
        }
    }
}
