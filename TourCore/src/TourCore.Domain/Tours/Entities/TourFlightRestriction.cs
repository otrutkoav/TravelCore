using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>
    /// Справочное ограничение для одного перелёта в составе тура.
    /// </summary>
    /// <remarks>
    /// Одна запись хранит один допустимый справочный идентификатор. Его назначение
    /// определяется свойством <see cref="Type"/>. Например, Type = DepartureCity и
    /// ReferenceId = 295 означают допустимый город отправления с идентификатором 295.
    /// ReferenceId намеренно не является внешним ключом: справочники могут поступать
    /// из другой базы, API или кэша. Внешняя согласованность проверяется прикладной
    /// логикой по актуальному справочному снимку.
    /// </remarks>
    public class TourFlightRestriction : Entity
    {
        /// <summary>Идентификатор перелёта тура, к которому относится ограничение.</summary>
        public int TourFlightId { get; protected set; }

        /// <summary>Перелёт тура, владеющий ограничением.</summary>
        public virtual TourFlight TourFlight { get; protected set; }

        /// <summary>Назначение справочного идентификатора.</summary>
        public TourFlightRestrictionType Type { get; protected set; }

        /// <summary>
        /// Идентификатор элемента соответствующего справочника без внешнего ключа
        /// на таблицу этого справочника.
        /// </summary>
        public int ReferenceId { get; protected set; }

        protected TourFlightRestriction() { }

        internal TourFlightRestriction(TourFlightRestrictionType type, int referenceId)
        {
            if (!System.Enum.IsDefined(typeof(TourFlightRestrictionType), type))
                throw new DomainException("Flight restriction type is invalid.");

            if (referenceId <= 0)
                throw new DomainException(
                    "Flight restriction reference id must be greater than zero."
                );

            Type = type;
            ReferenceId = referenceId;
        }
    }
}
