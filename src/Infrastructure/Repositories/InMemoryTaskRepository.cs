using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Infrastructure.Repositories;

/// <summary>Сховище в оперативній пам'яті (для тестів і швидкого запуску).</summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly Dictionary<Guid, TaskItem> _tasks = new();

    public void Add(TaskItem task) => _tasks[task.Id] = task;
    public TaskItem? GetById(Guid id) => _tasks.GetValueOrDefault(id);
    public IReadOnlyList<TaskItem> GetAll() => _tasks.Values.ToList();
    public void Update(TaskItem task) => _tasks[task.Id] = task;
}
