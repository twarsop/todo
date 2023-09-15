using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using ToDo.Mvc.WebUI.Enums;
using ToDo.Mvc.WebUI.Models;
using ToDo.Shared.Dtos;

namespace ToDo.Mvc.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private const string BaseApiUrl = "https://localhost:7180/api/v1/todoitems";
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private async Task<HttpResponseMessage> RetryApiEndpoint(HttpMethodEnum httpMethodEnum, string url, string jsonString = null)
    {
        for (int i = 0; i < 10; i++)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var content = jsonString == null
                        ? null
                        : new StringContent(jsonString, Encoding.UTF8, "application/json");

                    switch (httpMethodEnum)
                    {
                        case HttpMethodEnum.Get:
                            return await client.GetAsync(url);
                        case HttpMethodEnum.Post:
                            return await client.PostAsync(url, content);
                        case HttpMethodEnum.Put:
                            return await client.PutAsync(url, content);
                        case HttpMethodEnum.Delete:
                            return await client.DeleteAsync(url);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        return null;
    }

    public async Task<IActionResult> Index()
    {
        List<ToDoItemDto> toDoItemsDtos = null;

        var getAllResponse = await RetryApiEndpoint(HttpMethodEnum.Get, BaseApiUrl);

        if (getAllResponse != null && getAllResponse.IsSuccessStatusCode)
        {
            var data = await getAllResponse.Content.ReadAsStringAsync();
            toDoItemsDtos = JsonConvert.DeserializeObject<List<ToDoItemDto>>(data);
        }

        return View(toDoItemsDtos?
            .OrderBy(x => x.CreatedAt)
            .Select(x => _mapper.Map<ToDoItemViewModel>(x)).ToList());
    }

    public IActionResult AddToDoItem()
    {
        return View("AddToDoItem", new ToDoItemViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> AddToDoItemSubmit(ToDoItemViewModel toDoItemViewModel)
    {
        var putResponse = await RetryApiEndpoint(
            HttpMethodEnum.Post,
            BaseApiUrl,
            JsonConvert.SerializeObject(new ToDoItemForCreationDto(toDoItemViewModel.Description)));

        return Redirect("Index");
    }

    public async Task<IActionResult> EditToDoItem(Guid id)
    {
        var getResponse = await RetryApiEndpoint(HttpMethodEnum.Get, $"{BaseApiUrl}/{id}");

        if (getResponse != null && getResponse.IsSuccessStatusCode)
        {
            var data = await getResponse.Content.ReadAsStringAsync();
            ToDoItemDto toDoItemDto = JsonConvert.DeserializeObject<ToDoItemDto>(data);
            return View("EditToDoItem", _mapper.Map<ToDoItemViewModel>(toDoItemDto));
        }

        return View("EditToDoItem", new ToDoItemViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> EditToDoItemSubmit(ToDoItemViewModel toDoItemViewModel)
    {
        var putResponse = await RetryApiEndpoint(
            HttpMethodEnum.Put,
            $"{BaseApiUrl}/{toDoItemViewModel.Id}",
            JsonConvert.SerializeObject(new ToDoItemForUpdateDto(toDoItemViewModel.Description)));

        return Redirect("Index");
    }

    public async Task<IActionResult> DeleteToDoItem(Guid id)
    {
        var deleteResponse = await RetryApiEndpoint(HttpMethodEnum.Delete, $"{BaseApiUrl}/{id}");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async void CompleteToDoItem(string id)
    {
        await RetryApiEndpoint(HttpMethodEnum.Put, $"{BaseApiUrl}/complete/{id}");
    }

    [HttpPost]
    public async void UnCompleteToDoItem(string id)
    {
        await RetryApiEndpoint(HttpMethodEnum.Put, $"{BaseApiUrl}/uncomplete/{id}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}