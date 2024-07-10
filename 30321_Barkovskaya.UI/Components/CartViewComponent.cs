using Microsoft.AspNetCore.Mvc;

namespace _30321_Barkovskaya.UI.Components
{
    public class CartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
