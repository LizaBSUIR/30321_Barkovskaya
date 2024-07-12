namespace Blazor30321.Services
{
    public interface IDishService<T> where T : class
    {
        event Action ListChanged;

        // Список объектов
        IEnumerable<T> Dish { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }
        // Получение списка объектов
        Task GetDish(int pageNo = 1, int pageSize = 6);
    }
}
