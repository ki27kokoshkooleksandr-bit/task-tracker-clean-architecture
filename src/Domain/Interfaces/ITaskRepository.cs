using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain.Interfaces;

/// <summary>Абстракція сховища завдань. Реалізація знаходиться в Infrastructure.</summary>
public interface ITaskRepository
{
    void Add(TaskItem task);
    TaskItem? GetById(Guid id);
    IReadOnlyList<TaskItem> GetAll();
    void Update(TaskItem task);
}
