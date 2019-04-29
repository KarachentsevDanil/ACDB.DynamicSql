using ACDB.DynamicSql.DAL.Entities;
using ACDB.DynamicSql.DAL.Repositories.Contracts;
using ACDB.DynamicSql.DTOs.Companies;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ACDB.DynamicSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {
            IEnumerable<Company> items = await _companyRepository.GetListAsync();
            return Ok(_mapper.Map<IEnumerable<GetCompanyDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Company item = await _companyRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetCompanyDto>(item));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyDto item)
        {
            Company entity = _mapper.Map<Company>(item);

            _companyRepository.Create(entity);
            await _companyRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetCompanyDto>(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCompanyDto item)
        {
            item.Id = id;
            Company entity = _mapper.Map<Company>(item);

            _companyRepository.Update(entity);
            await _companyRepository.SaveChangesAsync();

            return Ok(_mapper.Map<GetCompanyDto>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Company item = await _companyRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _companyRepository.Delete(item);
            await _companyRepository.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}