using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.UILayotComponents
{
    public class _UILayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
