using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsDemo.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
