using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TranslateApp.DTOs;
using TranslateApp.Models;

namespace translateApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly List<string> mostUsedLanguages = new List<string>()
    {
        "English",
        "Mandarin Chinese",
        "Spanish",
        "Hindi",
        "Portuguese",
        "Russian",
        "Japanese",
        "Turkish",
        "French",
        "Italian",
        "German",
        "Albanian",
        "Serbian",
        "Arabic",
        "Georgian",
        "Korean",
        "Chech"
    };

    public HomeController(ILogger<HomeController> logger, IConfiguration config, HttpClient httpClient)
    {
        _logger = logger;
        _config = config;
        _httpClient = httpClient;

    }

    public IActionResult Index()
    {
        ViewBag.Languages = new SelectList(mostUsedLanguages);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetGPTResponse(string query, string selectedLanguage)
    {
        var openAPIKey = _config["OpenAI:ApiKey"];

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAPIKey}");

        var payload = new
        {
            model = "gpt-4o-mini",
            messages = new object[]
            {
                new {role = "system", content = $"Translate to {selectedLanguage}"},
                new {role = "user", content = query}

            },
            temperature = 0,
            max_tokens = 256
        };
        string jsonPayload = JsonConvert.SerializeObject(payload);
        HttpContent httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var responseMessage = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions",
            httpContent);
        var responseMessageJson = await responseMessage.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseMessageJson);

        ViewBag.Result = response.Choices[0].Message.Content;
        ViewBag.Languages = new SelectList(mostUsedLanguages);

        return View("Index");

    }
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
