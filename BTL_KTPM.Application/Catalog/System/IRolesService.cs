using BTL_KTPM.Application.Catalog.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.System
{
    public interface IRolesService
    {
        Task<List<RolesViewModel>> GetAll();
    }
}
