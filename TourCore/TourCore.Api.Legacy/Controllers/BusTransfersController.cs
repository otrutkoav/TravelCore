using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Bus.BusTransfers.Commands;
using TourCore.Application.Bus.BusTransfers.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Contracts.Common;

namespace TourCore.Api.Legacy.Controllers.Bus
{
    /// <summary>
    /// Контроллер для работы с автобусными переездами.
    /// </summary>
    [RoutePrefix("api/bus/transfers")]
    public class BusTransfersController : ApiController
    {
        private readonly ICommandHandler<CreateBusTransferCommand, BusTransferDto> _createHandler;
        private readonly ICommandHandler<UpdateBusTransferCommand, BusTransferDto> _updateHandler;
        private readonly IQueryHandler<GetBusTransfersQuery, PagedResponseDto<BusTransferListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetBusTransferByIdQuery, BusTransferDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера автобусных переездов.
        /// </summary>
        public BusTransfersController(
            ICommandHandler<CreateBusTransferCommand, BusTransferDto> createHandler,
            ICommandHandler<UpdateBusTransferCommand, BusTransferDto> updateHandler,
            IQueryHandler<GetBusTransfersQuery, PagedResponseDto<BusTransferListItemDto>> getListHandler,
            IQueryHandler<GetBusTransferByIdQuery, BusTransferDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список автобусных переездов.
        /// </summary>
        /// <remarks>
        /// Возвращает список маршрутов автобусных переездов между городами
        /// с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию автобусного переезда.
        /// - query.filter.countryFromId — фильтр по идентификатору страны отправления.
        /// - query.filter.cityFromId — фильтр по идентификатору города отправления.
        /// - query.filter.countryToId — фильтр по идентификатору страны прибытия.
        /// - query.filter.cityToId — фильтр по идентификатору города прибытия.
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
        /// - name
        /// - countryFromId
        /// - cityFromId
        /// - countryToId
        /// - cityToId
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка автобусных переездов.</param>
        /// <returns>Страница списка автобусных переездов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetBusTransfersQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetBusTransfersQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить автобусный переезд по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного автобусного переезда.
        /// Используется для просмотра или редактирования маршрута.
        /// </remarks>
        /// <param name="id">Идентификатор автобусного переезда.</param>
        /// <returns>Данные автобусного переезда.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetBusTransferByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать автобусный переезд.
        /// </summary>
        /// <remarks>
        /// Создает новый маршрут автобусного переезда между городами.
        /// 
        /// Указывает страну и город отправления и прибытия.
        /// Используется как базовая логистическая единица.
        /// </remarks>
        /// <param name="command">Данные автобусного переезда.</param>
        /// <returns>Созданный автобусный переезд.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateBusTransferCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить автобусный переезд.
        /// </summary>
        /// <remarks>
        /// Обновляет маршрут автобусного переезда.
        /// 
        /// Изменяет направление между городами, но не влияет на
        /// возможные связанные сущности (если появятся позже).
        /// </remarks>
        /// <param name="id">Идентификатор автобусного переезда.</param>
        /// <param name="command">Новые данные автобусного переезда.</param>
        /// <returns>Обновленный автобусный переезд.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateBusTransferCommand command)
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