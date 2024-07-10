using _30321_Barkovskaya.UI.Services;
using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;
using System.Threading.Channels;

namespace _30321_Barkovskaya.UI.Services
{
	public class MemoryProductService : IProductService
	{
		List<Dish> _dishes;
		List<Category> _categories;
		public MemoryProductService(ICategoryService categoryService)
		{
			_categories = categoryService.GetCategoryListAsync().Result.Data;
			SetupData();
		}
		/// <summary>
		/// Инициализация списков
		/// </summary>
		private void SetupData()
		{
			_dishes = new List<Dish>
			{
                new Dish {Id = 1, Name="Канапе", Description="Закуска из томатов и сыра", Image="images/канапе.jpg",
                CategoryId=1},
                new Dish {Id = 2, Name="Цезарь", Description="Овощи, сыр, яйцо, сухарики", Image="images/цезарь.jpg",
                CategoryId=2},
                new Dish {Id = 3, Name="Суп-харчо", Description="Очень острый, мясной", Image="images/харчо.jpg",
				CategoryId=3},
				new Dish { Id = 4, Name="Борщ",	Description="Свекла, капуста, мясо, без сметаны", Image="images/борщ.jpg",
				CategoryId=3},
                new Dish { Id = 5, Name="Томагавк", Description="Мясо на кости, овощи", Image="images/томагавк.jpg",
                CategoryId=4},
            };
		}
       
		public Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

		public Task DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<Dish>> GetProductByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
		{
			throw new NotImplementedException();
		}

		public Task UpdateProductAsync(int id, Dish product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}

        public Task<ResponseData<ListModel<Dish>>> GetListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var result = new ResponseData<ListModel<Dish>>();
            int? categoryId = null;

            if (categoryNormalizedName != null)
            {
                categoryId = _categories.Find(b =>
                b.NormalizedName.Equals(categoryNormalizedName))
                ?.Id;
            }

            var data = _dishes.Where(p => categoryId == null ||
                p.CategoryId.Equals(categoryId))
                ?.ToList();

            result.Data = new ListModel<Dish>()
            {
                Items = data
            };

            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }

            return Task.FromResult(result);
        }

    }
}
