using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.CustomActionFilters;
using TaskManagerAPI.Models.DTO;
using TaskManagerAPI.Repositories.Interface;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListRepository listRepository;
        private readonly IMapper mapper;

        public ListController(IListRepository listRepository, IMapper mapper)
        {
            this.listRepository = listRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listsDomain = await listRepository.GetAllAsync();
            var listsDto = mapper.Map<List<ListDto>>(listsDomain);
            return Ok(listsDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddListRequestDto addListRequestDto)
        {
            var listDomain = mapper.Map<Models.Domain.List>(addListRequestDto);
            
            listDomain = await listRepository.CreateAsync(listDomain);

            var listDto = mapper.Map<ListDto>(listDomain);

            return CreatedAtAction(nameof(Create), new { id = listDto.Id }, listDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var listDomain = await listRepository.DeleteAsync(id);

            if (listDomain == null)
            {
                return NotFound();
            }

            var listDto = mapper.Map<ListDto>(listDomain);

            return Ok(listDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateListRequestDto updateListRequestDto)
        {
            var listDomain = mapper.Map<Models.Domain.List>(updateListRequestDto);

            listDomain = await listRepository.UpdateAsync(id, listDomain);

            if(listDomain == null)
            {
                return NotFound();
            }

            var listDto = mapper.Map<ListDto>(listDomain);

            return Ok(listDto);
        }
    }
}
