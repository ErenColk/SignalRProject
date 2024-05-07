using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SocialMediaDtos;

namespace SignalRWebUI.ViewComponents.UILayotComponents
{
    public class _UILayoutSocialMediaComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public _UILayoutSocialMediaComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient(); // Bir istemci yarat 
            var responseMessage = await client.GetAsync("https://localhost:7194/api/SocialMedia"); //Tetiklenecek yer 

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string formatında oku 
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData); // Burada okunan değeri Json Formatından deserilaze ederek normal metne döndürür.

                return View(values);

            }

            return View();
        }





    }
}
