namespace TourCore.Application.Hotels.AccommodationPlacementRules.Queries
{
    public class GetAccommodationPlacementRuleByIdQuery
    {
        public GetAccommodationPlacementRuleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}