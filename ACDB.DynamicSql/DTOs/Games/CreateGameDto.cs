namespace ACDB.DynamicSql.DTOs.Games
{
    public class CreateGameDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int CompanyId { get; set; }
    }
}