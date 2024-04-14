using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalR.DtoLayer.SliderDtos;
using SignalRWebUI.Dtos.AboutDtos;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class _DefaultAboutComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultAboutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var client = _httpClientFactory.CreateClient(); // Bir istemci yarat 
            var responseMessage = await client.GetAsync("https://localhost:7194/api/About"); //Tetiklenecek yer 
            var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string formatında oku 
            var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData); // Burada okunan değeri Json Formatından deserilaze ederek normal metne döndürür.
            return View(values);


        }
    }
}
