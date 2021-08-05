using Application.Common.Builders.Interfaces;
using Domain.Entities;

namespace Application.Common.Builders
{
    public class WorkHourBuilder : IWorkHourBuilder
    {
        public WorkHour Build(WorkHour entityToExtend, User entityToBuildFrom)
        {
            entityToExtend.User = entityToBuildFrom;
            return entityToExtend;
        }
    }
}
