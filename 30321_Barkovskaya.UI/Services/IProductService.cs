using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;

namespace _30321_Barkovskaya.UI.Services
{
	public interface IProductService
	{
		 public Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string?
categoryNormalizedName, int pageNo = 1);
		public Task<ResponseData<Dish>> GetProductByIdAsync(int id);
		public Task UpdateProductAsync(int id, Dish product, IFormFile? formFile);
		public Task DeleteProductAsync(int id);
		public Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile?
	   formFile);
	}

}
