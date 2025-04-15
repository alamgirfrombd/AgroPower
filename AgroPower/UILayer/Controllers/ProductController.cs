using AgroPower.Application.Interfaces; // অ্যাপ্লিকেশনের সার্ভিস ইন্টারফেস ব্যবহার করার জন্য
using AgroPower.Domain.Entities; // ডোমেইন এনটিটি মডেল ব্যবহার করার জন্য
using AgroPower.DTOs; // ডাটা ট্রান্সফার অবজেক্ট (DTO) এর জন্য
using AutoMapper; // অটো-ম্যাপার লাইব্রেরি ব্যবহার করে ডাটা মডেল ম্যাপ করার জন্য
using Microsoft.AspNetCore.Mvc; // এমভিসি কন্ট্রোলার বেস ক্লাসের জন্য
using Microsoft.AspNetCore.Mvc.Rendering; // Dropdown এর জন্য SelectListItem ব্যবহার করার জন্য

namespace AgroPower.UILayer.Controllers // কন্ট্রোলারের নেমস্পেস
{
    public class ProductController : Controller // প্রোডাক্ট সম্পর্কিত ফাংশনালিটি হ্যান্ডেল করতে কন্ট্রোলার
    {
        private readonly IProductService _service; // প্রোডাক্ট সার্ভিস ইন্টারফেসের ডিপেন্ডেন্সি ইনজেকশন
        private readonly IProductCategoryService _categoryService; // ক্যাটেগরি সার্ভিস ইন্টারফেসের ডিপেন্ডেন্সি ইনজেকশন
        private readonly IMapper _mapper; // অটো-ম্যাপিংয়ের জন্য Mapper

        // কন্সট্রাক্টর: প্রয়োজনীয় সার্ভিসগুলো ইঞ্জেক্ট করা হয়
        public ProductController(IProductService service, IProductCategoryService categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // প্রোডাক্ট লিস্ট দেখানোর জন্য
        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllWithCategoryAsync(); // ক্যাটেগরি সহ ডেটা আনো
            var dtoList = _mapper.Map<IEnumerable<ProductReadDto>>(products);
            return View(dtoList);
        }



        // নতুন প্রোডাক্ট যোগ করার জন্য
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync(); // ড্রপডাউন ক্যাটেগরির জন্য ক্যাটেগরি লোড
            return View(); // ভিউ রিটার্ন
        }

        [HttpPost] // ফর্ম ডাটা সাবমিট হ্যান্ডেল করার জন্য
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid) // ভ্যালিডেশন চেক করা
            {
                await LoadCategoriesAsync(); // ক্যাটেগরি আবার লোড করা
                return View(dto); // ইরর মেসেজসহ ভিউতে ফেরত পাঠানো
            }

            var existing = await _service.GetByNameAsync(dto.Name); // প্রোডাক্ট আগে থেকে আছে কি না চেক
            if (existing != null)
            {
                ModelState.AddModelError("Name", "This product already exists."); // ইরর মেসেজ
                await LoadCategoriesAsync(); // ক্যাটেগরি আবার লোড করা
                return View(dto); // ভিউতে ফেরত পাঠানো
            }

            var product = _mapper.Map<Product>(dto); // DTO থেকে এনটিটি ম্যাপ করা
            await _service.AddAsync(product); // নতুন প্রোডাক্ট যুক্ত করা
            return RedirectToAction("Index"); // প্রোডাক্ট লিস্ট পেজে রিডাইরেক্ট
        }

        // ক্যাটেগরি লোড করার জন্য হেল্পার মেথড
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync(); // ক্যাটেগরি তালিকা পাওয়া
            ViewBag.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(), // ক্যাটেগরি আইডি
                    Text = c.Name // ক্যাটেগরি নাম
                })
                .ToList(); // SelectListItem লিস্টে কনভার্ট করা
        }

        // প্রোডাক্ট আপডেট করার জন্য
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _service.GetByIdAsync(id); // প্রোডাক্ট আইডি দিয়ে প্রোডাক্ট পাওয়া
            if (product == null) return NotFound(); // প্রোডাক্ট না পাওয়া গেলে 404 রিটার্ন
            var dto = _mapper.Map<ProductUpdateDto>(product); // প্রোডাক্টকে DTO তে ম্যাপ করা
            await LoadCategoriesAsync(); // ক্যাটেগরি লোড করা
            return View(dto); // ভিউতে DTO পাঠানো
        }


        [HttpPost] // ফর্ম ডাটা সাবমিট হ্যান্ডেল করার জন্য
        public async Task<IActionResult> Edit(ProductUpdateDto dto)
        {
            if (!ModelState.IsValid) // ভ্যালিডেশন চেক করা
            {
                await LoadCategoriesAsync(); // ক্যাটেগরি আবার লোড করা
                return View(dto); // ইরর মেসেজসহ ভিউতে ফেরত পাঠানো
            }
            var existing = await _service.GetByNameAsync(dto.Name); // প্রোডাক্ট আগে থেকে আছে কি না চেক
            if (existing != null && existing.Id != dto.Id)
            {
                ModelState.AddModelError("Name", "This product already exists."); // ইরর মেসেজ
                await LoadCategoriesAsync(); // ক্যাটেগরি আবার লোড করা
                return View(dto); // ভিউতে ফেরত পাঠানো
            }
            // এখানেই AutoMapper দিয়ে ট্র্যাকড অবজেক্টকে আপডেট করো
            _mapper.Map(dto, existing); // ❗ dto থেকে existing object-এর ভেতরে ডেটা বসাও

            await _service.UpdateAsync(existing); // এখন ট্র্যাকড অবজেক্ট দিয়ে আপডেট করো

            return RedirectToAction("Index");
        }
    }
}
