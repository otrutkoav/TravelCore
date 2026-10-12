# JWT-аутентификация и авторизация

`TourCore.Admin.Api` является JWT Resource Server: он проверяет access token, но
не хранит пользователей и не проверяет пароли. Токены для текущего вертикального
среза выдаёт отдельный `TourCore.Identity.Api`. В дальнейшем его реализацию можно
заменить на Keycloak, Microsoft Entra ID или полноценный Identity-сервис, не меняя
Catalog policies и контроллеры.

## Policies

| Policy | Начальные роли | Назначение |
|---|---|---|
| `Catalog.Read` | `CatalogReader`, `CatalogEditor`, `CatalogAdmin` | Чтение Catalog |
| `Catalog.Write` | `CatalogEditor`, `CatalogAdmin` | Создание и изменение Catalog |
| `Catalog.Delete` | `CatalogAdmin` | Удаление данных Catalog |
| `Catalog.Finance.Write` | `CatalogEditor`, `CatalogAdmin` | Изменение финансовых справочников |

Catalog-контроллеры получают политики централизованно по HTTP-операции. Product
endpoints пока защищены fallback policy и требуют любого валидного JWT.

Контроллеры зависят от policies, а не от бизнес-названий ролей. Поэтому позднее
можно сопоставить `Administrator`, `Manager`, `Accountant` и `PricingSpecialist`
с этими разрешениями без изменения endpoints.

JWT должен содержать claim `role`. Несколько ролей передаются несколькими claims
с одинаковым именем.

## Локальная симметричная подпись

Настройки:

```text
Authentication__Jwt__Issuer=TourCore.Local
Authentication__Jwt__Audience=TourCore.Admin.Api
Authentication__Jwt__SigningKey=<секрет не короче 32 символов>
Authentication__Jwt__RequireHttpsMetadata=false
```

Секрет нельзя коммитить. Для локального запуска он хранится в `.env`, который
создаётся из `.env.example`, или в .NET User Secrets.

Профили Visual Studio с `ASPNETCORE_ENVIRONMENT=Development` используют отдельный
ключ `TourCore.Development.SigningKey.NotForProduction.2026` из
`appsettings.Development.json`. Он предназначен только для локальной разработки,
не является production-секретом и не загружается в Production environment.

Пример генерации стойкого секрета PowerShell:

```powershell
$bytes = [byte[]]::new(48)
[Security.Cryptography.RandomNumberGenerator]::Fill($bytes)
[Convert]::ToBase64String($bytes)
```

## Внешний Identity Provider

Для перехода на внешний issuer задаются:

```text
Authentication__Jwt__Authority=https://identity.example.com/realms/tourcore
Authentication__Jwt__Audience=TourCore.Admin.Api
Authentication__Jwt__RequireHttpsMetadata=true
```

`SigningKey` в этом режиме не нужен: ключи загружаются через metadata/JWKS
Identity Provider.

## Swagger

При одновременном запуске `TourCore.Identity.Api` и `TourCore.Admin.Api` Swagger
Catalog содержит OAuth2 password flow. Нажмите `Authorize`, введите локальные
логин и пароль, после чего Swagger самостоятельно получит JWT и добавит заголовок
`Authorization: Bearer <token>` к запросам.

Локальные development-данные:

```text
Identity URL: https://localhost:7180
Username: admin
Password: TourCore.Admin.2026!
```

Пароль существует только для профиля `Development`. В репозитории хранится его
PBKDF2-SHA256 хеш, а не исходное значение. Для Compose логин и пароль задаются
через `TOURCORE_ADMIN_USERNAME` и `TOURCORE_ADMIN_PASSWORD` в `.env`.

Прямой JSON endpoint входа:

```http
POST /api/v1/auth/login
Content-Type: application/json

{"username":"admin","password":"TourCore.Admin.2026!"}
```

`POST /connect/token` предоставляет OAuth2-совместимый form endpoint для Swagger.
Access token действует 60 минут. Refresh tokens, база пользователей, блокировка
учётной записи и отзыв токенов в этот минимальный срез пока не входят.

Health endpoints `/health/live` и `/health/ready` намеренно доступны без токена.
