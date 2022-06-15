using ApplicationCore.Entities.Identity;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Web.Configuration;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

Dependencies.ConfigureServices(builder.Configuration, builder.Services);

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
  .AddDefaultTokenProviders();

builder.Services.AddCoreServices(builder.Configuration);

builder.Services.AddRazorPages();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();