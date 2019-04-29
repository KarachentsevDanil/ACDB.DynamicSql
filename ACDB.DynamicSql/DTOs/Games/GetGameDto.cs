using ACDB.DynamicSql.DTOs.Categories;
using ACDB.DynamicSql.DTOs.Companies;

namespace ACDB.DynamicSql.DTOs.Games
{
    public class GetGameDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public GetCategoryDto Category { get; set; }

        public GetCompanyDto Company { get; set; }
    }
}