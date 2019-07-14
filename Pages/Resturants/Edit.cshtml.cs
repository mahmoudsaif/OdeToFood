using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data.Core;

namespace OdeToFood.Pages.Resturants
{
	public class EditModel : PageModel
	{
		private readonly IResturantData resturantData;
		private readonly IHtmlHelper htmlHelper;

		public EditModel(IResturantData resturantData, IHtmlHelper htmlHelper)
		{
			this.resturantData = resturantData;
			this.htmlHelper = htmlHelper;
		}
		[BindProperty]
		public Resturant Resturant { set; get; }
		public IEnumerable<SelectListItem> Cuisines { get; set; }
		public IActionResult OnGet(int? resturantId
			)
		{
			Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
			if (resturantId.HasValue)
			{

				Resturant = resturantData.GetById(resturantId.Value);
			}
			else
			{
				Resturant = new Resturant();
			}
			if (Resturant == null)
			{

				RedirectToPage("./NotFound");
			}
			return Page();
		}
		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
				return Page();
			}
			if (Resturant.Id > 0)
			{
				Resturant = resturantData.Update(Resturant);
			}
			else
			{
				Resturant = resturantData.Add(Resturant);
			}
			resturantData.Commit();
			TempData["Message"] = "Item Saved Successfuly";
			return RedirectToPage("./Detail", new { resturantId = Resturant.Id });
		}
	}
}