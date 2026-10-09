# Архитектура TourCore

Связанные документы:

- [`docs/MODULES.md`](docs/MODULES.md) — карта Core и модулей;
- [`docs/DATA-ARCHITECTURE.md`](docs/DATA-ARCHITECTURE.md) — базы, владельцы данных и кеши;
- [`docs/DECISIONS.md`](docs/DECISIONS.md) — принятые и отложенные решения;
- [`docs/README.md`](docs/README.md) — индекс документации.

## Общая модель

TourCore состоит из предметного Core, отдельных бизнес-модулей, общих инфраструктурных провайдеров и отдельных запускаемых API/Service host'ов. Core содержит базовые туристические сущности, без которых остальные модули не имеют смысла. Admin является интерфейсом управления Core, а не отдельным бизнес-ядром.

```text
Предметный Core:
TourCore.Contracts
TourCore.Application
TourCore.Domain

Технический фундамент:
TourCore.Shared

Отдельные бизнес-модули:
TourCore.Search.Contracts
TourCore.Search.Application
TourCore.Booking.*          # в будущем
TourCore.Quotas.*           # в будущем
TourCore.*Pricing.*         # в будущем

Общий провайдер:
TourCore.Infrastructure.Postgres   # .NET 8, EF Core, PostgreSQL

Отдельные host'ы:
TourCore.Admin.Api
TourCore.Search.Api                # в будущем
TourCore.Booking.Api.*             # в будущем
```

Core включает географию, базовые модели отелей, транспорта, финансовые и другие фундаментальные справочники. Search, Booking, Quotas, Pricing и последующие области оформляются отдельными модульными Contracts/Application/Domain-проектами. Модуль может зависеть от устойчивых контрактов Core, но Core никогда не зависит от модулей.

Admin API использует Core Application. Будущий Search API использует
`TourCore.Search.Application` и `TourCore.Search.Contracts`. Источник поисковых
данных должен быть реализован поверх PostgreSQL или отдельного SearchCache.

PostgreSQL является единственным целевым транзакционным provider-ом. SQL Server
может использоваться внешним инструментом миграции только как источник старых
данных и не входит в runtime TourCore.

## Разрешённые зависимости

```text
Api -> Application, Contracts, Shared, Infrastructure
Infrastructure -> Application, Domain, Contracts, Shared
Application -> Domain, Contracts, Shared
Domain -> Shared
Contracts -> Shared или без зависимостей
Shared -> без ссылок на проекты решения
```

Запрещены зависимости `Application -> Api`, `Application -> Infrastructure`, `Infrastructure -> Api`, ссылки Core на бизнес-модули и любые project references из `TourCore.Shared`. Правила проверяются автоматически файлом `Directory.Build.targets` при сборке.

## Правило для TourCore.Shared

В `TourCore.Shared` допускаются только небольшие стабильные технические типы:

- handler-интерфейсы;
- базовые Application-исключения;
- стандартный контракт API-ошибки и его mapper;
- общие примитивы результата, пагинации и value objects без предметной принадлежности.

В `TourCore.Shared` запрещены бизнес-сущности и правила Admin/Search/Booking, EF, SQL, HTTP-host configuration, кеши конкретного контура и ссылки на другие проекты решения. Если тип имеет смысл только для одного контура, он остаётся в этом контуре.

## Целевая платформа

Все backend-проекты собираются для `.NET 8`. Единственная runtime-композиция —
ASP.NET Core + EF Core + PostgreSQL. Application и Domain по-прежнему не зависят
от ORM и конкретной СУБД; эта граница нужна для тестируемости и смены
инфраструктурных реализаций, а не для поддержки устаревшего host-а.

## Версионирование API

Канонический маршрут API содержит версию: `/api/v1/...`. Новая несовместимая
версия создаётся отдельным `/api/v2/...`, без молчаливого изменения поведения v1.
