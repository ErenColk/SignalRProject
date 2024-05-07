using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.MenuTableDto;
using SignalRWebUI.Dtos.MessageDto;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class DefaultController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public PartialViewResult SendMessage()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // 1.parametre gelen data , 2. parametre türkçe karakter var ise onuda kullan,3. parametre medianın türü
            var responseMessage = await client.PostAsync("https://localhost:7194/api/Message", stringContent); // API'ye git ve giderken olusturdugumuz kaynağı götür.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
