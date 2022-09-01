using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDemo.Services
{
    public interface IDbStore<T>
    {
        Task<int> AddContactAsync(T contact);
        Task<int> DeleteContactAsync(T contact);
        Task<int> UpdateContactAsync(T contact);
        Task<int> DeleteContactAsync(int id);
        Task<T> GetContactAsync(int id);
        Task<List<T>> GetContactsAsync(bool forceRefresh = false);
    }
}

