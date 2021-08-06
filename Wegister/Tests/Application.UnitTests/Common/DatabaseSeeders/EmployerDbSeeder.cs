using System.Collections.Generic;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class EmployerDbSeeder : ISeeder<Employer>
    {
        public HashSet<Employer> Seed()
        {
            return new()
            {
                new Employer
                {
                    Name = "Microsoft"
                },
                new Employer
                {
                    Name = "Google"
                },
                new Employer
                {
                    Name = "Apple"
                }
            };
        }
    }
}
