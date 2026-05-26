using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.Charters.Commands;
using TourCore.Application.Avia.Charters.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.Charters;
using TourCore.Contracts.Common;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с чартерными рейсами.
    /// </summary>
    [RoutePrefix("api/avia/charters")]
    public class ChartersController : ApiController
    {
        private readonly ICommandHandler<CreateCharterCommand, CharterDto> _createHandler;
        private readonly ICommandHandler<UpdateCharterCommand, CharterDto> _updateHandler;
        private readonly IQueryHandler<GetChartersQuery, PagedResponseDto<CharterListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetCharterByIdQuery, CharterDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера чартерных рейсов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания чартерного рейса.</param>
        /// <param name="updateHandler">Обработчик обновления чартерного рейса.</param>
        /// <param name="getListHandler">Обработчик получения списка чартерных рейсов.</param>
        /// <param name="getByIdHandler">Обработчик получения чартерного рейса по идентификатору.</param>
        public ChartersController(
            ICommandHandler<CreateCharterCommand, CharterDto> createHandler,
            ICommandHandler<UpdateCharterCommand, CharterDto> updateHandler,
            IQueryHandler<GetChartersQuery, PagedResponseDto<CharterListItemDto>> getListHandler,
            IQueryHandler<GetCharterByIdQuery, CharterDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список чартерных рейсов.
        /// </summary>
        /// <remarks>
        /// Возвращает список базовых чартерных маршрутов с поддержкой фильтрации, пагинации и сортировки.
        /// Чартер определяет направление перелета, перевозчика,
        /// номер рейса и технические параметры.
        ///
        /// Используется как шаблон при формировании авиаперелетов в турах,
        /// пакетных предложений и расписаний чартерных программ.
        ///
        /// Не содержит дат и конкретных вылетов — только базовую связку маршрута.
        ///
        /// Фильтрация:
        /// - query.filter.departureCityId — фильтр по идентификатору города вылета.
        /// - query.filter.arrivalCityId — фильтр по идентификатору города прилета.
        /// - query.filter.departureAirportCode — поиск по коду аэропорта вылета.
        /// - query.filter.arrivalAirportCode — поиск по коду аэропорта прилета.
        /// - query.filter.airlineCode — поиск по коду авиакомпании.
        /// - query.filter.flightNumber — поиск по номеру рейса.
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
        /// - departureCityId
        /// - departureAirportCode
        /// - arrivalCityId
        /// - arrivalAirportCode
        /// - airlineCode
        /// - flightNumber
        /// - aircraftCode
        /// - airClassCode
        /// - stopsCount
        /// - timeChangesCode
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка чартерных рейсов.</param>
        /// <returns>Страница списка чартерных рейсов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetChartersQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetChartersQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить чартерный рейс по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного чартерного рейса по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования чартерного маршрута.
        /// </remarks>
        /// <param name="id">Идентификатор чартерного рейса.</param>
        /// <returns>Данные чартерного рейса.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetCharterByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый чартерный рейс.
        /// </summary>
        /// <remarks>
        /// Создает базовый чартерный маршрут между городами.
        /// Может включать аэропорты вылета и прилета, авиакомпанию,
        /// номер рейса, тип самолета и класс обслуживания.
        ///
        /// Используется как основа для построения реальных перелетов,
        /// расписаний и пакетных туров.
        /// </remarks>
        /// <param name="command">Данные нового чартерного рейса.</param>
        /// <returns>Созданный чартерный рейс.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCharterCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий чартерный рейс.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры базового чартерного маршрута.
        /// Изменяет направление, перевозчика, номер рейса и технические параметры.
        ///
        /// Не изменяет фактические рейсы или конкретные вылеты,
        /// если они будут созданы на основе этого чартерного маршрута.
        /// </remarks>
        /// <param name="id">Идентификатор чартерного рейса.</param>
        /// <param name="command">Новые данные чартерного рейса.</param>
        /// <returns>Обновленный чартерный рейс.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateCharterCommand command)
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