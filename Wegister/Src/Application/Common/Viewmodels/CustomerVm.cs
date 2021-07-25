namespace Application.Common.Viewmodels
{
    public class CustomerVm : AddressBaseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailToSendHoursTo { get; set; }
    }
}
