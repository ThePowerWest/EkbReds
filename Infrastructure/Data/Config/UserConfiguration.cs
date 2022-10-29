using ApplicationCore.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    /// <summary>
    /// Конфигурирование сущности Пользователь
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc /> 
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(user => user.Id)
                .ValueGeneratedOnAdd();

            builder.
                HasMany(user => user.Predictions)
                .WithOne(prediction => prediction.User)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}