using AgroPower.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AgroPower.UILayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7009/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductReadDto>>("api/Product");
            return View(response);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Something went wrong");
            return View(dto);
        }
    }

}
