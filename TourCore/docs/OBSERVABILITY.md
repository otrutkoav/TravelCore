# Наблюдаемость TourCore

## Health checks

- `GET /health/live` проверяет, что процесс API способен отвечать на HTTP-запросы;
- `GET /health/ready` проверяет соединение с PostgreSQL и отсутствие неприменённых migrations.

Оба endpoint возвращают JSON. Неисправная readiness-проверка отвечает HTTP `503`,
но не делает liveness-проверку неуспешной. Поэтому временная недоступность БД не
создаёт цикл перезапусков исправного процесса API.

## Структурированные логи

Консольные логи записываются в JSON и содержат структурированные поля, включая:

- category и log level;
- HTTP method, path, status code и duration;
- W3C `TraceId`, `SpanId` и `ParentId`;
- request identifier;
- exception, если она возникла.

API возвращает `X-Trace-Id` в каждом HTTP-ответе. Это позволяет найти связанные
логи и distributed trace по идентификатору из клиентского запроса.

## OpenTelemetry

Инструментируются:

- входящие ASP.NET Core-запросы;
- исходящие `HttpClient`-запросы;
- EF Core/PostgreSQL spans;
- ASP.NET Core, HTTP и .NET runtime metrics;
- структурированные application logs.

Экспорт включается, если задан `OpenTelemetry:OtlpEndpoint` или environment
variable `OpenTelemetry__OtlpEndpoint`. В Compose используется
`http://otel-collector:4317`.

Локальный OpenTelemetry Collector:

- принимает OTLP/gRPC на `localhost:4317`;
- принимает OTLP/HTTP на `localhost:4318`;
- публикует Prometheus metrics на `http://localhost:9464/metrics`;
- выводит принятые traces и logs в собственный контейнерный лог.

Проверка:

```powershell
Invoke-WebRequest http://localhost:8080/health/live
Invoke-WebRequest http://localhost:8080/health/ready
Invoke-WebRequest http://localhost:9464/metrics
docker compose logs otel-collector
```

Collector является точкой расширения: вместо debug exporter к нему позднее можно
подключить Jaeger, Grafana Tempo, Loki, Elasticsearch или корпоративный OTLP backend,
не меняя код Catalog API.
