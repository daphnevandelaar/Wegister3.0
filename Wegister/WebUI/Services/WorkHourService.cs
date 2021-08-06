using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Viewmodels;
using Common;
using WebUI.Dtos;

namespace WebUI.Services
{
    public class WorkHourService
    {
        private static readonly List<WorkHourVm> workhours = new ()
        {
            new WorkHourVm
            {
                Id = 1,
                CustomerName = "KPN",
                Date = new DateTime(2020, 10, 20).ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10,
                Description = "dsfadfsadf "
            },
            new WorkHourVm
            {
                Id = 2,
                CustomerName = "Google",
                Date = new DateTime(2021, 5, 12).ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10,
                Description = "dsfadfsadf "
            },
            new WorkHourVm
            {
                Id = 3,
                CustomerName = "Ziggo",
                Date = new DateTime().ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10,
                Description = "dsfadfsadf "
            },
            new WorkHourVm
            {
                Id = 4,
                CustomerName = "KPN",
                Date = new DateTime().ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10,
                Description = "dsfadfsadf "
            }
        };

        private static WorkHourListVm Summaries = new (workhours);

        public WorkHourService()
        {
            
        }

        public WorkHourListVm GetHours()
        {
            return Summaries;
        }

        public void AddWorkHour(WorkHourDto workHour)
        {
            workhours.Add(new WorkHourVm
            {
                CustomerName = workHour.CustomerId.ToString(),
                Date = workHour.Date.ToString("dd-MM-yyyy"),
                StartTime = workHour.StartTime.ToString(@"hh\:mm"), //TODO: dont forget to return this notation from backend
                EndTime = workHour.EndTime.ToString(@"hh\:mm"),
                RecreationInMinutes = workHour.RecreationInMinutes,
                Description = workHour.Description
            });
        }

        public WorkHourListVm SearchHours(List<FilterValueListDto> filters)
        {
            var summaries = Summaries;

            foreach (var filter in filters)
            {
                if (filter.Type == "year")
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => f.Date.Contains(filter.FilterValue.Value)).ToList());
                if (filter.Type == "customer")
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => f.CustomerName.Contains(filter.FilterValue.Value)).ToList());

                if (filter.Type == "week")
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => "Week " + WeekNumberHelper.GetWeeknumberFromString(f.Date) == filter.FilterValue.Value).ToList());
            }

            return summaries;
        }

        public List<FilterValueListVm> GetFilters()
        {
            return new()
            {
                new FilterValueListVm
                {
                    Name = "Selecteer weeknummer",
                    Type = "week",
                    FilterValues = new List<FilterValueVm>
                    {
                        new()
                        {
                            Id = 1,
                            Value = "Week 1"
                        },
                        new()
                        {
                            Id = 2,
                            Value = "Week 5"
                        },
                        new()
                        {
                            Id = 3,
                            Value = "Week 23"
                        }
                    }
                },
                new FilterValueListVm
                {
                    Name = "Selecteer jaartal",
                    Type = "year",
                    FilterValues = new List<FilterValueVm>
                    {
                        new()
                        {
                            Id = 1,
                            Value = "2020"
                        },
                        new()
                        {
                            Id = 2,
                            Value = "2021"
                        },
                        new()
                        {
                            Id = 3,
                            Value = "0001"
                        }
                    }
                },
                new FilterValueListVm
                {
                    Name = "Selecteer klant",
                    Type = "customer",
                    FilterValues = new List<FilterValueVm>
                    {
                        new()
                        {
                            Id = 1,
                            Value = "KPN"
                        },
                        new()
                        {
                            Id = 2,
                            Value = "Ziggo"
                        },
                        new()
                        {
                            Id = 3,
                            Value = "Google"
                        }
                    }
                }
            };
        }

        public void DeleteWorkHour(int workHourId)
        {
            Summaries = new WorkHourListVm(Summaries.WorkHours.Where(w => w.Id != workHourId).ToList());
        }
    }
}
