namespace DownHillParkAPI.RequestModels
{
    public class PageRequest
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public SortOptions SortOption { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}
