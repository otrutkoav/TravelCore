using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Railway.RailwayTransfers.Commands;
using TourCore.Application.Railway.RailwayTransfers.Queries;
using TourCore.Contracts.Railway.RailwayTransfers;

namespace TourCore.Api.Legacy.Controllers.Railway
{
    /// <summary>
    /// Контроллер для работы с ЖД-переездами.
    /// </summary>
    [RoutePrefix("api/railway/transfers")]
    public class RailwayTransfersController : ApiController
    {
        private readonly ICommandHandler<CreateRailwayTransferCommand, RailwayTransferDto> _createHandler;
        private readonly ICommandHandler<UpdateRailwayTransferCommand, RailwayTransferDto> _updateHandler;
        private readonly IQueryHandler<GetRailwayTransfersQuery, ListResult<RailwayTransferListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRailwayTransferByIdQuery, RailwayTransferDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера ЖД-переездов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания ЖД-переезда.</param>
        /// <param name="updateHandler">Обработчик обновления ЖД-переезда.</param>
        /// <param name="getListHandler">Обработчик получения списка ЖД-переездов.</param>
        /// <param name="getByIdHandler">Обработчик получения ЖД-переезда по идентификатору.</param>
        public RailwayTransfersController(
            ICommandHandler<CreateRailwayTransferCommand, RailwayTransferDto> createHandler,
            ICommandHandler<UpdateRailwayTransferCommand, RailwayTransferDto> updateHandler,
            IQueryHandler<GetRailwayTransfersQuery, ListResult<RailwayTransferListItemDto>> getListHandler,
            IQueryHandler<GetRailwayTransferByIdQuery, RailwayTransferDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список ЖД-переездов.
        /// </summary>
        /// <remarks>
        /// Возвращает список базовых направлений железнодорожных переездов.
        ///
        /// ЖД-переезд определяет маршрут между городами:
        /// страну и город отправления, страну и город прибытия.
        ///
        /// Не содержит расписания, поездов и конкретных дат отправления.
        /// </remarks>
        /// <returns>Список ЖД-переездов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetRailwayTransfersQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить ЖД-переезд по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного ЖД-переезда по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования ЖД-направления.
        /// </remarks>
        /// <param name="id">Идентификатор ЖД-переезда.</param>
        /// <returns>Данные ЖД-переезда.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRailwayTransferByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать ЖД-переезд.
        /// </summary>
        /// <remarks>
        /// Создает новое железнодорожное направление между городами.
        ///
        /// Указывается название маршрута, страна и город отправления,
        /// страна и город прибытия.
        ///
        /// Используется как базовый справочник ЖД-направлений.
        /// </remarks>
        /// <param name="command">Данные нового ЖД-переезда.</param>
        /// <returns>Созданный ЖД-переезд.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRailwayTransferCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить ЖД-переезд.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры железнодорожного направления.
        ///
        /// Изменяет название маршрута, страну и город отправления,
        /// страну и город прибытия.
        ///
        /// Не изменяет расписания или конкретные рейсы, если они будут добавлены позже.
        /// </remarks>
        /// <param name="id">Идентификатор ЖД-переезда.</param>
        /// <param name="command">Новые данные ЖД-переезда.</param>
        /// <returns>Обновленный ЖД-переезд.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRailwayTransferCommand command)
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