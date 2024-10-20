using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "choices",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                choice_id = table.Column<int>(type: "integer", nullable: false),
                move = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_choices", x => x.id));

        migrationBuilder.CreateTable(
            name: "games",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                player_choice_id = table.Column<Guid>(type: "uuid", nullable: false),
                computer_choice_id = table.Column<Guid>(type: "uuid", nullable: false),
                game_result = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_games", x => x.id);
                table.ForeignKey(
                    name: "fk_games_choices_computer_choice_id",
                    column: x => x.computer_choice_id,
                    principalSchema: "public",
                    principalTable: "choices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_games_choices_player_choice_id",
                    column: x => x.player_choice_id,
                    principalSchema: "public",
                    principalTable: "choices",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "public",
            table: "choices",
            columns: ["id", "choice_id", "move"],
            values: new object[,]
            {
                { new Guid("038a9e62-7c05-42af-b85b-7050d14b6fc3"), 3, 3 },
                { new Guid("42c55ad4-f082-4289-9460-4880da57d77f"), 2, 2 },
                { new Guid("525ca1e4-3e8c-437c-b505-064bf63ad6f8"), 1, 1 },
                { new Guid("9abf55d1-8f8a-43a9-89e1-db6dc53fd27a"), 4, 4 },
                { new Guid("d032be8a-5260-4652-a047-ca5e1bb2ed47"), 5, 5 }
            });

        migrationBuilder.CreateIndex(
            name: "ix_games_computer_choice_id",
            schema: "public",
            table: "games",
            column: "computer_choice_id");

        migrationBuilder.CreateIndex(
            name: "ix_games_game_result",
            schema: "public",
            table: "games",
            column: "game_result");

        migrationBuilder.CreateIndex(
            name: "ix_games_player_choice_id",
            schema: "public",
            table: "games",
            column: "player_choice_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "games",
            schema: "public");

        migrationBuilder.DropTable(
            name: "choices",
            schema: "public");
    }
}
