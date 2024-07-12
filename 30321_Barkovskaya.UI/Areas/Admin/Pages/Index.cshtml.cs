using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _30321_Barkovskaya_API.Data;
using _30321_BarkovskayaDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using _30321_Barkovskaya.UI.Services;

namespace _30321_Barkovskaya.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Dish> Dish { get; set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync(int pageNo = 1)
        {
            var response = await _productService.GetProductListAsync(null, pageNo);
            if (response.Success)
            {
                Dish = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}