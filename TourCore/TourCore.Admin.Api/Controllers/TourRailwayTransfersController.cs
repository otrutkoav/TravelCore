using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Tours;

namespace TourCore.Admin.Api.Controllers.Tours;

/// <summary>Управление железнодорожным переездом в составе тура.</summary>
/// <remarks>
/// Одна услуга содержит одну конфигурацию. Идентификаторы стран, городов,
/// железнодорожных переездов и транспорта сохраняются без внешних ключей.
/// </remarks>
[ApiController]
[Route("api/v1/tours/{tourId:int}/services/{serviceId:int}/railway-transfer")]
public class TourRailwayTransfersController : ControllerBase
{
    private readonly IQueryHandler<GetTourRailwayTransferQuery, TourRailwayTransferDto> _get;
    private readonly ICommandHandler<
        ReplaceTourRailwayTransferCommand,
        TourRailwayTransferDto
    > _replace;

    /// <summary>Создаёт контроллер настроек железнодорожного переезда.</summary>
    /// <param name="get">Обработчик чтения настроек.</param>
    /// <param name="replace">Обработчик полной замены настроек.</param>
    public TourRailwayTransfersController(
        IQueryHandler<GetTourRailwayTransferQuery, TourRailwayTransferDto> get,
        ICommandHandler<ReplaceTourRailwayTransferCommand, TourRailwayTransferDto> replace
    )
    {
        _get = get;
        _replace = replace;
    }

    /// <summary>Получить настройки железнодорожного переезда.</summary>
    /// <remarks>
    /// До первого сохранения возвращает пустые наборы и полные суточные интервалы.
    /// Транспорт может быть не ограничен.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param>
    /// <param name="serviceId">Идентификатор услуги железнодорожного переезда.</param>
    /// <returns>Текущая конфигурация.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(TourRailwayTransferDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int tourId, int serviceId) =>
        Ok(
            await _get.Handle(
                new GetTourRailwayTransferQuery(tourId, serviceId),
                CancellationToken.None
            )
        );

    /// <summary>Создать или полностью заменить настройки железнодорожного переезда.</summary>
    /// <remarks>
    /// Атомарно заменяет направления, допустимые записи RailwayTransfers,
    /// транспорт и интервалы времени. Цены, ценовые пакеты и поставщики не
    /// участвуют. После сохранения увеличиваются Revision услуги и тура.
    /// </remarks>
    /// <param name="tourId">Идентификатор тура.</param>
    /// <param name="serviceId">Идентификатор услуги железнодорожного переезда.</param>
    /// <param name="command">Полная новая конфигурация.</param>
    /// <returns>Сохранённая конфигурация.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(TourRailwayTransferDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Replace(
        int tourId,
        int serviceId,
        [FromBody] ReplaceTourRailwayTransferCommand command
    )
    {
        if (command == null)
            throw new ValidationException(new[] { "Тело запроса не передано." });
        command.TourId = tourId;
        command.TourServiceId = serviceId;
        return Ok(await _replace.Handle(command, CancellationToken.None));
    }
}
