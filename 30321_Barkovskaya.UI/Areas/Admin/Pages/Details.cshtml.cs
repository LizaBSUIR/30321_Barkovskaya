using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _30321_Barkovskaya_API.Data;
using _30321_BarkovskayaDomain.Entities;

namespace _30321_Barkovskaya.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly _30321_Barkovskaya_API.Data.AppDbContext _context;

        public DetailsModel(_30321_Barkovskaya_API.Data.AppDbContext context)
        {
            _context = context;
        }

        public Dish Dish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                Dish = dish;
            }
            return Page();
        }
    }
}