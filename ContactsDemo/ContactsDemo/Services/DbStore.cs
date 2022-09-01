using ContactsDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ContactsDemo.Services
{
    public class DbStore : IDbStore<Contact>
    {
        private readonly SQLiteAsyncConnection _database;

        public DbStore(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>();
        }

        public Task<int> AddContactAsync(Contact contact)
        {
            return _database.InsertAsync(contact);
        }

        public Task<int> DeleteContactAsync(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }

        public async Task<int> DeleteContactAsync(int id)
        {
            var foundContact = GetContactAsync(id);

            if (foundContact == null)
            {
                throw new Exception();
            }

            return await _database.DeleteAsync(foundContact);
        }

        public async Task<Contact> GetContactAsync(int id)
        {
            return await _database.Table<Contact>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateContactAsync(Contact contact)
        {
            var updateContact = GetContactAsync(contact.Id);

            if (updateContact == null)
            {
                throw new Exception();
            }

            return await _database.UpdateAsync(contact);
        }

        Task<List<Contact>> IDbStore<Contact>.GetContactsAsync(bool forceRefresh)
        {
            return _database.Table<Contact>().ToListAsync();
        }
    }
}
