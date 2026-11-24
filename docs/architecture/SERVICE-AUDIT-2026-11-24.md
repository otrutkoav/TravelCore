# Аудит сервисов TravelCore по Service Template v1

**Дата:** 24 ноября 2026 года  
**Шаблон:** `TourCore/docs/SERVICE-TEMPLATE-V1.md`  
**Объекты:** `TourCore`, `TourCorePricing`, `TourCoreIdentity`

## Итог

Все три deployable-сервиса имеют определённые границы, независимые solution и
Compose-проекты. Core и Pricing являются JWT resource server, а Identity вынесен
в самостоятельный issuer. Ни одному текущему сценарию пока не нужны кеш, gRPC или
брокер, поэтому эти возможности оценены как «не применяется», а не как дефекты.

Главные долги:

1. Identity не подключён к OpenTelemetry и имеет только liveness.
2. Core всё ещё объединяет Catalog и Product одним `DbContext` и набором migrations.
3. Pricing проверяет Catalog-ссылки прямыми SQL-запросами к чужим таблицам общей БД.
4. API/component и integration tests покрывают только часть сценариев и security flow.
5. Production identity пока не имеет OIDC discovery/JWKS, ротации ключей, refresh,
   revocation и service-to-service credentials.

## Сводная матрица

| № | Область | TourCore | TourCorePricing | TourCoreIdentity |
|---:|---|---|---|---|
| 1 | Host, конфигурация, логи, telemetry | Частично | Частично | Частично |
| 2 | Authentication/authorization | Частично | Частично | Частично |
| 3 | Кеширование | Частично | Не применяется | Не применяется |
| 4 | Несколько хранилищ | Не применяется | Не применяется | Не применяется |
| 5 | Инфраструктура контекста | Частично | Частично | Не применяется |
| 6 | Модуль бизнес-логики | Соответствует | Соответствует | Частично |
| 7 | DI | Соответствует | Соответствует | Соответствует |
| 8 | gRPC | Не применяется | Не применяется | Не применяется |
| 9 | Брокер сообщений | Не применяется | Не применяется | Не применяется |
| 10 | Тесты | Частично | Частично | Частично |
| 11 | Docker/локальное окружение | Соответствует | Соответствует | Соответствует |
| 12 | Контракты и слабая связанность | Соответствует | Соответствует | Частично |

## 1. Host, конфигурация, логи и telemetry

### TourCore — частично

Admin API имеет структурированные JSON-логи, trace/span correlation, OTLP traces,
metrics и logs, PostgreSQL readiness и отдельный liveness. Миграции запускаются
отдельным режимом `--migrate-only`. JWT проверяется при старте, но connection string
и telemetry settings не оформлены единым набором типизированных Options с
`ValidateOnStart`. Reusable host-defaults между сервисами отсутствуют.

### Pricing — частично

Практически повторяет наблюдаемость Core и имеет readiness с проверкой ожидающих
migrations. Сохраняется тот же долг по типизированным Options. Дублирование host
infrastructure с Core создаёт риск расхождения поведения.

### Identity — частично

Есть composition root, JSON-логи, CORS только для настроенных доверенных origin и
liveness. JWT/user settings валидируются при регистрации. Нет OpenTelemetry,
trace-enriched request logging, единого JSON health response и readiness. Внешних
обязательных зависимостей сейчас нет, поэтому отдельная dependency-readiness
проверка не требуется.

## 2. Authentication и authorization

### Core и Pricing — частично

Оба API валидируют issuer, audience, lifetime и signature и применяют policies.
Swagger получает токен через внешний `TourCoreIdentity`. Для production остаются
симметричный общий ключ без ротации/JWKS и отсутствие service-to-service identity.

### Identity — частично

Issuer поддерживает несколько audience, PBKDF2-проверку пароля и отдельные роли
Catalog/Pricing. Это локальный development provider, а не production IAM: password
flow, один пользователь, нет refresh/revocation, client credentials, OIDC discovery,
JWKS, MFA, audit storage и блокировки учётной записи.

## 3. Кеширование

Core имеет cache ports и `NoCache` adapter, поэтому граница подготовлена, но правила
TTL/invalidation/versioning ещё не подтверждены реализацией. В Pricing и Identity
авторитетные данные пока читаются напрямую, а подтверждённого cache-сценария нет.

## 4. Несколько контекстов и хранилищ

Ни один сервис сейчас не нуждается в нескольких runtime-хранилищах. Core и Pricing
используют PostgreSQL; Identity не использует БД. SQL Server остаётся только legacy
источником анализа/миграции. Добавлять MongoDB или второй provider без сценария не надо.

## 5. Собственная инфраструктура контекста

Core отделяет repository ports от EF, но Catalog и Product используют общие
`TourCoreDbContext`, infrastructure assembly и migrations — статус частичный.

Pricing владеет `PricingDbContext`, migrations и repository adapters, но физически
работает в общей `TourCoreDb` и проверяет ссылки на Hotel/Charter/Transport прямыми
SQL-запросами к таблицам Catalog. Это сознательная переходная интеграция, которая
не должна пережить разделение БД. Нужен Catalog read contract/API либо локальная
проекция, когда появится реальная потребность в физическом разделении.

Identity не имеет persistence adapter; пункт к нему не применяется.

## 6. Модуль бизнес-логики

Core и Pricing разделены на Domain/Application/Contracts/Infrastructure/host,
Domain не зависит от EF/API. В Core Product больше не ссылается напрямую на Catalog
Domain/Application.

Identity пока оправданно мал, но token issuance, password verification, HTTP DTO и
host находятся в одной assembly. Для текущего local provider это приемлемо; перед
ростом до production IAM следует разделить protocol contracts и security application.

## 7. DI

Все сервисы используют Microsoft DI и не используют service locator в бизнес-коде.
Core/Pricing группируют регистрации по модулям. Identity регистрирует issuer и
password verifier через отдельный extension method. Критичные настройки проверяются
при старте, хотя единый Options-validation подход ещё не выработан.

## 8. gRPC

Не применяется: подтверждённого синхронного межсервисного RPC нет. Текущая связь со
Swagger/Identity использует стандартный HTTP OAuth endpoint. Добавлять `.proto` и
generated clients заранее не требуется.

## 9. Брокер сообщений

Не применяется: нет асинхронного бизнес-процесса, требующего доставки событий.
Outbox/inbox, retry/DLQ и конкретный брокер должны появиться вместе с первым таким
сценарием, а не как пустая инфраструктура.

## 10. Тесты

Core имеет application, Catalog unit и PostgreSQL integration tests, а также
MSBuild-проверки зависимостей. Не хватает Admin API auth/component smoke tests,
полного Product/Search покрытия и обязательного CI gate.

Pricing имеет Domain/Application unit tests, architecture tests, liveness smoke и
один PostgreSQL repository integration test. Не покрыты HTTP CRUD/auth/error flow,
migrations всех новых таблиц и repository integration для hotel/flight/bus/railway.

Identity имеет unit tests PBKDF2 и multi-audience JWT. Не хватает HTTP component
tests для login/token/CORS/invalid credentials и contract/security regression tests.

На текущей машине `dotnet test` не смог соединить `vstest.console` с `testhost` за
90 секунд. Проекты тестов компилируются; это не считается успешным выполнением тестов.

## 11. Docker и локальное окружение

У каждого сервиса собственные multi-stage Dockerfile, `.dockerignore`, Compose и
health check; контейнер запускается как `$APP_UID`. Core/Pricing отделяют migrations
от API. Все три Compose-конфигурации проходят `docker compose config --quiet`.
Для production ещё нужны digest pinning, resource limits, secret manager, image/SBOM
scan и orchestration manifests.

## 12. Контракты и слабая связанность

Core имеет независимые Catalog/Product/Search Contracts; Product не ссылается на
Catalog Domain/Application. Pricing Contracts не ссылается на Domain/Application/EF.
Межсервисные ссылки хранятся как стабильные ID без project reference на Core.

Identity не предоставляет отдельную contracts assembly или versioned client package;
его текущий внешний контракт — HTTP OAuth/JSON endpoints. До появления независимых
клиентов это допустимо частично, но совместимость endpoint/response необходимо
закрепить component tests до production-развития.

## Приоритет исправлений

### P0

1. Добавить Identity OpenTelemetry/request correlation и API component tests.
2. Добавить auth/component tests Admin и Pricing.
3. Зафиксировать временную прямую SQL-интеграцию Pricing → Catalog ADR-документом
   и не добавлять новые cross-context SQL-зависимости без отдельного решения.
4. Ввести единый подход к typed Options + `ValidateOnStart` для JWT, PostgreSQL,
   CORS и OpenTelemetry во всех host.

### P1 — только по реальному сценарию

1. Разделить Catalog/Product persistence, если появятся независимые deployment/SLA.
2. Добавить Catalog API/read projection для Pricing при физическом разделении БД.
3. Реализовать production OIDC/JWKS/key rotation и machine clients.
4. Добавить кеш, gRPC или брокер только при появлении соответствующей нагрузки или процесса.
