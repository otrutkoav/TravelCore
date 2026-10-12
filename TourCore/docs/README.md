# Документация TourCore

Архитектурные документы:

- [`../ARCHITECTURE.md`](../ARCHITECTURE.md) — обязательные правила структуры и зависимостей;
- [`MODULES.md`](MODULES.md) — карта Core, бизнес-модулей и запускаемых сервисов;
- [`DATA-ARCHITECTURE.md`](DATA-ARCHITECTURE.md) — владельцы данных, базы, кеши и межмодульное взаимодействие;
- [`DECISIONS.md`](DECISIONS.md) — принятые решения, временные реализации и отложенные вопросы;
- [`domain-mapping.txt`](domain-mapping.txt) — рабочая карта существующих доменных типов;
- [`CODING-STYLE.md`](CODING-STYLE.md) — обязательный стиль кода, Swagger-документации и обработки ошибок.
- [`TOURS.md`](TOURS.md) — модель тура, состав услуг и хранение ограничений авиаперелётов.
- [`TARGET-SERVICE-ARCHITECTURE.md`](TARGET-SERVICE-ARCHITECTURE.md) — проект перехода от общего предметного Core к независимым сервисным контекстам, их состав и порядок реализации.
- [`SERVICE-CATALOG.md`](SERVICE-CATALOG.md) — полный каталог бизнес-сервисов, интеграционных контуров, кешей и платформенных компонентов с их ответственностью и приоритетами.
- [`LOCAL-DEVELOPMENT.md`](LOCAL-DEVELOPMENT.md) — воспроизводимый запуск PostgreSQL, миграций и API через Docker Compose.
- [`OBSERVABILITY.md`](OBSERVABILITY.md) — health checks, JSON-логи, trace correlation, OpenTelemetry и OTLP/Prometheus.
- [`AUTHORIZATION.md`](AUTHORIZATION.md) — JWT Resource Server, policies, роли и переход на внешний Identity Provider.
- [`SERVICE-TEMPLATE-V1.md`](SERVICE-TEMPLATE-V1.md) — первая версия шаблона нового сервиса: 12 архитектурных требований, критерии приёмки и оценка текущего TourCore.

При противоречии обязательные правила из `ARCHITECTURE.md` имеют приоритет. Существенное изменение границ Core, модулей, инфраструктуры или владения данными должно сопровождаться обновлением этих документов.
