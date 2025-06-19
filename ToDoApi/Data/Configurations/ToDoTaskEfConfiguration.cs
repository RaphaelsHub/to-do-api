using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApi.Entities;

public class ToDoTaskEfConfiguration : IEntityTypeConfiguration<ToDoTaskEF>
{
    public void Configure(EntityTypeBuilder<ToDoTaskEF> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IsCompleted);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CompletedAt);
    }
}
