using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;

namespace _30321_Barkovskaya.UI.Services
{
    public class ApiCategoryService(HttpClient httpClient) : ICategoryService
    {
        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Category>>>();
            };

            var response = new ResponseData<List<Category>>
            {
                Success = false,
                ErrorMessage = "Ошибка чтения API"
            };

            return response;
        }
    }
}
