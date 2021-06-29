using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Ffitness.Data;
using Ffitness.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using FluentValidation.AspNetCore;
using Ffitness.ViewModels;
using FluentValidation;
using Ffitness.Validator;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ffitness.Services;

namespace Ffitness
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
			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
							Configuration.GetConnectionString("DefaultConnection")));

			services.AddDatabaseDeveloperPageExceptionFilter();
			
			services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
					.AddRoles<IdentityRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();

			services.AddIdentityServer()
					.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			services.AddAuthentication()
				.AddIdentityServerJwt()
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = true;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidAudience = Configuration["Jwt:Site"],
						ValidIssuer = Configuration["Jwt:Site"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"]))
					};
				});

			services.AddControllersWithViews().AddFluentValidation().AddNewtonsoftJson();
			services.AddRazorPages();
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
			
		
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddTransient<IValidator<ScheduledActivityViewModel>, ScheduledActivityValidator>();
			services.AddTransient<IValidator<ActivityViewModel>, ActivityValidator>();
			services.AddTransient<IValidator<ActivityWithTrainersViewModel>, ActivityWithTrainersValidator>();
			services.AddTransient<IValidator<TrainerWithActivitiesViewModel>, TrainerValidator>();
			services.AddTransient<IValidator<UserSubscriptionViewModel>, UserSubscriptionValidator>();
			services.AddTransient<StatisticsQueryService, StatisticsQueryService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
						name: "default",
						pattern: "{controller}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}