using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Bus.BusTransferPoints.Commands;
using TourCore.Application.Bus.BusTransferPoints.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Bus.BusTransferPoints;

namespace TourCore.Api.Legacy.Controllers.Bus
{
    /// <summary>
    /// Контроллер для работы с точками автобусных переездов.
    /// </summary>
    [RoutePrefix("api/bus/transfer-points")]
    public class BusTransferPointsController : ApiController
    {
        private readonly ICommandHandler<CreateBusTransferPointCommand, BusTransferPointDto> _createHandler;
        private readonly ICommandHandler<UpdateBusTransferPointCommand, BusTransferPointDto> _updateHandler;
        private readonly IQueryHandler<GetBusTransferPointsQuery, ListResult<BusTransferPointListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetBusTransferPointByIdQuery, BusTransferPointDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера точек автобусных переездов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания точки автобусного переезда.</param>
        /// <param name="updateHandler">Обработчик обновления точки автобусного переезда.</param>
        /// <param name="getListHandler">Обработчик получения списка точек автобусных переездов.</param>
        /// <param name="getByIdHandler">Обработчик получения точки автобусного переезда по идентификатору.</param>
        public BusTransferPointsController(
            ICommandHandler<CreateBusTransferPointCommand, BusTransferPointDto> createHandler,
            ICommandHandler<UpdateBusTransferPointCommand, BusTransferPointDto> updateHandler,
            IQueryHandler<GetBusTransferPointsQuery, ListResult<BusTransferPointListItemDto>> getListHandler,
            IQueryHandler<GetBusTransferPointByIdQuery, BusTransferPointDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список точек автобусных переездов.
        /// </summary>
        /// <remarks>
        /// Возвращает список точек или участков автобусных переездов.
        ///
        /// Точка автобусного переезда описывает отдельный участок маршрута:
        /// город отправления, город прибытия, время и относительный день отправления/прибытия.
        ///
        /// Используется для детализации базового автобусного переезда.
        /// </remarks>
        /// <returns>Список точек автобусных переездов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetBusTransferPointsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить точку автобусного переезда по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной точки автобусного переезда.
        /// Используется для просмотра или открытия формы редактирования точки маршрута.
        /// </remarks>
        /// <param name="id">Идентификатор точки автобусного переезда.</param>
        /// <returns>Данные точки автобусного переезда.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetBusTransferPointByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать точку автобусного переезда.
        /// </summary>
        /// <remarks>
        /// Создает новую точку или участок автобусного переезда.
        ///
        /// Указывается базовый автобусный переезд, направление участка,
        /// время отправления/прибытия и относительный день маршрута.
        ///
        /// Не создает отдельное расписание или конкретную дату поездки.
        /// </remarks>
        /// <param name="command">Данные новой точки автобусного переезда.</param>
        /// <returns>Созданная точка автобусного переезда.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateBusTransferPointCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить точку автобусного переезда.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры точки или участка автобусного переезда.
        ///
        /// Изменяет привязку к автобусному переезду, направление участка,
        /// время отправления/прибытия и относительные дни.
        /// </remarks>
        /// <param name="id">Идентификатор точки автобусного переезда.</param>
        /// <param name="command">Новые данные точки автобусного переезда.</param>
        /// <returns>Обновленная точка автобусного переезда.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateBusTransferPointCommand command)
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