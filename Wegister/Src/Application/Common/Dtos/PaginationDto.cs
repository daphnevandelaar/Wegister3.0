namespace Application.Common.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Take { get; set; } = 100;
        public int Skip { get; set; }
        public int Total { get; set; }
    }
}
