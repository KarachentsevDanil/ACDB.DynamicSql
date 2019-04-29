namespace ACDB.DynamicSql.DAL.Models
{
    public class FilterParams
    {
        public int PageSize { get; set; } = 25;

        public int PageNumber { get; set; } = 1;

        public string Term { get; set; }
    }
}