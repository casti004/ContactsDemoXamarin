using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsDemo.Models;

namespace ContactsDemo.Services
{
    public class MockDataStore : IDataStore<Contact>
    {
        List<Contact> contacts;

        public MockDataStore()
        {
            contacts = new List<Contact>();
            var mockContacts = new List<Contact>
            {
                new Contact { Id = Guid.NewGuid().ToString(), FirstName="Bill", LastName="Gates", Email="bill.gates@microsoft.com", Phone="8581234567" },
                new Contact { Id = Guid.NewGuid().ToString(), FirstName="Steve", LastName="Jobs", Email="steve.jobs@apple.com", Phone="8589876541"},
                new Contact { Id = Guid.NewGuid().ToString(), FirstName="Jeff", LastName="Besos", Email="jeff.besos@amazon.com", Phone="8584445566"},
                new Contact { Id = Guid.NewGuid().ToString(), FirstName="Larry", LastName="Ellison", Email="larry.ellison@oracle.com", Phone="8587778899"}
            };

            foreach (var contact in mockContacts)
            {
                contacts.Add(contact);
            }
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            contacts.Add(contact);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteContactAsync(Contact contact)
        {
            contacts.Remove(contact);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            var oldContact = contacts.Where((Contact arg) => arg.Id == contact.Id).FirstOrDefault();
            contacts.Remove(oldContact);
            contacts.Add(contact);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteContactAsync(string id)
        {
            var oldContact = contacts.Where((Contact arg) => arg.Id == id).FirstOrDefault();
            contacts.Remove(oldContact);

            return await Task.FromResult(true);
        }

        public async Task<Contact> GetContactAsync(string id)
        {
            return await Task.FromResult(contacts.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(contacts);
        }
    }
}