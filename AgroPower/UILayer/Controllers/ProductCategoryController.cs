using AgroPower.Application.Interfaces;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AgroPower.UILayer.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _service;
        private readonly IMapper _mapper;

        public ProductCategoryController(IProductCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<ProductCategoryReadDto>>(categories);
            return View(dtoList); // will look for Views/ProductCategory/Index.cshtml
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); // will look for Views/ProductCategory/Create.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var existing = await _service.GetByNameAsync(dto.Name);
            if (existing != null)
            {
                ModelState.AddModelError("Name", "This category already exists.");
                return View(dto);
            }

            var category = _mapper.Map<ProductCategory>(dto);
            await _service.AddAsync(category);
            return RedirectToAction("Index");
        }
    }
}
