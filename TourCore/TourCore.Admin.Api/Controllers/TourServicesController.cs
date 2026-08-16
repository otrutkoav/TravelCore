using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Tours;

namespace TourCore.Admin.Api.Controllers.Tours;

/// <summary>Управление составом услуг тура.</summary>
/// <remarks>
/// Работает с экземплярами услуг конкретного тура, а не со справочником ServiceTypes.
/// OrderIndex назначается сервером: при создании услуга добавляется в конец, а перестановка
/// выполняется передачей полного массива идентификаторов в требуемом порядке.
/// </remarks>
[ApiController, Route("api/v1/tours/{tourId:int}/services")]
public class TourServicesController : ControllerBase
{
    private readonly ICommandHandler<CreateTourServiceCommand, TourServiceDto> _create;
    private readonly ICommandHandler<UpdateTourServiceCommand, TourServiceDto> _update;
    private readonly ICommandHandler<DeleteTourServiceCommand, bool> _delete;
    private readonly ICommandHandler<
        ReorderTourServicesCommand,
        IReadOnlyCollection<TourServiceDto>
    > _reorder;
    private readonly IQueryHandler<GetTourServicesQuery, IReadOnlyCollection<TourServiceDto>> _list;
    private readonly IQueryHandler<GetTourServiceByIdQuery, TourServiceDto> _get;

    /// <summary>Создаёт экземпляр контроллера состава тура.</summary>
    /// <param name="create">Обработчик добавления услуги.</param><param name="update">Обработчик изменения услуги.</param><param name="delete">Обработчик удаления услуги.</param><param name="reorder">Обработчик изменения порядка.</param><param name="list">Обработчик получения состава.</param><param name="get">Обработчик получения услуги по ID.</param>
    public TourServicesController(
        ICommandHandler<CreateTourServiceCommand, TourServiceDto> create,
        ICommandHandler<UpdateTourServiceCommand, TourServiceDto> update,
        ICommandHandler<DeleteTourServiceCommand, bool> delete,
        ICommandHandler<ReorderTourServicesCommand, IReadOnlyCollection<TourServiceDto>> reorder,
        IQueryHandler<GetTourServicesQuery, IReadOnlyCollection<TourServiceDto>> list,
        IQueryHandler<GetTourServiceByIdQuery, TourServiceDto> get
    )
    {
        _create = create;
        _update = update;
        _delete = delete;
        _reorder = reorder;
        _list = list;
        _get = get;
    }

    /// <summary>Получить полный состав указанного тура.</summary>
    /// <remarks>Возвращает все услуги по возрастанию OrderIndex. Для существующего тура без услуг возвращается пустой массив. Если тур отсутствует, возвращается tour_not_found.</remarks>
    /// <param name="tourId">Идентификатор тура.</param><returns>Упорядоченный список услуг тура.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<TourServiceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int tourId)
    {
        return Ok(await _list.Handle(new GetTourServicesQuery(tourId), CancellationToken.None));
    }

    /// <summary>Получить услугу тура по идентификатору.</summary>
    /// <remarks>Поиск ограничен указанным туром. Для неизвестного ID или услуги другого тура возвращается tour_service_not_found.</remarks>
    /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор экземпляра услуги.</param><returns>Полные данные услуги, включая OrderIndex и Revision.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TourServiceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int tourId, int id)
    {
        return Ok(
            await _get.Handle(new GetTourServiceByIdQuery(tourId, id), CancellationToken.None)
        );
    }

    /// <summary>Добавить услугу в конец состава тура.</summary>
    /// <remarks>
    /// serviceTypeId обязателен и должен ссылаться на ServiceTypes; name обязателен и ограничен 256 символами;
    /// parentServiceId необязателен и должен ссылаться на услугу того же тура. OrderIndex вычисляется сервером,
    /// Revision новой услуги равна 1. Операция увеличивает Revision тура.
    /// isHidden скрывает услугу в клиентских интерфейсах; isRemovable разрешает убрать её из варианта
    /// в корзине; isOptional исключает её из первоначального расчёта до выбора пользователем.
    /// Скрытая услуга не может быть одновременно удаляемой или опциональной.
    /// Возможные ошибки: tour_not_found, service_type_not_found, tour_service_parent_not_found.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param><param name="command">Вид, название и необязательная родительская услуга.</param><returns>Созданная услуга с назначенными ID, OrderIndex и Revision.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TourServiceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(int tourId, [FromBody] CreateTourServiceCommand command)
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        command.TourId = tourId;
        return Ok(await _create.Handle(command, CancellationToken.None));
    }

    /// <summary>Изменить содержательные данные услуги тура.</summary>
    /// <remarks>Заменяет ServiceTypeId, Name, ParentServiceId, IsHidden, IsRemovable и IsOptional, но не меняет OrderIndex. Скрытая услуга не может быть удаляемой или опциональной. Родителем нельзя назначить саму услугу, услугу другого тура или собственного потомка. При успехе увеличиваются Revision услуги и тура. Возможные ошибки: tour_not_found, tour_service_not_found, service_type_not_found, tour_service_parent_not_found.</remarks>
    /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор изменяемой услуги.</param><param name="command">Новые данные услуги.</param><returns>Обновлённая услуга с новой Revision.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TourServiceDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(
        int tourId,
        int id,
        [FromBody] UpdateTourServiceCommand command
    )
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        command.TourId = tourId;
        command.Id = id;
        return Ok(await _update.Handle(command, CancellationToken.None));
    }

    /// <summary>Удалить услугу из состава тура.</summary>
    /// <remarks>
    /// Вместе с услугой каскадно удаляется принадлежащая ей специализированная
    /// структура: группы и ограничения перелётов, варианты проживания либо
    /// настройки автобусного/железнодорожного переезда. Услугу, являющуюся
    /// родителем других TourService, удалить нельзя. После удаления сервер
    /// уплотняет OrderIndex оставшихся услуг до 1..N и увеличивает Revision тура.
    /// Возможные ошибки: tour_not_found, tour_service_not_found,
    /// tour_service_has_children.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор удаляемой услуги.</param><returns>Пустой ответ 204 No Content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int tourId, int id)
    {
        await _delete.Handle(new DeleteTourServiceCommand(tourId, id), CancellationToken.None);
        return NoContent();
    }

    /// <summary>Изменить порядок услуг в составе тура.</summary>
    /// <remarks>ServiceIds должен содержать все услуги тура ровно по одному разу в требуемом порядке. Частичный список, повторы, неизвестные ID и услуги другого тура не принимаются. Сервер назначает OrderIndex от 1 до N. Revision тура увеличивается один раз, Revision отдельных услуг не изменяется.</remarks>
    /// <param name="tourId">Идентификатор тура.</param><param name="command">Полная последовательность идентификаторов услуг.</param><returns>Состав тура в новом порядке.</returns>
    [HttpPut("order")]
    [ProducesResponseType(typeof(IReadOnlyCollection<TourServiceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Reorder(
        int tourId,
        [FromBody] ReorderTourServicesCommand command
    )
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        command.TourId = tourId;
        return Ok(await _reorder.Handle(command, CancellationToken.None));
    }
}
