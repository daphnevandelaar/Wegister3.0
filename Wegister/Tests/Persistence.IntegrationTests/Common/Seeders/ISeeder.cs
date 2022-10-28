using System.Collections.Generic;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public interface ISeeder<T> where T : class
    {
        public HashSet<T> Seed();
    }
}
