using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsDemo.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddContactAsync(T contact);
        Task<bool> DeleteContactAsync(T contact);
        Task<bool> UpdateContactAsync(T contact);
        Task<bool> DeleteContactAsync(int id);
        Task<T> GetContactAsync(int id);
        Task<IEnumerable<T>> GetContactsAsync(bool forceRefresh = false);
    }
}
