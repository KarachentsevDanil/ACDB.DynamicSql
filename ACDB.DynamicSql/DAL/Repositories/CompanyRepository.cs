using ACDB.DynamicSql.DAL.Context;
using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Repositories.Contracts;

namespace ACDB.DynamicSql.DAL.Repositories
{
    public class CompanyRepository : BaseRepository<int, Company, GameContext>, ICompanyRepository
    {
        public CompanyRepository(GameContext context) : base(context)
        {
        }
    }
}