using Kaira.WebUI.DTOs.ProductDtos;
using Kaira.WebUI.Repositories.CategoryRepositories;
using Kaira.WebUI.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Kaira.WebUI.Controllers
{
    public class ProductController(IProductRepository _productRepository,ICategoryRepository _categoryRepository) : Controller
    {
        private async Task GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = (from category in categories
                                  select new SelectListItem
                                  {
                                      Text = category.Name,
                                      Value = category.CategoryId.ToString()
                                  }).ToList();
        }
        public async Task<IActionResult> Index()
        {
            var values = await _productRepository.GetAllAsync();
            return View(values);
        }

        public async Task<IActionResult> Create()
        {
            await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            
            await _productRepository.CreateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            await GetCategoriesAsync();
            var value = await _productRepository.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                GetCategoriesAsync();
                return View(dto);
            }
            await _productRepository.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
