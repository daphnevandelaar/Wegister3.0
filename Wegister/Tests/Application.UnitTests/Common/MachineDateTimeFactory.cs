using System;
using Common;

namespace Application.UnitTests.Common
{
    class MachineDateTimeFactory : IDateTime
    {
        public DateTime Now => new(3001, 1, 1);
    }
}
