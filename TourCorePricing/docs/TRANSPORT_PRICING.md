# Цены автобусных и железнодорожных переездов

## Результаты анализа legacy

Для ЖД фактическое соответствие: `CS_CODE -> RailwayTransfers`, `CS_SUBCODE1 -> Transport`, `CS_SUBCODE2 = 0`. Комментарий старого enum о плане рассадки в `SubCode2` устарел: `Service.SV_ISSUBCODE2 = 0`, все 148 реальных ЖД-цен содержат ноль, а план рассадки выбирается независимо по транспорту и дате.

Для автобусов: `CS_CODE -> BusTransferPoints`, `CS_SUBCODE1 -> CityDictionary`, `CS_SUBCODE2 -> Transport`. Город в 155 из 158 строк совпадает с `BusTransferPoint.CityFrom`, поэтому в новой цене он не дублируется.

Таблицы разделены, поскольку они ссылаются на разные предметные сущности и развиваются независимо.

## Общие поля

| Поле | Тип | Обяз. | Назначение |
|---|---|:---:|---|
| `Id` | integer identity | да | Идентификатор цены. |
| `PriceBlockId` | integer, FK | да | Ценовой блок соответствующего типа услуги. |
| `TransportId` | integer, FK | да | Вид/класс транспорта из `Transports`. |
| `TravelDateFrom`, `TravelDateTo` | date | нет | Границы дат выполнения переезда; могут быть открытыми. |
| `DaysOfWeekMask` | smallint/flags | да | Пн=1, Вт=2, Ср=4, Чт=8, Пт=16, Сб=32, Вс=64, все=127. |
| `SaleDateFrom`, `SaleDateTo` | date | нет | Дополнительные ограничения продажи строки; базовые ограничения находятся в блоке. |
| `AgeFrom`, `AgeTo` | smallint | нет | Возраст пассажира включительно. |
| `Scope` | smallint/enum | да | `1 PerPassenger`, `2 PerService`. Во втором случае сумма не умножается на пассажиров. |
| `PeriodUnit` | smallint/enum | да | `1 PerTrip`, `2 PerDay`. |
| `NettoAmount`, `BruttoAmount` | numeric(18,4) | нет | Хотя бы одна неотрицательная сумма обязательна. |
| `CurrencyId` | integer | нет | Переопределение валюты; иначе `PriceBlock.CurrencyId`. |
| `Priority` | integer | да | Приоритет при пересечении цен. |
| `CreatedByUserId`, `CreatedAt`, `UpdatedAt` | audit | да/да/нет | Создатель и даты в UTC. |

## BusTransferPrices

Дополнительные поля:

| Поле | Назначение |
|---|---|
| `BusTransferPointId` | FK на конкретный участок/точку автобусного маршрута. Направление и города берутся из `BusTransferPoints`. |
| `PassengerCountFrom` | Минимальное количество пассажиров включительно. |
| `PassengerCountTo` | Максимальное количество пассажиров включительно. |

Блок обязан иметь `ServiceTypeId = 14`. Цена выбирается по точке, транспорту, датам, возрасту и числу пассажиров. Расписание проверяется через `BusSchedules`, но конкретное расписание в цене не хранится.

## RailwayTransferPrices

Дополнительное поле — `RailwayTransferId`, FK на направление из `RailwayTransfers`. Блок обязан иметь `ServiceTypeId = 15`.

`TrainScheduleId` и `VehiclePlanId` отсутствуют намеренно. Расписание определяет возможность выполнения направления, план рассадки — доступные места при бронировании; ни то, ни другое не меняет идентичность ценовой строки.

## API

- `/api/pricing/bus-transfer-prices` — CRUD и фильтры `priceBlockId`, `busTransferPointId`, `transportId`;
- `/api/pricing/railway-transfer-prices` — CRUD и фильтры `priceBlockId`, `railwayTransferId`, `transportId`.

При удалении ценового блока его цены удаляются каскадно. Удаление используемых справочников запрещено внешними ключами.
