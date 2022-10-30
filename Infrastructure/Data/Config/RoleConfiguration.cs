using ApplicationCore.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config
{
    /// <summary>
    /// Конфигурирование сущности Роль
    /// </summary>
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        /// <inheritdoc /> 
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .Property(role => role.Id)
                .ValueGeneratedOnAdd();
        }
    }
}