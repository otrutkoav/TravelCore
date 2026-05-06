using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.AccommodationTypes.Commands;
using TourCore.Application.Hotels.AccommodationTypes.Queries;
using TourCore.Contracts.Hotels.AccommodationTypes;

namespace TourCore.Api.Legacy.Controllers.Hotels
{
    /// <summary>
    /// Контроллер для работы с типами размещения.
    /// </summary>
    [RoutePrefix("api/hotels/accommodation-types")]
    public class AccommodationTypesController : ApiController
    {
        private readonly ICommandHandler<CreateAccommodationTypeCommand, AccommodationTypeDto> _createHandler;
        private readonly ICommandHandler<UpdateAccommodationTypeCommand, AccommodationTypeDto> _updateHandler;
        private readonly IQueryHandler<GetAccommodationTypesQuery, ListResult<AccommodationTypeListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAccommodationTypeByIdQuery, AccommodationTypeDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера типов размещения.
        /// </summary>
        /// <param name="createHandler">Обработчик создания типа размещения.</param>
        /// <param name="updateHandler">Обработчик обновления типа размещения.</param>
        /// <param name="getListHandler">Обработчик получения списка типов размещения.</param>
        /// <param name="getByIdHandler">Обработчик получения типа размещения по идентификатору.</param>
        public AccommodationTypesController(
            ICommandHandler<CreateAccommodationTypeCommand, AccommodationTypeDto> createHandler,
            ICommandHandler<UpdateAccommodationTypeCommand, AccommodationTypeDto> updateHandler,
            IQueryHandler<GetAccommodationTypesQuery, ListResult<AccommodationTypeListItemDto>> getListHandler,
            IQueryHandler<GetAccommodationTypeByIdQuery, AccommodationTypeDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список типов размещения.
        /// </summary>
        /// <remarks>
        /// Возвращает справочник типов размещения.
        /// Тип размещения определяет код, название, возрастные ограничения,
        /// количество человек на номер и правила основных/дополнительных мест.
        /// </remarks>
        /// <returns>Список типов размещения.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetAccommodationTypesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить тип размещения по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного типа размещения.
        /// Используется для просмотра или открытия формы редактирования.
        /// </remarks>
        /// <param name="id">Идентификатор типа размещения.</param>
        /// <returns>Данные типа размещения.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAccommodationTypeByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать тип размещения.
        /// </summary>
        /// <remarks>
        /// Создает новый тип размещения.
        /// Можно указать код, название, возрастные ограничения,
        /// количество человек на номер, порядок сортировки и правила размещения.
        /// </remarks>
        /// <param name="command">Данные нового типа размещения.</param>
        /// <returns>Созданный тип размещения.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAccommodationTypeCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить тип размещения.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующего типа размещения.
        /// </remarks>
        /// <param name="id">Идентификатор типа размещения.</param>
        /// <param name="command">Новые данные типа размещения.</param>
        /// <returns>Обновленный тип размещения.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAccommodationTypeCommand command)
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