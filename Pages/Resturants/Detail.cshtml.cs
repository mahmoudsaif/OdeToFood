using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Data.Core;
using OdeToFood.Data.Presistence;

namespace OdeToFood.Pages.Resturants
{
	public class DetailModel : PageModel
	{
		private readonly IResturantData resturantData;

		public DetailModel(IResturantData resturantData)
		{
			this.resturantData = resturantData;
		}
		[TempData]
		public string Message { set; get; }
		public Resturant Resturant { get; set; }
		public IActionResult OnGet(int resturantId)
		{
			Resturant = resturantData.GetById(resturantId);
			if (Resturant == null)
			{
				return RedirectToPage("./NotFound");
			}
			return Page();
		}
	}
}