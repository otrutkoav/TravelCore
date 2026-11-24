# TourCore Identity

Самостоятельный сервис локальной аутентификации и выпуска JWT для сервисов TravelCore.
Он не входит в Core и не зависит от его проектов или базы данных.

## Потребители

- `TourCore.Admin.Api`, audience `TourCore.Admin.Api`, роли `CatalogReader`,
  `CatalogEditor`, `CatalogAdmin`;
- `TourCore.Pricing.Api`, audience `TourCore.Pricing.Api`, роли `PricingReader`,
  `PricingEditor`, `PricingAdmin`.

Один токен может содержать обе audience и оба набора ролей. Каждый resource server
проверяет свою audience и применяет собственные authorization policies.

## Локальный запуск

```powershell
dotnet run --project src/TourCore.Identity.Api
```

- HTTPS: `https://localhost:7180`;
- Swagger: `https://localhost:7180/swagger`;
- OAuth2 token endpoint: `POST https://localhost:7180/connect/token`;
- JSON login endpoint: `POST https://localhost:7180/api/v1/auth/login`;
- health check: `GET https://localhost:7180/health/live`.

Development-пользователь: `admin`. Локальный пароль хранится как PBKDF2-хеш в
`appsettings.Development.json`; production/Compose получает секреты только из
переменных окружения или secret storage.

## Docker Compose

```powershell
Copy-Item .env.example .env
docker compose up --build
```

## Соответствие Service Template v1

| № | Область | Статус | Вывод |
|---:|---|---|---|
| 1 | Host и наблюдаемость | Частично | JSON logs и liveness есть; OpenTelemetry и request correlation отсутствуют |
| 2 | Аутентификация | Частично | Локальный multi-audience JWT issuer; не production OIDC/IAM |
| 3 | Кеширование | Не применяется | Авторитетных данных и cache-сценария нет |
| 4 | Несколько хранилищ | Не применяется | Сервис не использует БД |
| 5 | Инфраструктура контекста | Не применяется | Persistence adapter отсутствует осознанно |
| 6 | Бизнес-модуль | Частично | Малый сервис оправданно в одной assembly; при росте потребуется разделение |
| 7 | DI | Соответствует | Microsoft DI и отдельная регистрация issuer/verifier |
| 8 | gRPC | Не применяется | RPC-сценария нет |
| 9 | Брокер | Не применяется | Асинхронного процесса нет |
| 10 | Тесты | Частично | Unit-тесты JWT/PBKDF2 есть; HTTP/security component tests отсутствуют |
| 11 | Контейнеризация | Соответствует | Собственные Dockerfile, Compose, health check, non-root user |
| 12 | Контракты | Частично | HTTP OAuth/JSON контракт есть, отдельной contracts assembly и compatibility tests нет |

Полная сравнительная оценка всех сервисов находится в
[`../docs/architecture/SERVICE-AUDIT-2026-11-24.md`](../docs/architecture/SERVICE-AUDIT-2026-11-24.md).
