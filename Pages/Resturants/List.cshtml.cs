using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;
using OdeToFood.Data.Core;
using OdeToFood.Data.Presistence;

namespace OdeToFood.Pages.Resturants
{
    public class ListModel : PageModel
    {
		private readonly IConfiguration config;
		private readonly IResturantData resturantData;
		public ListModel(IConfiguration configuration
			,IResturantData resturantData)
		{
			this.config = configuration;
			this.resturantData = resturantData;
		}
		public IEnumerable<Resturant> resturants { get; set; }
		public string Message { get; set; }
		public string MessageFromConfig { get; set; }
		[BindProperty(SupportsGet =true)]
		public string SearchTerm { get; set; }

		public void OnGet()
        {
			MessageFromConfig = config["Message"];
			Message = "Hello,world using asp core!";
			resturants = resturantData.GetResturantsByName(SearchTerm);

		}
    }
}