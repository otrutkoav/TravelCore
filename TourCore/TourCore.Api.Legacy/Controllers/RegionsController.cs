using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Geography.Regions.Commands;
using TourCore.Application.Geography.Regions.Queries;
using TourCore.Contracts.Geography.Regions;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы с регионами.
    /// </summary>
    [RoutePrefix("api/geography/regions")]
    public class RegionsController : ApiController
    {
        private readonly ICommandHandler<CreateRegionCommand, RegionDto> _createHandler;
        private readonly ICommandHandler<UpdateRegionCommand, RegionDto> _updateHandler;
        private readonly IQueryHandler<GetRegionsQuery, ListResult<RegionListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRegionByIdQuery, RegionDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера регионов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания региона.</param>
        /// <param name="updateHandler">Обработчик обновления региона.</param>
        /// <param name="getListHandler">Обработчик получения списка регионов.</param>
        /// <param name="getByIdHandler">Обработчик получения региона по идентификатору.</param>
        public RegionsController(
            ICommandHandler<CreateRegionCommand, RegionDto> createHandler,
            ICommandHandler<UpdateRegionCommand, RegionDto> updateHandler,
            IQueryHandler<GetRegionsQuery, ListResult<RegionListItemDto>> getListHandler,
            IQueryHandler<GetRegionByIdQuery, RegionDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список регионов.
        /// </summary>
        /// <remarks>
        /// Возвращает список регионов из справочника.
        /// Используется для группировки городов внутри страны, фильтрации направлений и настройки географии.
        /// </remarks>
        /// <returns>Список регионов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetRegionsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить регион по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного региона по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования региона.
        /// </remarks>
        /// <param name="id">Идентификатор региона.</param>
        /// <returns>Данные региона.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRegionByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый регион.
        /// </summary>
        /// <remarks>
        /// Добавляет новый регион в справочник.
        /// Регион обязательно должен быть привязан к стране.
        /// Код региона должен быть уникален в рамках страны.
        /// </remarks>
        /// <param name="command">Данные нового региона.</param>
        /// <returns>Созданный регион.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRegionCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий регион.
        /// </summary>
        /// <remarks>
        /// Изменяет данные региона по указанному идентификатору.
        /// Используется для редактирования справочника регионов.
        /// </remarks>
        /// <param name="id">Идентификатор региона.</param>
        /// <param name="command">Новые данные региона.</param>
        /// <returns>Обновленный регион.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRegionCommand command)
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