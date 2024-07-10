using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;


namespace _30321_Barkovskaya.UI.Services
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>>GetCategoryListAsync()
		{
			var categories = new List<Category>
			{
			 new Category {Id=1, Name="Стартеры", NormalizedName="starters"},
			 new Category {Id=2, Name="Салаты", NormalizedName="salads"},
			 new Category {Id=3, Name="Супы", NormalizedName="soups"},
			 new Category {Id=4, Name="Основное", NormalizedName="main"}
			};
			var result = new ResponseData<List<Category>>();
			result.Data = categories;
			return Task.FromResult(result);
		}
	}

}
