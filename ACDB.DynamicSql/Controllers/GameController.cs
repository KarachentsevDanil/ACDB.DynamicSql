using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Models;
using ACDB.DynamicSql.DAL.Models.Games;
using ACDB.DynamicSql.DAL.Repositories.Contracts;
using ACDB.DynamicSql.DTOs.Games;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ACDB.DynamicSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet("ef")]
        public async Task<IActionResult> GetItemsAsync([FromQuery] FilterParams filterParams)
        {
            CollectionResult<Game> items = await _gameRepository.GetGamesAsync(filterParams);
            return Ok(_mapper.Map<CollectionResult<GetGameDto>>(items));
        }

        [HttpGet("sp")]
        public IActionResult GetItemsUsingStoredProcedures([FromQuery] FilterParams filterParams)
        {
            CollectionResult<GameModel> items = _gameRepository.GetGamesUsingStoredProcedure(filterParams);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Game item = await _gameRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetGameDto>(item));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameDto item)
        {
            Game entity = _mapper.Map<Game>(item);

            _gameRepository.Create(entity);
            await _gameRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetGameDto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateGameDto item)
        {
            item.Id = id;
            Game entity = _mapper.Map<Game>(item);

            _gameRepository.Update(entity);
            await _gameRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetGameDto>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Game item = await _gameRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _gameRepository.Delete(item);
            await _gameRepository.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}