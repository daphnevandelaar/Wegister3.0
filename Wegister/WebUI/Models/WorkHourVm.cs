namespace WebUI.Models
{
    public class WorkHourVm
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int TotalWorkHoursInMinutes { get; set; }
    }
}
