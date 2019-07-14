using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdeToFood.Data;
using OdeToFood.Data.Core;
using OdeToFood.Data.Presistence;

namespace OdeToFood
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<OdeToFoodContext>(option =>
			{
				option
				.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
			});

			//addSingleton is very bad in this situaton so we need another solution so add scoped is collect all changes and push it to the db in one request.
			services.AddScoped<IResturantData, ResturantData>();
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseNodeModules(env);
			app.UseCookiePolicy();
			app.UseMvc();
		}
	}
}
