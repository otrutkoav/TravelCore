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

Общие провайдеры:
TourCore.Infrastructure.SqlServer  # .NET Framework 4.8, EF6, SQL Server
TourCore.Infrastructure.Postgres   # современный .NET, EF Core, PostgreSQL

Отдельные host'ы:
TourCore.Admin.Api.Legacy
TourCore.Search.Api.Legacy
TourCore.Admin.Api
TourCore.Search.Api                # в будущем
TourCore.Booking.Api.*             # в будущем
```

Core включает географию, базовые модели отелей, транспорта, финансовые и другие фундаментальные справочники. Search, Booking, Quotas, Pricing и последующие области оформляются отдельными модульными Contracts/Application/Domain-проектами. Модуль может зависеть от устойчивых контрактов Core, но Core никогда не зависит от модулей.

Admin API использует Core Application. Search API использует `TourCore.Search.Application` и `TourCore.Search.Contracts`. Реализация SQL-поиска находится в общем провайдере под namespace `TourCore.Infrastructure.SqlServer.Search`.

SQL Server и PostgreSQL остаются отдельными проектами из-за несовместимых runtime и ORM-зависимостей, но каждый является общим провайдером для Admin, Search и будущего Booking.

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

## Совместимость Legacy и Modern

`TourCore.Shared`, Contracts, Domain и Application должны собираться для `netstandard2.0`, пока поддерживается .NET Framework 4.8. В них нельзя использовать API, доступные только в современных версиях .NET, или добавлять зависимости, несовместимые с `net48`.

Различаться могут только host и Infrastructure:

```text
Api.Legacy + EF6 + SQL Server
Api        + EF Core + PostgreSQL
```

Обе реализации проходят через одинаковые Application-абстракции. Добавление PostgreSQL не создаёт второй handler с бизнес-логикой.

## Версионирование API

Канонический маршрут Legacy API содержит версию: `/api/v1/...`. Старые `/api/...` временно поддерживаются внутренним compatibility handler и не публикуются как основной контракт в Swagger. Новая несовместимая версия создаётся отдельным `/api/v2/...`, без изменения поведения v1.

## Legacy NuGet и runtime-зависимости

Для классических проектов `packages.config` транзитивные зависимости должны быть явно зафиксированы в `packages.config`, `.csproj` и `Web.config` binding redirects. Проверка выполняется после чистой сборки:

```powershell
& "$env:ProgramFiles\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" TourCore.sln /t:Rebuild /p:Configuration=Debug
.\Verify-LegacyRuntime.ps1
```

Нельзя считать incremental build достаточной проверкой: старые DLL в `bin` могут скрывать отсутствующую зависимость.
