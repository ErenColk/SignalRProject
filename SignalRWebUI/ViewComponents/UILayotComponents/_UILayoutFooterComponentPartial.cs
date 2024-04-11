using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UILayotComponents
{
    public class _UILayoutFooterComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
