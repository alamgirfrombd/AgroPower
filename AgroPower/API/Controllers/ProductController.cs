using AgroPower.Application.Interfaces;
using AgroPower.Application.Services;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgroPower.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _service = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Duplicate name check
            var existingProduct = await _service.GetByNameAsync(dto.Name);
            if (existingProduct != null)
            {
                ModelState.AddModelError("Name", "Product with this name already exists.");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(dto); // AutoMapper replaces manual mapping


            await _service.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Product product)
        {
            if (id != product.Id) return BadRequest();
            await _service.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
