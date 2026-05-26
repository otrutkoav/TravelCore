using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.Hotels.Commands;
using TourCore.Application.Hotels.Hotels.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.Hotels;

namespace TourCore.Api.Legacy.Controllers.Hotels
{
    /// <summary>
    /// Контроллер для работы с отелями.
    /// </summary>
    [RoutePrefix("api/hotels")]
    public class HotelsController : ApiController
    {
        private readonly ICommandHandler<CreateHotelCommand, HotelDto> _createHandler;
        private readonly ICommandHandler<UpdateHotelCommand, HotelDto> _updateHandler;
        private readonly IQueryHandler<GetHotelsQuery, PagedResponseDto<HotelListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetHotelByIdQuery, HotelDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера отелей.
        /// </summary>
        /// <param name="createHandler">Обработчик создания отеля.</param>
        /// <param name="updateHandler">Обработчик обновления отеля.</param>
        /// <param name="getListHandler">Обработчик получения списка отелей.</param>
        /// <param name="getByIdHandler">Обработчик получения отеля по идентификатору.</param>
        public HotelsController(
            ICommandHandler<CreateHotelCommand, HotelDto> createHandler,
            ICommandHandler<UpdateHotelCommand, HotelDto> updateHandler,
            IQueryHandler<GetHotelsQuery, PagedResponseDto<HotelListItemDto>> getListHandler,
            IQueryHandler<GetHotelByIdQuery, HotelDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список отелей.
        /// </summary>
        /// <remarks>
        /// Возвращает список отелей с поддержкой фильтрации, пагинации и сортировки.
        /// Отель привязан к стране и городу, может быть связан с курортом
        /// и категорией отеля.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по коду, названию и английскому названию отеля.
        /// - query.filter.countryId — фильтр по идентификатору страны.
        /// - query.filter.cityId — фильтр по идентификатору города.
        /// - query.filter.resortId — фильтр по идентификатору курорта.
        /// - query.filter.categoryId — фильтр по идентификатору категории отеля.
        /// - query.filter.isCruise — фильтр по признаку круизного отеля.
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
        /// - countryId
        /// - cityId
        /// - resortId
        /// - categoryId
        /// - name
        /// - nameEn
        /// - stars
        /// - code
        /// - address
        /// - phone
        /// - fax
        /// - email
        /// - website
        /// - latitude
        /// - longitude
        /// - isCruise
        /// - sortOrder
        /// - rank
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка отелей.</param>
        /// <returns>Страница списка отелей.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetHotelsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetHotelsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить отель по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного отеля по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования отеля.
        /// </remarks>
        /// <param name="id">Идентификатор отеля.</param>
        /// <returns>Данные отеля.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetHotelByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать отель.
        /// </summary>
        /// <remarks>
        /// Создает новый отель.
        /// Можно указать страну, город, курорт, категорию,
        /// название, код, контакты, координаты и параметры сортировки.
        /// </remarks>
        /// <param name="command">Данные нового отеля.</param>
        /// <returns>Созданный отель.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateHotelCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить отель.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующего отеля.
        /// </remarks>
        /// <param name="id">Идентификатор отеля.</param>
        /// <param name="command">Новые данные отеля.</param>
        /// <returns>Обновленный отель.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateHotelCommand command)
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