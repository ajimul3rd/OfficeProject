namespace OfficeProject.Servicess
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
