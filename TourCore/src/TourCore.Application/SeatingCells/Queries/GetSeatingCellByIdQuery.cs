namespace TourCore.Application.SeatingCells.Queries
{
    public class GetSeatingCellByIdQuery
    {
        public GetSeatingCellByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}