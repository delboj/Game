using Domain.Choices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Choices;

/// <summary>
/// Fluent configuration for Choice DbSet (EF) 
/// </summary>
internal class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasIndex(c => c.ChoiceId).IsUnique();

        builder.HasData(
                new Choice() { Id = Guid.NewGuid(), ChoiceId = (int)Move.Rock, Move = Move.Rock },
                new Choice() { Id = Guid.NewGuid(), ChoiceId = (int)Move.Paper, Move = Move.Paper },
                new Choice() { Id = Guid.NewGuid(), ChoiceId = (int)Move.Scissors, Move = Move.Scissors },
                new Choice() { Id = Guid.NewGuid(), ChoiceId = (int)Move.Lizard, Move = Move.Lizard },
                new Choice() { Id = Guid.NewGuid(), ChoiceId = (int)Move.Spock, Move = Move.Spock });
    }
}
