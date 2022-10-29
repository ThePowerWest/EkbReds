using ApplicationCore.Entities.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    /// <summary>
    /// Конфигурирование сущности Статус Матча
    /// </summary>
    internal class MatchStatusConfiguration : IEntityTypeConfiguration<MatchStatus>
    {
        /// <inheritdoc /> 
        public void Configure(EntityTypeBuilder<MatchStatus> builder)
        {
            builder
                .Property(matchStatus => matchStatus.Id)
                .HasConversion<byte>();

            builder
                .HasData(
                    Enum.GetValues(typeof(MatchStatusId))
                        .Cast<MatchStatusId>()
                        .Select(matchStatus => new MatchStatus()
                        {
                            Id = matchStatus,
                            Name = matchStatus.ToString()
                        })
                );
        }
    }
}