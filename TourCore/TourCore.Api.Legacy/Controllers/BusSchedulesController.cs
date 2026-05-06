using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Bus.BusSchedules.Commands;
using TourCore.Application.Bus.BusSchedules.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Contracts.Common;

namespace TourCore.Api.Legacy.Controllers.Bus
{
    /// <summary>
    /// Контроллер для работы с расписаниями автобусных переездов.
    /// </summary>
    [RoutePrefix("api/bus/schedules")]
    public class BusSchedulesController : ApiController
    {
        private readonly ICommandHandler<CreateBusScheduleCommand, BusScheduleDto> _createHandler;
        private readonly ICommandHandler<UpdateBusScheduleCommand, BusScheduleDto> _updateHandler;
        private readonly IQueryHandler<GetBusSchedulesQuery, PagedResponseDto<BusScheduleListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetBusScheduleByIdQuery, BusScheduleDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера расписаний автобусных переездов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания расписания.</param>
        /// <param name="updateHandler">Обработчик обновления расписания.</param>
        /// <param name="getListHandler">Обработчик получения списка расписаний.</param>
        /// <param name="getByIdHandler">Обработчик получения расписания по идентификатору.</param>
        public BusSchedulesController(
            ICommandHandler<CreateBusScheduleCommand, BusScheduleDto> createHandler,
            ICommandHandler<UpdateBusScheduleCommand, BusScheduleDto> updateHandler,
            IQueryHandler<GetBusSchedulesQuery, PagedResponseDto<BusScheduleListItemDto>> getListHandler,
            IQueryHandler<GetBusScheduleByIdQuery, BusScheduleDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список расписаний автобусных переездов.
        /// </summary>
        /// <remarks>
        /// Возвращает список расписаний автобусных маршрутов
        /// с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Расписание определяет:
        /// - период действия (даты)
        /// - время отправления и прибытия
        /// - дни недели выполнения рейса
        /// - длительность маршрута (в днях)
        ///
        /// Используется для формирования календаря автобусных туров.
        ///
        /// Фильтрация:
        /// - query.filter.busTransferId — фильтр по идентификатору автобусного переезда.
        ///
        /// Пагинация:
        /// - query.page — номер страницы. Минимальное значение: 1. По умолчанию: 1.
        /// - query.pageSize — размер страницы. По умолчанию: 20. Максимальное значение: 100.
        ///
        /// Сортировка:
        /// - query.sortBy — поле сортировки.
        /// - query.sortDirection — направление сортировки:
        ///   0 — по возрастанию,
        ///   1 — по убыванию.
        ///
        /// Допустимые значения query.sortBy:
        /// - id
        /// - busTransferId
        /// - dateFrom
        /// - dateTo
        /// - timeFrom
        /// - timeTo
        /// - daysOnRoad
        /// </remarks>
        /// <param name="query">
        /// Параметры фильтрации, пагинации и сортировки расписаний автобусных переездов.
        /// </param>
        /// <returns>Страница списка расписаний автобусных переездов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetBusSchedulesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetBusSchedulesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить расписание автобусного переезда по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные конкретного расписания автобусного маршрута.
        /// Используется для просмотра или редактирования расписания.
        /// </remarks>
        /// <param name="id">Идентификатор расписания.</param>
        /// <returns>Данные расписания автобусного переезда.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetBusScheduleByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать расписание автобусного переезда.
        /// </summary>
        /// <remarks>
        /// Создает новое расписание для автобусного маршрута.
        ///
        /// Позволяет задать:
        /// - период действия маршрута
        /// - время отправления и прибытия
        /// - дни недели (в формате "1234567")
        /// - длительность поездки в днях
        ///
        /// Не создает конкретные рейсы — только шаблон расписания.
        /// </remarks>
        /// <param name="command">Данные нового расписания.</param>
        /// <returns>Созданное расписание.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateBusScheduleCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить расписание автобусного переезда.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующего расписания:
        /// период действия, время, дни недели и длительность маршрута.
        ///
        /// Не влияет на уже сформированные поездки (если будут реализованы).
        /// </remarks>
        /// <param name="id">Идентификатор расписания.</param>
        /// <param name="command">Новые данные расписания.</param>
        /// <returns>Обновленное расписание.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateBusScheduleCommand command)
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