using System.Collections.Generic;
using System.Data;
using ACDB.DynamicSql.DAL.Context;
using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Models;
using ACDB.DynamicSql.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ACDB.DynamicSql.DAL.Models.Games;
using Dapper;

namespace ACDB.DynamicSql.DAL.Repositories
{
    public class GameRepository : BaseRepository<int, Game, GameContext>, IGameRepository
    {
        public GameRepository(GameContext context) : base(context)
        {
        }

        public async Task<CollectionResult<Game>> GetGamesAsync(FilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<Game> query = DbContext.Games.Include(g => g.Category).Include(g => g.Company);

            if (!string.IsNullOrWhiteSpace(filterParams.Term))
            {
                query = query.Where(g => EF.Functions.Like(g.Name, $"%{filterParams.Term}%") ||
                                         EF.Functions.Like(g.Description, $"%{filterParams.Term}%"));
            }

            query = query.Skip((filterParams.PageNumber - 1) * filterParams.PageSize).Take(filterParams.PageSize);

            CollectionResult<Game> result = new CollectionResult<Game>
            {
                TotalCount = await query.CountAsync(ct),
                Collection = await query.ToListAsync(ct)
            };

            return result;
        }

        public CollectionResult<GameModel> GetGamesUsingStoredProcedure(FilterParams filterParams)
        {
            CollectionResult<GameModel> result = new CollectionResult<GameModel>();

            var parameters = new DynamicParameters();

            parameters.Add("pageSize", dbType: DbType.Int32, value: filterParams.PageSize, direction: ParameterDirection.Input);
            parameters.Add("pageNumber", dbType: DbType.Int32, value: filterParams.PageNumber, direction: ParameterDirection.Input);
            parameters.Add("term", dbType: DbType.String, value: filterParams.Term, direction: ParameterDirection.Input);

            using (var reader = DbContext.Database.GetDbConnection().QueryMultiple("[dbo].[GetGames]", parameters, commandType: CommandType.StoredProcedure))
            {
                result.TotalCount = reader.ReadFirstOrDefault<int>();

                result.Collection = reader.Read<GameModel>().ToList();
            }

            return result;
        }

        public override async Task<Game> GetAsync(int id, CancellationToken ct = default)
        {
            return await DbContext.Games
                .Include(g => g.Category).Include(g => g.Company)
                .FirstOrDefaultAsync(g => g.Id == id, ct);
        }
    }
}
