# TourCore Pricing

Самостоятельный сервис управления ценообразованием. Сервис владеет своей моделью,
таблицами и миграциями, не зависит от проектов основного `TourCore`, но использует
общую PostgreSQL-базу `TourCoreDb`.

Таблица первого среза называется `PricePackages`; имена таблиц и колонок следуют
PascalCase-стилю существующей схемы TourCore.

Реализованы ценовые пакеты, ценовые блоки, гостиничные правила и компоненты,
правила питания, цены авиационных сегментов, автобусов и железнодорожных переездов.

## Структура

- `src/TourCore.Pricing.Domain` — агрегаты и бизнес-инварианты;
- `src/TourCore.Pricing.Application` — сценарии и порты инфраструктуры;
- `src/TourCore.Pricing.Contracts` — публичные HTTP DTO;
- `src/TourCore.Pricing.Infrastructure.Postgres` — EF Core, PostgreSQL и миграции;
- `src/TourCore.Pricing.Api` — HTTP host, JWT, Swagger, health checks и telemetry;
- `tests` — unit, architecture и host smoke tests.

## API

Базовый ресурс первого среза: `/api/pricing/packages`.

- `GET /api/pricing/packages` — список без удалённых записей; фильтры `countryId`,
  `isDisabled`, `search`;
- `GET /api/pricing/packages/{id}` — пакет по ID;
- `POST /api/pricing/packages` — создание;
- `PUT /api/pricing/packages/{id}` — изменение;
- `DELETE /api/pricing/packages/{id}` — мягкое удаление.

Справочник типов будущих ценовых блоков сохраняет историческое имя `SPOTypes` и
доступен через CRUD `/api/pricing/spo-types`. `SPOTypes.Id` генерируется PostgreSQL
и в будущем используется как FK `PriceBlock.SPOTypeId`.

В Domain также закреплён общий enum `CostOfferState` со значениями `New`,
`InChecked`, `ActivatedForNetto`, `Activated`, `Deactivated`.

Гостиничные цены, формульные компоненты и отдельные правила питания описаны в
[документации модели](docs/HOTEL_PRICING.md).

Цены отдельных авиационных сегментов описаны в
[документации авиационных цен](docs/FLIGHT_PRICING.md).

Автобусные и железнодорожные цены описаны в
[документации транспортных цен](docs/TRANSPORT_PRICING.md).

Ценовые блоки доступны через CRUD `/api/pricing/blocks`. Блок содержит прямой
`PricePackageId`; промежуточная таблица наподобие legacy `CostOfferPackets` не
используется. Изменение состояния выполняется отдельно через
`PUT /api/pricing/blocks/{id}/state` и автоматически фиксирует пользователя и дату.

Создатель берётся из JWT claim `sub`. Для доступа используются политики
`Pricing.Read`, `Pricing.Write`, `Pricing.Delete` и роли `PricingReader`,
`PricingEditor`, `PricingAdmin`. Issuer должен выпускать токен для audience
`TourCore.Pricing.Api`.

`CountryId` — стабильная ссылка на Catalog без межсервисного foreign key. Проверка
существования страны требует отдельного межсервисного контракта и сознательно не
входит в первый срез.

## Ошибки связанных справочников

API не полагается только на выбор значений в UI. При создании или изменении цены
оно дополнительно проверяет существование связанных объектов Catalog и возвращает
`404` со стабильным машинным кодом. Например:

```json
{
  "Code": "hotel_not_found",
  "Message": "Отель с идентификатором '123' не найден.",
  "Details": null
}
```

Аналогично возвращаются `room_type_not_found`, `room_category_not_found`,
`accommodation_type_not_found`, `meal_type_not_found`, `charter_not_found`,
`air_class_not_found`, `bus_transfer_point_not_found`,
`railway_transfer_not_found` и `transport_not_found`. Ошибки формы и несовместимые
параметры остаются `400 validation_error`.

## Локальный запуск

```powershell
Copy-Item .env.example .env
docker compose up --build
```

Swagger: `http://localhost:8082/swagger`.

Health endpoints:

- `GET /health/live`;
- `GET /health/ready` — подключение к PostgreSQL и отсутствие ожидающих миграций.

В `.env` задаётся `TOURCORE_DB_CONNECTION_STRING` — строка подключения к уже
существующей базе `TourCoreDb`. Compose не создаёт отдельный PostgreSQL.
Миграции запускаются отдельным одноразовым контейнером `migrate`; API сам не
изменяет схему при обычном старте.

## Проверка

```powershell
dotnet tool restore
dotnet build TourCorePricing.sln
dotnet test TourCorePricing.sln --no-build
```

PostgreSQL integration test запускается при наличии переменной
`PRICING_TEST_POSTGRES`. Имя выделенной тестовой базы обязано содержать `test`;
иначе тест откажется выполнять изменения.

## Соответствие Service Template v1

| № | Область | Статус | Решение |
|---:|---|---|---|
| 1 | Host и наблюдаемость | Частично | Structured logs, OTLP и health checks есть; typed Options унифицированы не полностью |
| 2 | Аутентификация | Частично | JWT Resource Server и policies есть; production OIDC/JWKS и machine identity отсутствуют |
| 3 | Кеширование | Не применяется | Для CRUD пакетов сценарий не требует кеша |
| 4 | Несколько хранилищ | Не применяется | Общая TourCoreDb, отдельный Pricing DbContext |
| 5 | Собственная инфраструктура | Частично | Собственные DbContext/migrations; Catalog-ссылки временно проверяются прямым SQL в общей БД |
| 6 | Бизнес-модуль | Соответствует | Domain и Application не зависят от EF/API |
| 7 | DI | Соответствует | Microsoft DI и явная регистрация |
| 8 | gRPC | Не применяется | Синхронного межсервисного сценария пока нет |
| 9 | Брокер | Не применяется | Асинхронного процесса пока нет |
| 10 | Тесты | Частично | Unit/architecture/liveness и один repository integration test; нет полного API/auth и persistence coverage |
| 11 | Контейнеризация | Соответствует | Dockerfile, Compose, migrator, health checks |
| 12 | Контракты | Соответствует | Независимая Contracts assembly |

Полная сравнительная оценка всех сервисов находится в
[`../docs/architecture/SERVICE-AUDIT-2026-11-24.md`](../docs/architecture/SERVICE-AUDIT-2026-11-24.md).
