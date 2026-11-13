using System.Text.Json;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;

namespace TaskTracker.Infrastructure.Repositories;

/// <summary>Сховище завдань у JSON-файлі.</summary>
public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public JsonTaskRepository(string filePath) => _filePath = filePath;

    public void Add(TaskItem task)
    {
        var all = Load();
        all.Add(TaskDto.From(task));
        Save(all);
    }

    public TaskItem? GetById(Guid id) => Load().FirstOrDefault(t => t.Id == id)?.ToEntity();

    public IReadOnlyList<TaskItem> GetAll() => Load().Select(t => t.ToEntity()).ToList();

    public void Update(TaskItem task)
    {
        var all = Load();
        var index = all.FindIndex(t => t.Id == task.Id);
        if (index < 0) throw new KeyNotFoundException($"Завдання з Id {task.Id} не знайдено.");
        all[index] = TaskDto.From(task);
        Save(all);
    }

    private List<TaskDto> Load()
    {
        if (!File.Exists(_filePath)) return new List<TaskDto>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TaskDto>>(json, Options) ?? new List<TaskDto>();
    }

    private void Save(List<TaskDto> tasks) =>
        File.WriteAllText(_filePath, JsonSerializer.Serialize(tasks, Options));

    private record TaskDto(Guid Id, string Title, DateTime CreatedAt, bool IsCompleted, DateTime? CompletedAt)
    {
        public static TaskDto From(TaskItem t) => new(t.Id, t.Title, t.CreatedAt, t.IsCompleted, t.CompletedAt);
        public TaskItem ToEntity() => new(Id, Title, CreatedAt, IsCompleted, CompletedAt);
    }
}
