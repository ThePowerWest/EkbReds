using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Entities.DTO;
//using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
string connection = Environment.GetEnvironmentVariable("DB_STRING");

builder.Services.AddRazorPages();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMatchLoadService, MatchLoadService>();
builder.Services.AddScoped<IList<Match>, List<Match>>();
builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddIdentity<User, Role>(options => 
{ 
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

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
//var fileOptions = new StaticFileOptions
//{
//    OnPrepareResponse = (context) => context.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=604800"
//};
//app.UseStaticFiles(fileOptions);
app.Run();