namespace TourCore.Application.Transfers.TransferDirections.DTOs
{
    /// <summary>
    /// Фильтр списка направлений трансферов.
    /// </summary>
    public class TransferDirectionListFilter
    {
        /// <summary>
        /// Поиск по названию направления трансфера.
        /// </summary>
        public string Search { get; set; }
    }
}