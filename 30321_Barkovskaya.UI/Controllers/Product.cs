using _30321_Barkovskaya.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace _30321_Barkovskaya.UI.Controllers
{
	public class Product : Controller
	{
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public Product(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;

        }
        [Route("Products/{category?}")]
        public async Task<IActionResult> Index(string? category, int pageNo=1)
        {
            var categoriesRespnonse = await _categoryService.GetCategoryListAsync();
            if (!categoriesRespnonse.Success) return NotFound(categoriesRespnonse.ErrorMessage);
            ViewData["categories"] = categoriesRespnonse.Data;

            var currentCategory = category == null ?
                "Все" :
                categoriesRespnonse.Data.FirstOrDefault(b => b.NormalizedName == category)?.Name;
            ViewData["currentCategory"] = currentCategory;

            var productResponse = await _productService.GetProductListAsync(category);
            if (!productResponse.Success) return NotFound(productResponse.ErrorMessage);

            return View(productResponse.Data.Items);
        }
    }
}
