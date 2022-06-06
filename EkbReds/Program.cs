using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Entities.Identity;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
string connection = Environment.GetEnvironmentVariable("DB_STRING");

builder.Services.AddRazorPages();

builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
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

app.Run();