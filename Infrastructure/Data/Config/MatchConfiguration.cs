using ApplicationCore.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    /// <summary>
    /// Конфигурирование сущности Матч
    /// </summary>
    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        /// <inheritdoc /> 
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .Property(match => match.MatchStatusId)
                .HasConversion<byte>();
        }
    }
}