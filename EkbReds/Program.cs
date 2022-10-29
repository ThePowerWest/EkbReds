using ApplicationCore.Entities.Identity;
using ApplicationCore.Managers;
using Hangfire;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.WebEncoders;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Web.Configuration;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

builder.HangfireInitialize();

Dependencies.ConfigureServices(builder.Services);

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<MainContext>()
  .AddUserManager<UserManagerEx>()
  .AddDefaultTokenProviders();

builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

builder.Services.AddAuthentication().AddCookie();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCoreServices();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter () }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();