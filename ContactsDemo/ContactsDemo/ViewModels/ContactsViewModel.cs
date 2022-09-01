using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ContactsDemo.Models;
using ContactsDemo.Views;

namespace ContactsDemo.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ContactsViewModel()
        {
            Title = "Browse";
            Contacts = new ObservableCollection<Contact>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewContactPage, Contact>(this, "AddContact", async (obj, contact) =>
            {
                var newContact = contact as Contact;
                Contacts.Add(newContact);
                await DataStore.AddContactAsync(newContact);
            });

            MessagingCenter.Subscribe<ContactDetailPage, ContactDetailViewModel>(this, "DeleteContact", async (obj, Contact) =>
            {
                var deleteContact = Contact as ContactDetailViewModel;

                var itemToRemove = Contacts.Contains(deleteContact.Contact);

                if (itemToRemove)
                {
                    Contacts.Remove(deleteContact.Contact);
                    await DataStore.DeleteContactAsync(deleteContact.Contact);
                }

            });

            MessagingCenter.Subscribe<ContactDetailPage, ContactDetailViewModel>(this, "UpdateContact", async (obj, Contact) =>
            {
                var updateContact = Contact as ContactDetailViewModel;

                var itemToUpdate = Contacts.Contains(updateContact.Contact);

                if (itemToUpdate)
                {
                    Contacts.Remove(updateContact.Contact);
                    await DataStore.UpdateContactAsync(updateContact.Contact);

                    var contactToAdd = new Contact();
                    contactToAdd.Email = updateContact.Contact.Email;
                    contactToAdd.FirstName = updateContact.Contact.FirstName;
                    contactToAdd.LastName = updateContact.Contact.LastName;
                    contactToAdd.Phone = updateContact.Contact.Phone;

                    Contacts.Add(contactToAdd);
                }

            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Contacts.Clear();
                var contacts = await DataStore.GetContactsAsync(true);
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}