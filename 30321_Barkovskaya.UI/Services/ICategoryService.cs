using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;
using _30321_Barkovskaya.UI;

namespace _30321_Barkovskaya.UI.Services
{
	public interface ICategoryService
	{
		/// <summary>
		/// Получение списка всех категорий
		/// </summary>
		/// <returns></returns>
		public Task<ResponseData<List<Category>>> GetCategoryListAsync();

	}
}
