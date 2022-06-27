using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Contacts
{
    public class ManageContact : IManageContact
    {
        private readonly BTL_KTPMDbContext _context;
        public ManageContact(BTL_KTPMDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CreateContactRequest request)
        {
            var contact = new Contact()
            {
                Email = request.Email,
                Message = request.Message,
                Name = request.Name,
                NumberPhone = request.NumberPhone,
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<int> Delete(int ContactId)
        {
            var contact = await _context.Contacts.Where(x => x.Id == ContactId).FirstOrDefaultAsync();
            if (contact == null) throw new BTL_KTPMException("Can not find product");
            _context.Contacts.Remove(contact);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Contact>> GetAll()
        {
            var data = await _context.Contacts.ToListAsync();
            return data;
        }

        public async Task<Contact> GetById(int ContactId)
        {
            var categories = await _context.Contacts.Where(x => x.Id == ContactId).FirstOrDefaultAsync();

            if (categories != null)
            {                
                return categories;
            }
            else
            {
                return null;
            }
        }
    }
}
