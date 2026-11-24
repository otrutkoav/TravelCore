# Локальный запуск TourCore

## Требования

- Docker Desktop с запущенным Docker Engine;
- Docker Compose v2.

Локальный .NET SDK и установленный PostgreSQL для контейнерного запуска не нужны.

## Первый запуск

Сначала из каталога `TourCoreIdentity`:

```powershell
Copy-Item .env.example .env
docker compose up --build -d
```

Затем из каталога `TourCore`:

```powershell
Copy-Item .env.example .env
docker compose up --build
```

Перед использованием измените пароль PostgreSQL. Одинаковый
`TOURCORE_JWT_SIGNING_KEY` длиной не менее 32 символов должен быть настроен в
`.env` Identity и Core.

Compose выполняет запуск в следующем порядке:

1. запускает PostgreSQL и ждёт успешного `pg_isready`;
2. запускает образ API с аргументом `--migrate-only`;
3. применяет все EF Core migrations и системные начальные данные;
4. запускает Catalog API. Identity работает отдельным сервисом и отдельным Compose-проектом.

Catalog Swagger доступен по адресу `http://localhost:8080/swagger`, Swagger
отдельного Identity — `http://localhost:8081/swagger`. В Catalog Swagger кнопка `Authorize`
принимает логин и пароль, после чего получает JWT через Identity API.
Порт API и PostgreSQL можно изменить в `.env`.

Технические endpoints после запуска:

- `http://localhost:8080/health/live`;
- `http://localhost:8080/health/ready`;
- `http://localhost:8081/health/live` — Identity;
- `http://localhost:9464/metrics` — метрики OpenTelemetry Collector для Prometheus.

## Повторный запуск

```powershell
docker compose up
```

Миграции идемпотентны: EF Core применяет только отсутствующие версии. Системные
типы услуг создаются существующей migration `SeedStandardServiceTypes` ровно один раз.

## Проверка состояния

```powershell
docker compose ps
docker compose logs migrate
docker compose logs api
```

Контейнер `migrate` должен завершиться с кодом `0`; это нормальное состояние.

## Остановка

```powershell
docker compose down
```

Эта команда сохраняет PostgreSQL volume. Для полностью чистой базы volume нужно
удалять явно командой `docker compose down --volumes`; все локальные данные при
этом будут потеряны.

## Запуск миграций без Compose

Специальный режим API использует обычные configuration providers ASP.NET Core:

```powershell
dotnet run --project TourCore.Admin.Api -- --migrate-only
```

Connection string берётся из `ConnectionStrings:TourCoreDb`, поэтому пароль не
должен храниться в репозитории: используйте User Secrets или environment variable
`ConnectionStrings__TourCoreDb`.
