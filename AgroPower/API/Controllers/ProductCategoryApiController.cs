using AgroPower.Application.Interfaces;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgroPower.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryApiController : ControllerBase
    {
        private readonly IProductCategoryService _service;
        private readonly IMapper _mapper;
        public ProductCategoryApiController(IProductCategoryService productService, IMapper mapper)
        {
            _service = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            var readDtos = _mapper.Map<IEnumerable<ProductCategoryReadDto>>(categories);
            return Ok(readDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryCreateDto dto)
        {
            //Duplicate name check
            var existingProduct = await _service.GetByNameAsync(dto.Name);
            if (existingProduct != null)
            {
                ModelState.AddModelError("Name", "Product with this name already exists.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // AutoMapper replaces manual mapping
            var productCategory = _mapper.Map<ProductCategory>(dto); // AutoMapper replaces manual mapping



            await _service.AddAsync(productCategory);
            return CreatedAtAction(nameof(GetById), new { id = productCategory.Id }, productCategory);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _service.UpdateAsync(existing);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
