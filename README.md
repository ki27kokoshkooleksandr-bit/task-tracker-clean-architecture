# Task Tracker — облік завдань (Clean Architecture)

Лабораторна робота № 5 з дисципліни «Сучасні технології проєктування».
Виконав: Кокошко Олександр, група 4КІ2.

## Предметна область

Консольний застосунок для обліку завдань: користувач може додати завдання,
переглянути список (усі або лише активні) та позначити завдання виконаним.
Застосунок побудовано за принципами Clean Architecture на C# (.NET 8).

## Політика гілок

| Гілка | Призначення |
|---|---|
| `main` | Стабільний код, лише перевірені релізи (теги `vX.Y.Z`) |
| `develop` | Інтеграційна гілка, куди зливаються всі feature-гілки |
| `feature/*` | Гілки під конкретні задачі (окремий архітектурний шар) |

Кожна feature-гілка створюється від `develop` і повертається в неї через pull request
з код-рев'ю. Повідомлення комітів оформлюються за конвенцією Conventional Commits.

## Архітектура застосунку (Clean Architecture)

Проєкт реалізує шарувату архітектуру за принципами Clean Architecture.  
Залежності спрямовані строго всередину: зовнішні шари залежать від внутрішніх, Domain не залежить ні від кого.

**Структура шарів:**

- **Presentation (CLI)** — `src/TaskTracker.Presentation/Program.cs`  
  Консольний інтерфейс та збирання залежностей (Dependency Injection).

- **Application** — `src/Application/UseCases/`  
  Use cases: CreateTaskUseCase, CompleteTaskUseCase, ListTasksUseCase.  
  Працює тільки через інтерфейси Domain.

- **Domain** — `src/Domain/`  
  Entities/TaskItem.cs та Interfaces/ITaskRepository.cs.  
  Бізнес-сутності та правила. Немає залежностей від інших шарів.

- **Infrastructure** — `src/Infrastructure/Repositories/`  
  JsonTaskRepository.cs та InMemoryTaskRepository.cs.  
  Конкретні реалізації репозиторіїв.

**Напрямок залежностей:**

| Шар              | Залежить від                        | Не залежить від                  |
|------------------|-------------------------------------|----------------------------------|
| Presentation     | Application, Infrastructure         | —                                |
| Application      | Domain (тільки інтерфейси)          | Infrastructure, Presentation     |
| Domain           | —                                   | Усіх інших шарів                 |
| Infrastructure   | Domain                              | Application, Presentation        |

**Принцип інверсії залежностей:**  
Інтерфейс `ITaskRepository` оголошений у Domain, а його реалізації (JsonTaskRepository, InMemoryTaskRepository) — в Infrastructure.  
Application-шар працює тільки з інтерфейсом, тому спосіб зберігання даних можна замінити без зміни бізнес-логіки.

---

## Збірка та запуск

### Вимоги
- .NET 8.0 / .NET 10.0 SDK

### Команди

```bash
# Клонування
git clone https://github.com/ki27kokoshkooleksandr-bit/task-tracker-clean-architecture.git
cd task-tracker-clean-architecture

# Відновлення залежностей і збірка
dotnet restore
dotnet build

# Запуск
dotnet run --project src/TaskTracker.Presentation
