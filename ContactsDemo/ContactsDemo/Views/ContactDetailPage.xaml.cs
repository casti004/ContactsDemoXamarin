using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ContactsDemo.Models;
using ContactsDemo.ViewModels;

namespace ContactsDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ContactDetailPage : ContentPage
    {
        ContactDetailViewModel viewModel;

        public ContactDetailPage(ContactDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ContactDetailPage()
        {
            InitializeComponent();

            var contact = new Contact
            {
                FirstName = "First",
                LastName = "Last",
                Email = "first.last@somecompany.com",
                Phone = "18881234567"
            };

            viewModel = new ContactDetailViewModel(contact);
            BindingContext = viewModel;
        }

        async void Update_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateContact", viewModel);
            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteContact", viewModel);
            await Navigation.PopAsync();
        }
    }
}