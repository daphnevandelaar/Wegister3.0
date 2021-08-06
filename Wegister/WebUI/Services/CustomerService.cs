using System.Collections.Generic;
using System.Linq;
using WebUI.ViewModels;

namespace WebUI.Services
{
    public class CustomerService
    {
        private static readonly List<CustomerVm> Summaries = new ()
        {
            new CustomerVm
            {
                Id = 1,
                Name = "Alice Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012XP"
            },
            new CustomerVm
            {
                Id = 2,
                Name = "Bob Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012XP"
            },
            new CustomerVm
            {
                Id = 3,
                Name = "Glenis Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012XP"
            },
            new CustomerVm
            {
                Id = 4,
                Name = "Tom Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012XP"
            },
            new CustomerVm
            {
                Id = 5,
                Name = "Riley Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012XP"
            }
        };

        public List<CustomerVm> GetCustomers()
        {
            return Summaries;
        }

        public void AddCustomer(CustomerVm customer)
        {
            Summaries.Add(customer);
        }
        public List<CustomerVm> SearchCustomers(string customerName)
        {
            return Summaries.Where(c => c.Name.Contains(customerName)).ToList();
        }
    }
}
