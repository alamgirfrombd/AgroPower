using AgroPower.Application.Interfaces;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroPower.UILayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IProductCategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IProductCategoryService categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<ProductReadDto>>(products);
            return View(dtoList);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(dto);
            }

            var existing = await _service.GetByNameAsync(dto.Name);
            if (existing != null)
            {
                ModelState.AddModelError("Name", "This product already exists.");
                await LoadCategoriesAsync();
                return View(dto);
            }

            var product = _mapper.Map<Product>(dto);
            await _service.AddAsync(product);
            return RedirectToAction("Index");
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
    }
}
