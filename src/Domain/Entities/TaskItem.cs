namespace TaskTracker.Domain.Entities;

/// <summary>Завдання — центральна сутність предметної області.</summary>
public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва завдання не може бути порожньою.", nameof(title));

        Id = Guid.NewGuid();
        Title = title.Trim();
        CreatedAt = DateTime.Now;
    }

    // Конструктор для відновлення сутності зі сховища.
    public TaskItem(Guid id, string title, DateTime createdAt, bool isCompleted, DateTime? completedAt)
    {
        Id = id;
        Title = title;
        CreatedAt = createdAt;
        IsCompleted = isCompleted;
        CompletedAt = completedAt;
    }

    /// <summary>Бізнес-правило: завершити можна лише незавершене завдання.</summary>
    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Завдання вже виконане.");
        IsCompleted = true;
        CompletedAt = DateTime.Now;
    }
}
