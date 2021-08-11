namespace Application.Common.Models
{
    public class CurrentUser
    {
        public string UserId { get; }
        public string CompanyId { get; }

        public CurrentUser(string userId, string companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }
    }
}
