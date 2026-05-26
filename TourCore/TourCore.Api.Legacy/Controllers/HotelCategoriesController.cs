using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.HotelCategories.Commands;
using TourCore.Application.Hotels.HotelCategories.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.HotelCategories;

namespace TourCore.Api.Legacy.Controllers.Hotels
{
    /// <summary>
    /// Контроллер для работы с категориями отелей.
    /// </summary>
    [RoutePrefix("api/hotels/hotel-categories")]
    public class HotelCategoriesController : ApiController
    {
        private readonly ICommandHandler<CreateHotelCategoryCommand, HotelCategoryDto> _createHandler;
        private readonly ICommandHandler<UpdateHotelCategoryCommand, HotelCategoryDto> _updateHandler;
        private readonly IQueryHandler<GetHotelCategoriesQuery, PagedResponseDto<HotelCategoryListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetHotelCategoryByIdQuery, HotelCategoryDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера категорий отелей.
        /// </summary>
        /// <param name="createHandler">Обработчик создания категории отеля.</param>
        /// <param name="updateHandler">Обработчик обновления категории отеля.</param>
        /// <param name="getListHandler">Обработчик получения списка категорий отелей.</param>
        /// <param name="getByIdHandler">Обработчик получения категории отеля по идентификатору.</param>
        public HotelCategoriesController(
            ICommandHandler<CreateHotelCategoryCommand, HotelCategoryDto> createHandler,
            ICommandHandler<UpdateHotelCategoryCommand, HotelCategoryDto> updateHandler,
            IQueryHandler<GetHotelCategoriesQuery, PagedResponseDto<HotelCategoryListItemDto>> getListHandler,
            IQueryHandler<GetHotelCategoryByIdQuery, HotelCategoryDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список категорий отелей.
        /// </summary>
        /// <remarks>
        /// Возвращает список категорий отелей из справочника с поддержкой фильтрации, пагинации и сортировки.
        /// Используется в карточке отеля, фильтрах поиска, настройках размещения и отображении звездности.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию, английскому названию и глобальному коду категории отеля.
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
        /// - printOrder
        /// - globalCode
        /// - description
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка категорий отелей.</param>
        /// <returns>Страница списка категорий отелей.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetHotelCategoriesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetHotelCategoriesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить категорию отеля по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной категории отеля по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования категории отеля.
        /// </remarks>
        /// <param name="id">Идентификатор категории отеля.</param>
        /// <returns>Данные категории отеля.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetHotelCategoryByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новую категорию отеля.
        /// </summary>
        /// <remarks>
        /// Добавляет новую категорию отеля в справочник.
        /// Глобальный код категории, если указан, должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные новой категории отеля.</param>
        /// <returns>Созданная категория отеля.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateHotelCategoryCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующую категорию отеля.
        /// </summary>
        /// <remarks>
        /// Изменяет данные категории отеля по указанному идентификатору.
        /// Используется для редактирования справочника категорий отелей.
        /// Глобальный код категории, если указан, должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор категории отеля.</param>
        /// <param name="command">Новые данные категории отеля.</param>
        /// <returns>Обновленная категория отеля.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateHotelCategoryCommand command)
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