using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.CharterSeasons.Commands;
using TourCore.Application.Avia.CharterSeasons.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Contracts.Common;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с сезонами чартерных рейсов.
    /// </summary>
    [RoutePrefix("api/avia/charter-seasons")]
    public class CharterSeasonsController : ApiController
    {
        private readonly ICommandHandler<CreateCharterSeasonCommand, CharterSeasonDto> _createHandler;
        private readonly ICommandHandler<UpdateCharterSeasonCommand, CharterSeasonDto> _updateHandler;
        private readonly IQueryHandler<GetCharterSeasonsQuery, PagedResponseDto<CharterSeasonListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetCharterSeasonByIdQuery, CharterSeasonDto> _getByIdHandler;

        public CharterSeasonsController(
            ICommandHandler<CreateCharterSeasonCommand, CharterSeasonDto> createHandler,
            ICommandHandler<UpdateCharterSeasonCommand, CharterSeasonDto> updateHandler,
            IQueryHandler<GetCharterSeasonsQuery, PagedResponseDto<CharterSeasonListItemDto>> getListHandler,
            IQueryHandler<GetCharterSeasonByIdQuery, CharterSeasonDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список сезонов чартерных рейсов.
        /// </summary>
        /// <remarks>
        /// Возвращает список периодов действия чартерных маршрутов с поддержкой фильтрации, пагинации и сортировки.
        /// Сезон определяет даты действия, дни недели, время вылета,
        /// время прилета и признак прилета на следующий день.
        ///
        /// Используется для построения расписания чартерных программ,
        /// поиска доступных дат вылета и формирования пакетных туров.
        ///
        /// Фильтрация:
        /// - query.filter.charterId — фильтр по идентификатору базового чартерного рейса.
        /// - query.filter.dateFrom — фильтр по дате начала сезона.
        /// - query.filter.dateTo — фильтр по дате окончания сезона.
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
        /// - charterId
        /// - dateFrom
        /// - dateTo
        /// - daysOfWeek
        /// - timeFrom
        /// - timeTo
        /// - isNextDayArrival
        /// - remark
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка сезонов чартерных рейсов.</param>
        /// <returns>Страница списка сезонов чартерных рейсов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetCharterSeasonsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetCharterSeasonsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить сезон чартерного рейса по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного сезона чартерного рейса.
        /// Используется для просмотра или открытия формы редактирования расписания.
        /// </remarks>
        /// <param name="id">Идентификатор сезона чартерного рейса.</param>
        /// <returns>Данные сезона чартерного рейса.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetCharterSeasonByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый сезон чартерного рейса.
        /// </summary>
        /// <remarks>
        /// Создает период действия базового чартерного маршрута.
        ///
        /// Позволяет указать:
        /// - даты начала и окончания сезона;
        /// - дни недели в legacy-формате, например "1234567";
        /// - время вылета и прилета;
        /// - признак прилета на следующий день;
        /// - короткое примечание.
        ///
        /// Используется как основа для генерации доступных дат перелета.
        /// </remarks>
        /// <param name="command">Данные нового сезона чартерного рейса.</param>
        /// <returns>Созданный сезон чартерного рейса.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCharterSeasonCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий сезон чартерного рейса.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры периода действия чартерного маршрута:
        /// даты сезона, дни недели, время вылета и прилета,
        /// признак прилета на следующий день и примечание.
        ///
        /// Не изменяет сам базовый чартерный маршрут.
        /// </remarks>
        /// <param name="id">Идентификатор сезона чартерного рейса.</param>
        /// <param name="command">Новые данные сезона чартерного рейса.</param>
        /// <returns>Обновленный сезон чартерного рейса.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateCharterSeasonCommand command)
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