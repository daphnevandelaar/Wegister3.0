namespace Application.Common.Dtos
{
    public class PersonBaseDto : BaseDto
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Place { get; set; }
        public string ZipCode { get; set; }
    }
}
