﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class ContactController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // Bir istemci yarat 
            var responseMessage = await client.GetAsync("https://localhost:7194/api/Contact"); //Tetiklenecek yer 

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string formatında oku 
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData); // Burada okunan değeri Json Formatından deserilaze ederek normal metne döndürür.

                return View(values);

            }

            return View();
        }

        [HttpGet]

        public IActionResult CreateContact()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // 1.parametre gelen data , 2. parametre türkçe karakter var ise onuda kullan,3. parametre medianın türü
            var responseMessage = await client.PostAsync("https://localhost:7194/api/Contact", stringContent); // API'ye git ve giderken olusturdugumuz kaynağı götür.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }


        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7194/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> UpdateContact(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7194/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateContactDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7194/api/Contact", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }

            return View();
        }
    }
}
