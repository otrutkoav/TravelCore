using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Geography.Countries.Commands;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Contracts.Geography.Countries;
using TourCore.Contracts.Common;

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
        private readonly IQueryHandler<GetCountriesQuery, PagedResponseDto<CountryListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetCountryByIdQuery, CountryDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера стран.
        /// </summary>
        /// <param name="createHandler">Обработчик создания страны.</param>
        /// <param name="updateHandler">Обработчик обновления страны.</param>
        /// <param name="getListHandler">Обработчик получения списка стран.</param>
        /// <param name="getByIdHandler">Обработчик получения страны по идентификатору.</param>
        public CountriesController(
            ICommandHandler<CreateCountryCommand, CountryDto> createHandler,
            ICommandHandler<UpdateCountryCommand, CountryDto> updateHandler,
            IQueryHandler<GetCountriesQuery, PagedResponseDto<CountryListItemDto>> getListHandler,
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
        /// Возвращает список стран с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию, английскому названию, коду страны, ISO2, ISO3, цифровому коду и названию гражданства.
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
        /// - nameEn
        /// - code
        /// - isoCode2
        /// - isoCode3
        /// - digitalCode
        /// - citizenshipName
        /// - citizenshipNameEn
        /// - sortOrder
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка стран.</param>
        /// <returns>Страница списка стран.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetCountriesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetCountriesQuery(),
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