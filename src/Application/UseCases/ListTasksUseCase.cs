using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.UseCases;

/// <summary>Сценарій «Переглянути список завдань».</summary>
public class ListTasksUseCase
{
    private readonly ITaskRepository _repository;

    public ListTasksUseCase(ITaskRepository repository) => _repository = repository;

    public IReadOnlyList<TaskItem> Execute(bool onlyActive = false) =>
        _repository.GetAll()
                   .Where(t => !onlyActive || !t.IsCompleted)
                   .OrderBy(t => t.CreatedAt)
                   .ToList();
}
