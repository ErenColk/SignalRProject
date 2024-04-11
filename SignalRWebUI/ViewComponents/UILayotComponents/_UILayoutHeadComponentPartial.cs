using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UILayotComponents
{
    public class _UILayoutHeadComponentPartial :  ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
