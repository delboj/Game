using Domain.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Games;

/// <summary>
/// Fluent configuration for Game DbSet (EF) 
/// </summary>
internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasOne(g => g.PlayerChoice)
            .WithMany()
            .HasForeignKey("PlayerChoiceId")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(g => g.ComputerChoice)
            .WithMany()
            .HasForeignKey("ComputerChoiceId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.GameResult);
    }
}
