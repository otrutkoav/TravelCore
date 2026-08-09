using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Common;
using TourCore.Contracts.Tours;

namespace TourCore.Admin.Api.Controllers.Tours;

/// <summary>Управление иерархическим справочником типов туров.</summary>
[ApiController, Route("api/v1/tours/tour-types")]
public class TourTypesController : ControllerBase
{
    private readonly ICommandHandler<CreateTourTypeCommand, TourTypeDto> _create;
    private readonly ICommandHandler<UpdateTourTypeCommand, TourTypeDto> _update;
    private readonly ICommandHandler<DeleteTourTypeCommand, bool> _delete;
    private readonly IQueryHandler<GetTourTypesQuery, PagedResponseDto<TourTypeListItemDto>> _list;
    private readonly IQueryHandler<GetTourTypeByIdQuery, TourTypeDto> _get;

    /// <summary>Создаёт контроллер справочника типов туров.</summary>
    public TourTypesController(
        ICommandHandler<CreateTourTypeCommand, TourTypeDto> create,
        ICommandHandler<UpdateTourTypeCommand, TourTypeDto> update,
        ICommandHandler<DeleteTourTypeCommand, bool> delete,
        IQueryHandler<GetTourTypesQuery, PagedResponseDto<TourTypeListItemDto>> list,
        IQueryHandler<GetTourTypeByIdQuery, TourTypeDto> get
    )
    {
        _create = create;
        _update = update;
        _delete = delete;
        _list = list;
        _get = get;
    }

    /// <summary>Получить страницу типов туров.</summary><remarks>Поддерживает поиск, фильтрацию по родителю, корневым элементам и пагинацию.</remarks>
    /// <param name="query">Поиск, фильтр дерева и параметры пагинации.</param><returns>Страница кратких данных типов туров.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResponseDto<TourTypeListItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetTourTypesQuery query)
    {
        return Ok(await _list.Handle(query ?? new GetTourTypesQuery(), CancellationToken.None));
    }

    /// <summary>Получить тип тура по идентификатору.</summary><remarks>Возвращает название, родителя и даты аудита. Ошибка: tour_type_not_found.</remarks>
    /// <param name="id">Идентификатор типа тура.</param><returns>Полные данные типа тура.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TourTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _get.Handle(new GetTourTypeByIdQuery(id), CancellationToken.None));
    }

    /// <summary>Создать тип тура.</summary><remarks>Name обязателен, уникален и содержит не более 128 символов. ParentId должен ссылаться на существующий тип.</remarks>
    /// <param name="command">Название и необязательный родитель.</param><returns>Созданный тип тура.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TourTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateTourTypeCommand command)
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        return Ok(await _create.Handle(command, CancellationToken.None));
    }

    /// <summary>Изменить тип тура.</summary><remarks>Нельзя назначить тип своим родителем или создать цикл в дереве.</remarks>
    /// <param name="id">Идентификатор типа.</param><param name="command">Новое название и родитель.</param><returns>Обновлённый тип тура.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TourTypeDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTourTypeCommand command)
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        command.Id = id;
        return Ok(await _update.Handle(command, CancellationToken.None));
    }

    /// <summary>Удалить тип тура.</summary><remarks>Тип с дочерними элементами или используемый туром удалить нельзя. При успехе возвращается 204.</remarks>
    /// <param name="id">Идентификатор типа.</param><returns>Пустой ответ 204.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _delete.Handle(new DeleteTourTypeCommand(id), CancellationToken.None);
        return NoContent();
    }
}

/// <summary>Управление шаблонами туров.</summary>
[ApiController, Route("api/v1/tours")]
public class ToursController : ControllerBase
{
    private readonly ICommandHandler<CreateTourCommand, TourDto> create;
    private readonly ICommandHandler<UpdateTourCommand, TourDto> update;
    private readonly ICommandHandler<DeleteTourCommand, bool> delete;
    private readonly IQueryHandler<GetToursQuery, PagedResponseDto<TourListItemDto>> list;
    private readonly IQueryHandler<GetTourByIdQuery, TourDto> get;

    public ToursController(
        ICommandHandler<CreateTourCommand, TourDto> create,
        ICommandHandler<UpdateTourCommand, TourDto> update,
        ICommandHandler<DeleteTourCommand, bool> delete,
        IQueryHandler<GetToursQuery, PagedResponseDto<TourListItemDto>> list,
        IQueryHandler<GetTourByIdQuery, TourDto> get
    )
    {
        this.create = create;
        this.update = update;
        this.delete = delete;
        this.list = list;
        this.get = get;
    }

    /// <summary>Получить страницу туров.</summary><remarks>Фильтры: Search, Status и CurrencyId. Статусы: 1 — новый, 2 — подготовленный, 3 — доступен для продажи, 4 — скрытый.</remarks>
    /// <param name="query">Фильтры и параметры пагинации.</param><returns>Страница кратких данных туров.</returns>
    [
        HttpGet,
        ProducesResponseType(typeof(PagedResponseDto<TourListItemDto>), StatusCodes.Status200OK)
    ]
    public async Task<IActionResult> Get([FromQuery] GetToursQuery query) =>
        Ok(await list.Handle(query ?? new GetToursQuery(), CancellationToken.None));

    /// <summary>Получить тур по идентификатору.</summary><remarks>Возвращает календарь доступности, назначенные типы и форматированные HTML-поля. Ошибка: tour_not_found.</remarks>
    /// <param name="id">Идентификатор тура.</param><returns>Полные данные шаблона тура.</returns>
    [HttpGet("{id:int}"), ProducesResponseType(typeof(TourDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await get.Handle(new GetTourByIdQuery(id), CancellationToken.None));

    /// <summary>Создать тур.</summary><remarks>DateMode 1 использует AvailableDateFrom и AvailableDateTo; DateMode 2 использует AvailableDates. CurrencyId и TourTypeIds должны существовать. Новый тур получает статус 1. HTML хранится в DescriptionHtml и BookingConditionsHtml.</remarks>
    /// <param name="command">Основные параметры, календарь, типы и описание.</param><returns>Созданный тур с ID от 100000000.</returns>
    [HttpPost, ProducesResponseType(typeof(TourDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateTourCommand command)
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });
        return Ok(await create.Handle(command, CancellationToken.None));
    }

    /// <summary>Изменить тур.</summary><remarks>Полностью заменяет основные поля, статус, типы и календарь. Для режима 1 AvailableDates игнорируется; для режима 2 границы периода игнорируются.</remarks>
    /// <param name="id">Идентификатор тура.</param><param name="command">Новое полное состояние тура.</param><returns>Обновлённый тур.</returns>
    [HttpPut("{id:int}"), ProducesResponseType(typeof(TourDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTourCommand command)
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });
        command.Id = id;
        return Ok(await update.Handle(command, CancellationToken.None));
    }

    /// <summary>Удалить тур.</summary><remarks>Удаляет шаблон, конкретные доступные даты и связи с типами. При успехе возвращается 204.</remarks>
    /// <param name="id">Идентификатор тура.</param><returns>Пустой ответ 204.</returns>
    [HttpDelete("{id:int}"), ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await delete.Handle(new DeleteTourCommand(id), CancellationToken.None);
        return NoContent();
    }
}
