namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string CompanyId { get; }
        public bool IsAuthenticated { get; }
    }
}
