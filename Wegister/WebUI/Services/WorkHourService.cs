using System;
using System.Collections.Generic;
using WebUI.Dtos;
using WebUI.Models;

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
                Date = new DateTime().ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10
            },
            new WorkHourVm
            {
                Id = 2,
                CustomerName = "Google",
                Date = new DateTime().ToString("dd-MM-yyyy"),
                StartTime = "10:20",
                EndTime = "15:39",
                RecreationInMinutes = 1,
                TotalWorkHoursInMinutes = 10,
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
            },
        };

        private static readonly WorkHourListVm Summaries = new (workhours);

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
                RecreationInMinutes = workHour.RecreationInMinutes
            });
        }
    }
}
