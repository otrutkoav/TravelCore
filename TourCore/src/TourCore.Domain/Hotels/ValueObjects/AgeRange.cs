using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.ValueObjects
{
    /// <summary>
    /// Диапазон возраста.
    /// Используется, например, для задания допустимого возраста детей.
    /// </summary>
    public class AgeRange
    {
        /// <summary>
        /// Возраст "от".
        /// </summary>
        public short AgeFrom { get; }

        /// <summary>
        /// Возраст "до".
        /// </summary>
        public short AgeTo { get; }

        public AgeRange(short ageFrom, short ageTo)
        {
            if (ageFrom < 0)
                throw new DomainException("Age range start cannot be negative.");

            if (ageTo < 0)
                throw new DomainException("Age range end cannot be negative.");

            if (ageFrom > ageTo)
                throw new DomainException("Age range start cannot be greater than age range end.");

            AgeFrom = ageFrom;
            AgeTo = ageTo;
        }
    }
}