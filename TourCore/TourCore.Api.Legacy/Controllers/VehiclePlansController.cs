using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Seating.VehiclePlans.Commands;
using TourCore.Application.Seating.VehiclePlans.Queries;
using TourCore.Contracts.Seating.VehiclePlans;

namespace TourCore.Api.Legacy.Controllers.Seating
{
    /// <summary>
    /// Контроллер для работы со схемами транспорта.
    /// </summary>
    [RoutePrefix("api/seating/vehicle-plans")]
    public class VehiclePlansController : ApiController
    {
        private readonly ICommandHandler<CreateVehiclePlanCommand, VehiclePlanDto> _createHandler;
        private readonly ICommandHandler<UpdateVehiclePlanCommand, VehiclePlanDto> _updateHandler;
        private readonly IQueryHandler<GetVehiclePlansQuery, ListResult<VehiclePlanListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetVehiclePlanByIdQuery, VehiclePlanDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера схем транспорта.
        /// </summary>
        /// <param name="createHandler">Обработчик создания схемы транспорта.</param>
        /// <param name="updateHandler">Обработчик обновления схемы транспорта.</param>
        /// <param name="getListHandler">Обработчик получения списка схем транспорта.</param>
        /// <param name="getByIdHandler">Обработчик получения схемы транспорта по идентификатору.</param>
        public VehiclePlansController(
            ICommandHandler<CreateVehiclePlanCommand, VehiclePlanDto> createHandler,
            ICommandHandler<UpdateVehiclePlanCommand, VehiclePlanDto> updateHandler,
            IQueryHandler<GetVehiclePlansQuery, ListResult<VehiclePlanListItemDto>> getListHandler,
            IQueryHandler<GetVehiclePlanByIdQuery, VehiclePlanDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список схем транспорта.
        /// </summary>
        /// <remarks>
        /// Возвращает список схем транспорта.
        /// Схема транспорта определяет количество рядов, колонок,
        /// номер зоны и параметры отображения схемы размещения.
        /// </remarks>
        /// <returns>Список схем транспорта.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetVehiclePlansQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить схему транспорта по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной схемы транспорта.
        /// Используется для просмотра или открытия формы редактирования схемы.
        /// </remarks>
        /// <param name="id">Идентификатор схемы транспорта.</param>
        /// <returns>Данные схемы транспорта.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetVehiclePlanByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать схему транспорта.
        /// </summary>
        /// <remarks>
        /// Создает новую схему транспорта.
        /// Схема привязывается к виду транспорта и задает размерность
        /// сетки размещения, номер зоны, ориентацию, признак самолета,
        /// даты действия и комментарий.
        /// </remarks>
        /// <param name="command">Данные новой схемы транспорта.</param>
        /// <returns>Созданная схема транспорта.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateVehiclePlanCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить схему транспорта.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующей схемы транспорта:
        /// транспорт, размерность сетки, номер зоны, название,
        /// ориентацию, признак самолета, даты действия и комментарий.
        /// </remarks>
        /// <param name="id">Идентификатор схемы транспорта.</param>
        /// <param name="command">Новые данные схемы транспорта.</param>
        /// <returns>Обновленная схема транспорта.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateVehiclePlanCommand command)
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