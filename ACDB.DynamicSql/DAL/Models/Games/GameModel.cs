namespace ACDB.DynamicSql.DAL.Models.Games
{
    public class GameModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyDescription { get; set; }
    }
}