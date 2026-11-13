using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.UseCases;

/// <summary>Сценарій «Створити завдання». Працює лише через інтерфейс ITaskRepository.</summary>
public class CreateTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CreateTaskUseCase(ITaskRepository repository) => _repository = repository;

    public TaskItem Execute(string title)
    {
        var task = new TaskItem(title);
        _repository.Add(task);
        return task;
    }
}
