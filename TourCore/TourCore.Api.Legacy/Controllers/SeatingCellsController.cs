using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Seating.SeatingCells.Commands;
using TourCore.Application.Seating.SeatingCells.Queries;
using TourCore.Contracts.Seating.SeatingCells;

namespace TourCore.Api.Legacy.Controllers.Seating
{
    /// <summary>
    /// Контроллер для работы с ячейками схем размещения.
    /// </summary>
    [RoutePrefix("api/seating/cells")]
    public class SeatingCellsController : ApiController
    {
        private readonly ICommandHandler<CreateSeatingCellCommand, SeatingCellDto> _createHandler;
        private readonly ICommandHandler<UpdateSeatingCellCommand, SeatingCellDto> _updateHandler;
        private readonly IQueryHandler<GetSeatingCellsQuery, ListResult<SeatingCellListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetSeatingCellByIdQuery, SeatingCellDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера ячеек схем размещения.
        /// </summary>
        /// <param name="createHandler">Обработчик создания ячейки схемы размещения.</param>
        /// <param name="updateHandler">Обработчик обновления ячейки схемы размещения.</param>
        /// <param name="getListHandler">Обработчик получения списка ячеек схем размещения.</param>
        /// <param name="getByIdHandler">Обработчик получения ячейки схемы размещения по идентификатору.</param>
        public SeatingCellsController(
            ICommandHandler<CreateSeatingCellCommand, SeatingCellDto> createHandler,
            ICommandHandler<UpdateSeatingCellCommand, SeatingCellDto> updateHandler,
            IQueryHandler<GetSeatingCellsQuery, ListResult<SeatingCellListItemDto>> getListHandler,
            IQueryHandler<GetSeatingCellByIdQuery, SeatingCellDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список ячеек схем размещения.
        /// </summary>
        /// <remarks>
        /// Возвращает список ячеек схем размещения.
        /// Ячейка может описывать место, блок, проход или другой элемент схемы.
        /// </remarks>
        /// <returns>Список ячеек схем размещения.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetSeatingCellsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить ячейку схемы размещения по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной ячейки схемы размещения.
        /// Используется для просмотра или открытия формы редактирования.
        /// </remarks>
        /// <param name="id">Идентификатор ячейки схемы размещения.</param>
        /// <returns>Данные ячейки схемы размещения.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetSeatingCellByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать ячейку схемы размещения.
        /// </summary>
        /// <remarks>
        /// Создает новую ячейку схемы размещения для указанной схемы транспорта.
        /// Позволяет задать номер, тип ячейки, количество мест, индекс и визуальную границу.
        /// </remarks>
        /// <param name="command">Данные новой ячейки схемы размещения.</param>
        /// <returns>Созданная ячейка схемы размещения.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateSeatingCellCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить ячейку схемы размещения.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующей ячейки схемы размещения:
        /// схему транспорта, номер, тип, количество мест, индекс и визуальную границу.
        /// </remarks>
        /// <param name="id">Идентификатор ячейки схемы размещения.</param>
        /// <param name="command">Новые данные ячейки схемы размещения.</param>
        /// <returns>Обновленная ячейка схемы размещения.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateSeatingCellCommand command)
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