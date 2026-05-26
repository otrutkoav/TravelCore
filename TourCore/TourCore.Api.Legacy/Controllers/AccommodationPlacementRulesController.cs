using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.AccommodationPlacementRules.Commands;
using TourCore.Application.Hotels.AccommodationPlacementRules.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;

namespace TourCore.Api.Legacy.Controllers.Hotels
{
    /// <summary>
    /// Контроллер для работы с правилами размещения.
    /// </summary>
    [RoutePrefix("api/hotels/accommodation-placement-rules")]
    public class AccommodationPlacementRulesController : ApiController
    {
        private readonly ICommandHandler<CreateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto> _createHandler;
        private readonly ICommandHandler<UpdateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto> _updateHandler;
        private readonly IQueryHandler<GetAccommodationPlacementRulesQuery, PagedResponseDto<AccommodationPlacementRuleListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAccommodationPlacementRuleByIdQuery, AccommodationPlacementRuleDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера правил размещения.
        /// </summary>
        /// <param name="createHandler">Обработчик создания правила размещения.</param>
        /// <param name="updateHandler">Обработчик обновления правила размещения.</param>
        /// <param name="getListHandler">Обработчик получения списка правил размещения.</param>
        /// <param name="getByIdHandler">Обработчик получения правила размещения по идентификатору.</param>
        public AccommodationPlacementRulesController(
            ICommandHandler<CreateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto> createHandler,
            ICommandHandler<UpdateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto> updateHandler,
            IQueryHandler<GetAccommodationPlacementRulesQuery, PagedResponseDto<AccommodationPlacementRuleListItemDto>> getListHandler,
            IQueryHandler<GetAccommodationPlacementRuleByIdQuery, AccommodationPlacementRuleDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список правил размещения.
        /// </summary>
        /// <remarks>
        /// Возвращает список правил размещения с поддержкой фильтрации, пагинации и сортировки.
        /// Правило определяет количество взрослых, количество детей
        /// и признак того, что дети считаются инфантами.
        ///
        /// Возрастные диапазоны детей являются частью правила размещения.
        ///
        /// Фильтрация:
        /// - query.filter.adultsCount — фильтр по количеству взрослых.
        /// - query.filter.childrenCount — фильтр по количеству детей.
        /// - query.filter.childrenAreInfants — фильтр по признаку, что дети считаются инфантами.
        ///
        /// Пагинация:
        /// - query.page — номер страницы. Минимальное значение: 1. По умолчанию: 1.
        /// - query.pageSize — размер страницы. По умолчанию: 20. Максимальное значение: 100.
        ///
        /// Сортировка:
        /// - query.sortBy — поле сортировки.
        /// - query.sortDirection — направление сортировки: 0 — по возрастанию, 1 — по убыванию.
        ///
        /// Допустимые значения query.sortBy:
        /// - id
        /// - adultsCount
        /// - childrenCount
        /// - childrenAreInfants
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка правил размещения.</param>
        /// <returns>Страница списка правил размещения.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetAccommodationPlacementRulesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetAccommodationPlacementRulesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить правило размещения по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного правила размещения по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования правила.
        /// </remarks>
        /// <param name="id">Идентификатор правила размещения.</param>
        /// <returns>Данные правила размещения.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAccommodationPlacementRuleByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать правило размещения.
        /// </summary>
        /// <remarks>
        /// Создает новое правило размещения.
        /// Правило может использоваться типами размещения как правило
        /// для основных или дополнительных мест.
        /// </remarks>
        /// <param name="command">Данные нового правила размещения.</param>
        /// <returns>Созданное правило размещения.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAccommodationPlacementRuleCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить правило размещения.
        /// </summary>
        /// <remarks>
        /// Обновляет количество взрослых, количество детей
        /// и признак того, что дети считаются инфантами.
        ///
        /// Возрастные диапазоны детей обновляются отдельно.
        /// </remarks>
        /// <param name="id">Идентификатор правила размещения.</param>
        /// <param name="command">Новые данные правила размещения.</param>
        /// <returns>Обновленное правило размещения.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAccommodationPlacementRuleCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            command.Id = id;

            var result = await _updateHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }
    }
}