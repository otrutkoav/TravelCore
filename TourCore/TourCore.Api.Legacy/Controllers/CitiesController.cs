using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Cities.Commands;
using TourCore.Application.Cities.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Geography.Cities;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы с городами.
    /// </summary>
    [RoutePrefix("api/geography/cities")]
    public class CitiesController : ApiController
    {
        private readonly ICommandHandler<CreateCityCommand, CityDto> _createHandler;
        private readonly ICommandHandler<UpdateCityCommand, CityDto> _updateHandler;
        private readonly IQueryHandler<GetCitiesQuery, ListResult<CityListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetCityByIdQuery, CityDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера городов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания города.</param>
        /// <param name="updateHandler">Обработчик обновления города.</param>
        /// <param name="getListHandler">Обработчик получения списка городов.</param>
        /// <param name="getByIdHandler">Обработчик получения города по идентификатору.</param>
        public CitiesController(
            ICommandHandler<CreateCityCommand, CityDto> createHandler,
            ICommandHandler<UpdateCityCommand, CityDto> updateHandler,
            IQueryHandler<GetCitiesQuery, ListResult<CityListItemDto>> getListHandler,
            IQueryHandler<GetCityByIdQuery, CityDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список городов.
        /// </summary>
        /// <remarks>
        /// Возвращает список городов из справочника.
        /// Используется в формах, фильтрах поиска, настройках направлений и выборе городов вылета.
        /// </remarks>
        /// <returns>Список городов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetCitiesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить город по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного города по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования города.
        /// </remarks>
        /// <param name="id">Идентификатор города.</param>
        /// <returns>Данные города.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetCityByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый город.
        /// </summary>
        /// <remarks>
        /// Добавляет новый город в справочник.
        /// Город обязательно должен быть привязан к стране.
        /// Регион указывается только при необходимости.
        /// Код города должен быть уникален в рамках страны.
        /// </remarks>
        /// <param name="command">Данные нового города.</param>
        /// <returns>Созданный город.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCityCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий город.
        /// </summary>
        /// <remarks>
        /// Изменяет данные города по указанному идентификатору.
        /// Используется для редактирования справочника городов.
        /// Страна обязательна, регион может быть не указан.
        /// </remarks>
        /// <param name="id">Идентификатор города.</param>
        /// <param name="command">Новые данные города.</param>
        /// <returns>Обновленный город.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateCityCommand command)
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