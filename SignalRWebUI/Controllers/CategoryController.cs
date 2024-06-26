﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient(); // Bir istemci yarat 
            var responseMessage = await client.GetAsync("https://localhost:7194/api/Category"); //Tetiklenecek yer 

            if(responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Jsondan gelen içeriği string formatında oku 
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // Burada okunan değeri Json Formatından deserilaze ederek normal metne döndürür.

                return View(values);

            }

            return View();
        }

        [HttpGet]
        
        public IActionResult CreateCategory()
        {


            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            createCategoryDto.Status = true;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json"); // 1.parametre gelen data , 2. parametre türkçe karakter var ise onuda kullan,3. parametre medianın türü
            var responseMessage = await client.PostAsync("https://localhost:7194/api/Category", stringContent); // API'ye git ve giderken olusturdugumuz kaynağı götür.
            if( responseMessage.IsSuccessStatusCode )
            {
                return RedirectToAction("Index");

            }
            return View();
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7194/api/Category/{id}");

            if( responseMessage.IsSuccessStatusCode )
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7194/api/Category/{id}");
            
            if ( responseMessage.IsSuccessStatusCode )
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7194/api/Category",stringContent);

            if( responseMessage.IsSuccessStatusCode )
            {
                return RedirectToAction("Index");

            }

            return View();
        }


    }
}
