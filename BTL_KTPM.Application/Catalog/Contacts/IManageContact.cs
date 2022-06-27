using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Contacts
{
    public interface IManageContact
    {
        Task<List<Contact>> GetAll();
        Task<int> Create(CreateContactRequest request);
        Task<int> Delete(int CategoryId);
        Task<Contact> GetById(int categoryId);
    }
}
