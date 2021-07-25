namespace Application.Common.Models
{
    public class AddressBase
    {
        public string Street { get; }
        public string HouseNumber { get; }
        public string Place { get; }
        public string ZipCode { get; }

        public AddressBase(string street, string houseNumber, string place, string zipCode)
        {
            Street = street;
            HouseNumber = houseNumber;
            Place = place;
            ZipCode = zipCode;
        }
    }
}
