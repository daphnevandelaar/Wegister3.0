namespace Application.Common.Dtos
{
    public class CustomerLookupDto : PersonBaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailToSendHoursTo { get; set; }
    }
}
