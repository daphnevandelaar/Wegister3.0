﻿namespace Application.Common.Viewmodels
{
    public class PaginationVm
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Take { get; set; } = 100;
        public int Skip { get; set; }
        public int Total { get; set; }
    }
}
