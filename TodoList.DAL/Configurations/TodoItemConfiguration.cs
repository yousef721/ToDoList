
namespace TodoList.DAL.Configurations;

public sealed class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        
        builder.ToTable("Todos");

        builder.HasKey(todo => todo.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(todo => todo.Description)
            .HasMaxLength(1000);

        builder.Property(todo => todo.Priority)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(t => t.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(t => t.CreatedDate)
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.Property(t => t.UpdatedDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(todo => todo.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.HasOne(todo => todo.User)
            .WithMany(user => user.Todos)
            .HasForeignKey(todo => todo.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Global query filter for soft delete
        builder.HasQueryFilter(todo => !todo.IsDeleted);
    }
}
