using _30321_BarkovskayaDomain.Entities;
using _30321_BarkovskayaDomain.Models;
using System.Collections.ObjectModel;
using static System.Net.WebRequestMethods;

namespace Blazor30321.Services
{
    public class ApiDishService(HttpClient httpClient) : IDishService<Dish>
    {
        public event Action ListChanged;
        private List<Dish> _dishes;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Dish> Dishes => _dishes;

        public int CurrentPage => _currentPage;

        public int TotalPages => _totalPages;

        public async Task GetDishes(int pageNo = 1, int pageSize = 6)
        {
            // Url сервиса API
            var uri = httpClient.BaseAddress.AbsoluteUri;
            // данные для Query запроса
            var queryData = new Dictionary<string, string>
            {
                { "pageNo", pageNo.ToString() },
                {"pageSize", pageSize.ToString() }
            };

            var query = QueryString.Create(queryData);
            // Отправить запрос http
            var result = await httpClient.GetAsync(uri + query.Value);
            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content.ReadFromJsonAsync<ResponseData<ListModel<Dish>>>();

                // обновить параметры
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
				_dishes = responseData.Data.Items;
                ListChanged?.Invoke();
            }
            else // В случае ошибки
            {
				_dishes = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}
