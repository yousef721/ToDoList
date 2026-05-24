using System.ComponentModel.DataAnnotations;
using TodoList.Shared.Enums;

namespace TodoList.DAL.Entities;

public sealed class TodoItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }

    public TodoPriority Priority { get; set; } = TodoPriority.Medium;

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser? User { get; set; }
}
