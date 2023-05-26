using KuceZBronksuBLL.Models;
using KuceZBronksuBLL.Services;
using KuceZBronksuDAL.Context;
using KuceZBronksuDAL.Models;
using KuceZBronksuDAL.Repository;
using KuceZBronksuDAL.Repository.IRepository;
using KuceZBronksuBLL.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KuceZBronksuBLL.ConfigurationMail;
using Hangfire;
using Serilog;
using KuceZBronksuWEB.Middlewares;

namespace KuceZBronksuWEB
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<MealAppContext>(options =>
			options.UseSqlServer("name=AuthDbContextConnection"));
			var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddHangfire(configuration => configuration
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UseSqlServerStorage(connectionstring, new Hangfire.SqlServer.SqlServerStorageOptions
			{
				CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
				SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
				QueuePollInterval = TimeSpan.Zero,
				UseRecommendedIsolationLevel = true,
				DisableGlobalLocks = true,
			}));
			builder.Host.UseSerilog((context, configuration) =>
			configuration.ReadFrom.Configuration(context.Configuration));
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            builder.Services.AddHangfireServer();
            builder.Services.AddMvc();
			builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
					.AddRoles<IdentityRole<int>>()
					.AddEntityFrameworkStores<MealAppContext>()
					.AddDefaultTokenProviders()
					.AddDefaultUI();
			builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			builder.Services.AddTransient<IReportService, ReportService>();
			builder.Services.AddTransient<IRecipeService,RecipeService>();
			builder.Services.AddTransient<ITimeService, TimeService>();
			builder.Services.AddTransient<IUserService,UserService>();
			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
			builder.Services.AddTransient<IMailService, MailService>();
			builder.Services.AddControllersWithViews();
			builder.Services.AddAutoMapper(typeof(RecipeViewModel), typeof(Program));
			builder.Services.AddAutoMapper(typeof(EditAndCreateViewModel), typeof(Program));
			builder.Services.AddAutoMapper(typeof(Recipe), typeof(Program));
			builder.Services.AddAutoMapper(typeof(User), typeof(Program));
			builder.Services.AddHttpClient();
			var app = builder.Build();
			await CreateDbIfNotExists(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication(); ;
			app.UseSerilogRequestLogging();

			app.UseAuthorization();
			app.UseHangfireDashboard();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			RecurringJob.AddOrUpdate<ITimeService>("SendEmailToAdmin",service => service.SendEmailToAdmin(),Cron.Minutely);
			app.MapRazorPages();

			app.Run();
		}

		private static async Task CreateDbIfNotExists(IHost host)
		{
			using var scope = host.Services.CreateScope();
			var services = scope.ServiceProvider;
			try
			{
				var context = services.GetRequiredService<MealAppContext>();
				var userManager = services.GetRequiredService<UserManager<User>>();
				var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
				await MealAppSeed.Initialize(context, userManager, roleManager);
			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An error occurred creating the DB.");
			}
		}
	}
}