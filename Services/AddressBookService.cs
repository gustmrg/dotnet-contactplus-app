using System;
using ContactPlus.Data;
using ContactPlus.Interfaces;
using ContactPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPlus.Services
{
    public class AddressBookService : IAddressBookService
    {
        private readonly ApplicationDbContext _context;

        public AddressBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
            try
            {
                // check if the category is in the contact
                if (!await IsContactInCategory(categoryId, contactId))
                {
                    var contact = await _context.Contacts.FindAsync(contactId);
                    var category = await _context.Categories.FindAsync(categoryId);

                    if (category != null && contact != null)
                    {
                        category.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<ICollection<Category>> GetContactCategoriesAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId)
        {
            var categories = new List<Category>();

            try
            {
                categories = await _context.Categories.Where(c => c.UserId == userId).OrderBy(c => c.Name).ToListAsync();
            }
            catch
            {
                throw;
            }

            return categories;
        }

        public async Task<bool> IsContactInCategory(int categoryId, int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);

            return await _context.Categories
                .Include(c => c.Contacts)
                .Where(c => c.Id == categoryId && c.Contacts.Contains(contact))
                .AnyAsync();
        }

        public Task RemoveContactFromCategoryAsync(int categoryId, int contactId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> SearchForContacts(int searchString, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

