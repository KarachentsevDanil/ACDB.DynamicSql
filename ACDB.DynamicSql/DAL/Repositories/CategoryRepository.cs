using ACDB.DynamicSql.DAL.Context;
using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Repositories.Contracts;

namespace ACDB.DynamicSql.DAL.Repositories
{
    public class CategoryRepository : BaseRepository<int, Category, GameContext>, ICategoryRepository
    {
        public CategoryRepository(GameContext context) : base(context)
        {
        }
    }
}