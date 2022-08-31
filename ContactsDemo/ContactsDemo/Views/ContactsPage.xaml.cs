using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ContactsDemo.Models;
using ContactsDemo.Views;
using ContactsDemo.ViewModels;

namespace ContactsDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ContactsPage : ContentPage
    {
        ContactsViewModel viewModel;

        public ContactsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ContactsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var contact = args.SelectedItem as Contact;
            if (contact == null)
                return;

            await Navigation.PushAsync(new ContactDetailPage(new ContactDetailViewModel(contact)));

            // Manually deselect item.
            ContactsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewContactPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Contacts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}