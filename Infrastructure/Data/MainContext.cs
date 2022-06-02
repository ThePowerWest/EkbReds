using ApplicationCore.Entities.Main;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MainContext : IdentityDbContext<User, Role, string>
    {
        public MainContext(DbContextOptions<MainContext> options)
        : base(options)
        {
        }
    }
}
