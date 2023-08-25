using System.Collections.Generic;

namespace Persistence.UnitTests.Common.Seeders
{
    public interface ISeeder<T> where T : class
    {
        public HashSet<T> Seed();
    }
}
