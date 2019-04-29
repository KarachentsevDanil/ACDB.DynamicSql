using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Repositories.Contracts;
using ACDB.DynamicSql.DTOs.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ACDB.DynamicSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {
            IEnumerable<Category> items = await _categoryRepository.GetListAsync();
            return Ok(_mapper.Map<IEnumerable<GetCategoryDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Category item = await _categoryRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetCategoryDto>(item));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto item)
        {
            Category entity = _mapper.Map<Category>(item);

            _categoryRepository.Create(entity);
            await _categoryRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetCategoryDto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryDto item)
        {
            item.Id = id;
            Category entity = _mapper.Map<Category>(item);

            _categoryRepository.Update(entity);
            await _categoryRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetCategoryDto>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Category item = await _categoryRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(item);
            await _categoryRepository.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}