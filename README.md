# MES App - Система управления производством

Веб-приложение для управления производственными процессами на базе Blazor Server (.NET 8) с поддержкой нескольких провайдеров баз данных.

## 🗄️ Поддерживаемые базы данных

- **SQLite** (по умолчанию для разработки)
- **PostgreSQL** (рекомендуется для продакшена)
- **Microsoft SQL Server**

## 🛠️ Настройка окружения

### Разработка (Development)
По умолчанию использует SQLite базу данных `mes_dev.db` в корне проекта.

```bash
# Запуск в режиме разработки
dotnet run
# или
dotnet run --launch-profile "http"
```

### Продакшен (Production)
Использует настройки из `appsettings.Production.json`.

```bash
# Запуск в режиме продакшена с HTTPS
dotnet run --launch-profile "https"
```

## ⚙️ Конфигурация базы данных

### Изменение провайдера БД

Отредактируйте соответствующий файл настроек:

**Для разработки:** `appsettings.json`
```json
{
  "Database": {
    "Provider": "Sqlite",
    "ConnectionString": "Data Source=mes_dev.db"
  }
}
```

**Для продакшена:** `appsettings.Production.json`
```json
{
  "Database": {
    "Provider": "Postgres",
    "ConnectionString": "Host=192.168.1.100;Database=mesapp_prod;Username=mesuser;Password=your_password;Port=5432"
  }
}
```

### Примеры строк подключения

#### PostgreSQL
```
Host=192.168.1.100;Database=mesapp_prod;Username=mesuser;Password=your_password;Port=5432
```

#### SQL Server
```
Server=192.168.1.100,1433;Database=mesapp_prod;User Id=mesuser;Password=your_password;TrustServerCertificate=true
```

#### SQLite
```
Data Source=mes_database.db
```

## 📊 Миграции базы данных

### Создание новой миграции
```bash
dotnet ef migrations add InitialCreate
```

### Применение миграций
```bash
# Для текущего окружения
dotnet ef database update

# Для конкретного окружения
ASPNETCORE_ENVIRONMENT=Production dotnet ef database update
```

### Просмотр миграций
```bash
dotnet ef migrations list
```

## 🚀 Команды запуска

### Локальная разработка
```bash
# HTTP (Development)
dotnet run

# С указанием профиля
dotnet run --launch-profile "http"
```

### Продакшен
```bash
# HTTPS (Production)
dotnet run --launch-profile "https"
```

### Первый запуск в продакшене
```bash
# 1. Настройте строку подключения в appsettings.Production.json
# 2. Примените миграции
ASPNETCORE_ENVIRONMENT=Production dotnet ef database update

# 3. Запустите приложение
dotnet run --launch-profile "https"
```

## 🔧 Переменные окружения

- `ASPNETCORE_ENVIRONMENT` - Окружение (Development/Production)
- `ASPNETCORE_URLS` - URL для прослушивания

Пример запуска с переменными:
```bash
ASPNETCORE_ENVIRONMENT=Production ASPNETCORE_URLS="https://0.0.0.0:5013" dotnet run
```

## 📁 Структура файлов конфигурации

```
MesApp/
├── appsettings.json                 # Настройки для Development
├── appsettings.Production.json      # Настройки для Production
├── Properties/
│   └── launchSettings.json          # Профили запуска
└── mes_dev.db                       # SQLite база (создается автоматически)
```

## 🌐 Сетевая настройка

### Локальная сеть
Для доступа из локальной сети измените URL в `launchSettings.json`:
```json
"applicationUrl": "https://0.0.0.0:5013;http://0.0.0.0:5012"
```

### Firewall
Убедитесь что порты открыты:
- **5012** - HTTP
- **5013** - HTTPS

## 📋 Горячие клавиши

- **Ctrl+S** - Сохранить форму
- **F5** - Обновить список

## 🧪 Тестирование

```bash
# Запуск тестов (если есть)
dotnet test

# Сборка проекта
dotnet build

# Очистка
dotnet clean
```

## 🔒 Безопасность

### HTTPS Сертификаты
Для разработки:
```bash
dotnet dev-certs https --trust
```

### Продакшен
- Используйте валидные SSL сертификаты
- Настройте secure connection strings
- Ограничьте доступ к базе данных

## 📖 Дополнительная информация

### Модули системы
- **Warehouse** - Управление складом
- **QC** - Контроль качества
- **Lab** - Лабораторные испытания
- **OPP** - Подготовка производства

### Роли пользователей
- **WAREHOUSE** - Складские операции
- **QC** - Входной контроль
- **LAB** - Центральная заводская лаборатория
- **OPP** - Отдел подготовки производства

## 🐛 Устранение проблем

### База данных недоступна
1. Проверьте строку подключения
2. Убедитесь что сервер БД запущен
3. Проверьте сетевое подключение
4. Выполните `dotnet ef database update`

### Ошибки миграции
```bash
# Сброс миграций
dotnet ef database drop
dotnet ef database update
```

### Проблемы с HTTPS
```bash
# Переустановка dev сертификатов
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

## 📝 Логирование

Логи настроены в `appsettings.json`:
- **Development**: Подробные логи
- **Production**: Только важные события

## 📧 Поддержка

При возникновении проблем проверьте:
1. Версию .NET 8
2. Строки подключения к БД
3. Права доступа к файлам
4. Сетевые настройки