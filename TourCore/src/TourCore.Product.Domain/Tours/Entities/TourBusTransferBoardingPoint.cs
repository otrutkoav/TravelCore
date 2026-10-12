using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferBoardingPoint : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int BoardingPointId { get; protected set; }

        protected TourBusTransferBoardingPoint() { }

        internal TourBusTransferBoardingPoint(int boardingPointId)
        {
            if (boardingPointId <= 0)
                throw new DomainException("Boarding point identifier must be positive.");
            BoardingPointId = boardingPointId;
        }
    }
}
