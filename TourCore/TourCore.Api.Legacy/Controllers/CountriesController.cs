using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Countries.Commands;
using TourCore.Application.Countries.Queries;
using TourCore.Contracts.Geography.Countries;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы со странами.
    /// </summary>
    [RoutePrefix("api/geography/countries")]
    public class CountriesController : ApiController
    {
        private readonly ICommandHandler<CreateCountryCommand, CountryDto> _createHandler;
        private readonly ICommandHandler<UpdateCountryCommand, CountryDto> _updateHandler;
        private readonly IQueryHandler<GetCountriesQuery, ListResult<CountryListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetCountryByIdQuery, CountryDto> _getByIdHandler;

        public CountriesController(
            ICommandHandler<CreateCountryCommand, CountryDto> createHandler,
            ICommandHandler<UpdateCountryCommand, CountryDto> updateHandler,
            IQueryHandler<GetCountriesQuery, ListResult<CountryListItemDto>> getListHandler,
            IQueryHandler<GetCountryByIdQuery, CountryDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список стран.
        /// </summary>
        /// <remarks>
        /// Возвращает список стран из справочника.
        /// Можно использовать для выбора страны в формах, фильтрах и настройках.
        /// </remarks>
        /// <returns>Список стран.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetCountriesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить страну по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной страны по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования страны.
        /// </remarks>
        /// <param name="id">Идентификатор страны.</param>
        /// <returns>Данные страны.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetCountryByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новую страну.
        /// </summary>
        /// <remarks>
        /// Добавляет новую страну в справочник.
        /// Код страны, ISO2 и ISO3 должны быть уникальными.
        /// </remarks>
        /// <param name="command">Данные новой страны.</param>
        /// <returns>Созданная страна.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateCountryCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующую страну.
        /// </summary>
        /// <remarks>
        /// Изменяет данные страны по указанному идентификатору.
        /// Используется для редактирования справочника стран.
        /// </remarks>
        /// <param name="id">Идентификатор страны.</param>
        /// <param name="command">Новые данные страны.</param>
        /// <returns>Обновленная страна.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateCountryCommand command)
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