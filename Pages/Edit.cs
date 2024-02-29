using System.Text.Json;
using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitWeb.Pages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
	private readonly HttpClient _httpClient;
	[BindProperty] public Fruit FruitModel { get; set;}
	public List<SelectListItem> Options { get; set; }

    public EditModel(ILogger<EditModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
		_httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGet(int id)
    {
			FruitModel = await _httpClient.GetFromJsonAsync<Fruit>(id.ToString());
			IEnumerable<Color> colors = await _httpClient.GetFromJsonAsync<IEnumerable<Color>>("color");
			Options = colors?.Select(a => new SelectListItem { Value = a.Id.ToString(), 
				Text =  a.Name, 
				Selected = FruitModel?.ColorId == a.Id }).ToList();
    }

    public async Task<IActionResult> OnPost()
	 {
			var response = await _httpClient.PutAsJsonAsync<Fruit>(FruitModel.Id.ToString(), FruitModel);

			 if (response.IsSuccessStatusCode)
			 {
					 TempData["success"] = "Data was edited successfully.";
					 return RedirectToPage("Index");
			 }
			 else
			 {
					 TempData["failure"] = "Operation was not successful";
					 return RedirectToPage("Index");
			 }
	
	 }
}
