# Стиль кода TourCore

## Общие правила

- Используется отступ в четыре пробела; несколько операторов и объявлений в одной строке запрещены.
- Папки отражают предметную область и ответственность: `Commands`, `Queries`, `Handlers`, `Validators`, `Mappings`, `DTOs`, `Entities`, `Configurations`, `Repositories`.
- API содержит только HTTP-адаптацию. Валидация сценария и бизнес-правила находятся в Application и Domain.
- Handler использует полные имена зависимостей и приватные поля с префиксом `_`.
- Ошибки отсутствующих ресурсов оформляются как `NotFoundException` с общими `ErrorMessages` и `ErrorCode`; ошибки входных данных — как `ValidationException`.
- Публичные команды, DTO, параметры и результаты контроллеров документируются XML-комментариями для Swagger.
- Контроллеры должны оставаться тонкими и передавать сценарий Application-обработчику.
- Изменение структуры тура или его услуги увеличивает соответствующий `Revision`.

## Границы зависимостей

- Domain не зависит от Application, Infrastructure или API.
- Contracts не зависит от Domain, Application, Infrastructure или API.
- Application зависит от Domain и Contracts, но не от EF, SQL Server, PostgreSQL и API.
- Infrastructure реализует Application-абстракции и содержит настройки конкретного провайдера.
- API не содержит бизнес-правил и работает через Application handlers.

## Проверка перед завершением задачи

1. XML-комментарии отображаются в Swagger обоих Admin API.
2. Нет однострочных классов, методов с несколькими операторами в строке и сокращённых имён зависимостей.
3. EF Core является деталью PostgreSQL Infrastructure и не проникает в Application/Domain.
4. Полная сборка `TourCore.sln` завершается без ошибок и предупреждений.
