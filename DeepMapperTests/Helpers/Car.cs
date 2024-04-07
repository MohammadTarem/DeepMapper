using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepMapperTests.Helpers
{
    public class Car
    {
        public string Name { get;  set; }
        public string Manufacturer { get;  set; }
        public Engine Engine { get;  set; }
    }

    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public CustomerDto Customer { get; set; }
    }

    public class CustomerDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public Customer Customer { get; set; }
        public DateTime Created { get; set; }

    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }

}
