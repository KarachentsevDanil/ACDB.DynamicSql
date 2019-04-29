using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Models;
using ACDB.DynamicSql.DAL.Models.Games;
using System.Threading;
using System.Threading.Tasks;

namespace ACDB.DynamicSql.DAL.Repositories.Contracts
{
    public interface IGameRepository : IBaseRepository<int, Game>
    {
        Task<CollectionResult<Game>> GetGamesAsync(FilterParams filterParams, CancellationToken ct = default);

        CollectionResult<GameModel> GetGamesUsingStoredProcedure(FilterParams filterParams);
    }
}