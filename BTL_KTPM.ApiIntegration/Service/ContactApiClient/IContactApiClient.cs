using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;
using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.ApiIntegration.Service.ContactApiClient
{
    public interface IContactApiClient
    {
        Task<ApiResult<List<Contact>>> GetAll();
        Task<ApiResult<bool>> CreateContact(CreateContactRequest request);
        Task<Contact> GetById(int id);
        Task<bool> Delete(int id);
    }
}
