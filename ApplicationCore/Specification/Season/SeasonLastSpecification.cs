using Ardalis.Specification;
using EntitySeason = ApplicationCore.Entities.Main.Season;

namespace ApplicationCore.Specification.Season
{
    public class SeasonLastSpecification : Specification<EntitySeason>
    {
        public SeasonLastSpecification()
        {
            Query
                .OrderByDescending(season => season.Id);
        }
    }
}