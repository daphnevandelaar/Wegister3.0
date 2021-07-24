using System;
using Common;

namespace Application.UnitTests.Common.Implementations
{
    class TestMachineDate : IDateTime
    {
        public DateTime Now => new(3001, 10, 7);
    }
}
