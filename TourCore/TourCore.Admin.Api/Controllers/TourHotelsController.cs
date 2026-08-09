using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Tours;

namespace TourCore.Admin.Api.Controllers.Tours;

/// <summary>Управление вариантами проживания в составе тура.</summary>
/// <remarks>
/// Настройки принадлежат услуге тура. Справочные идентификаторы стран, городов и
/// отелей сохраняются без внешних ключей на соответствующие справочники.
/// </remarks>
[ApiController, Route("api/v1/tours/{tourId:int}/services/{serviceId:int}/hotels")]
public class TourHotelsController : ControllerBase
{
    private readonly IQueryHandler<GetTourHotelsQuery, IReadOnlyCollection<TourHotelGroupDto>> _get;
    private readonly ICommandHandler<
        ReplaceTourHotelsCommand,
        IReadOnlyCollection<TourHotelGroupDto>
    > _replace;

    /// <summary>Создаёт контроллер настроек проживания тура.</summary>
    /// <param name="get">Обработчик чтения настроек проживания.</param>
    /// <param name="replace">Обработчик полной замены настроек проживания.</param>
    public TourHotelsController(
        IQueryHandler<GetTourHotelsQuery, IReadOnlyCollection<TourHotelGroupDto>> get,
        ICommandHandler<ReplaceTourHotelsCommand, IReadOnlyCollection<TourHotelGroupDto>> replace
    )
    {
        _get = get;
        _replace = replace;
    }

    /// <summary>Получить варианты проживания выбранной услуги тура.</summary>
    /// <remarks>
    /// Группы и вложенные шаблоны возвращаются по OrderIndex. Продолжительности и
    /// идентификаторы разрешённых отелей возвращаются как числовые массивы.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param>
    /// <param name="serviceId">Идентификатор услуги проживания в составе тура.</param>
    /// <returns>Упорядоченные группы с шаблонами проживания.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<TourHotelGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int tourId, int serviceId)
    {
        return Ok(
            await _get.Handle(new GetTourHotelsQuery(tourId, serviceId), CancellationToken.None)
        );
    }

    /// <summary>Полностью заменить настройки проживания выбранной услуги тура.</summary>
    /// <remarks>
    /// Операция атомарно заменяет группы, шаблоны, продолжительности и разрешённые
    /// отели. Каждая группа является альтернативным вариантом, а шаблоны внутри
    /// группы выполняются последовательно. Пустой список отелей не означает
    /// «все отели города» и не допускается. Цены и ценовые пакеты не участвуют.
    /// После сохранения увеличиваются Revision услуги и тура.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param>
    /// <param name="serviceId">Идентификатор услуги проживания в составе тура.</param>
    /// <param name="command">Полная новая структура проживания.</param>
    /// <returns>Сохранённая структура с назначенными идентификаторами.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(IReadOnlyCollection<TourHotelGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Replace(
        int tourId,
        int serviceId,
        [FromBody] ReplaceTourHotelsCommand command
    )
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });

        command.TourId = tourId;
        command.TourServiceId = serviceId;

        return Ok(await _replace.Handle(command, CancellationToken.None));
    }
}
