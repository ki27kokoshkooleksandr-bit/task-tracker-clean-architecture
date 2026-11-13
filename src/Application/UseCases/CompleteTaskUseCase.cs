using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Application.UseCases;

/// <summary>Сценарій «Позначити завдання виконаним».</summary>
public class CompleteTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CompleteTaskUseCase(ITaskRepository repository) => _repository = repository;

    public void Execute(Guid id)
    {
        var task = _repository.GetById(id)
                   ?? throw new KeyNotFoundException($"Завдання з Id {id} не знайдено.");
        task.Complete();
        _repository.Update(task);
    }
}
