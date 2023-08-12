using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityServerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "player_data");

            migrationBuilder.CreateTable(
                name: "player",
                schema: "player_data",
                columns: table => new
                {
                    steam_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    is_a_penguin = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_player", x => x.steam_id);
                });

            migrationBuilder.CreateTable(
                name: "progress",
                schema: "player_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    tool_progress = table.Column<IEnumerable<byte>>(type: "jsonb", nullable: true),
                    achievements = table.Column<IEnumerable<byte>>(type: "jsonb", nullable: true),
                    selections = table.Column<IEnumerable<byte>>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_progress", x => x.id);
                    table.ForeignKey(
                        name: "fk_progress_player_player_id",
                        column: x => x.player_id,
                        principalSchema: "player_data",
                        principalTable: "player",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stats",
                schema: "player_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    kill_count = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stats", x => x.id);
                    table.ForeignKey(
                        name: "fk_stats_player_player_id",
                        column: x => x.player_id,
                        principalSchema: "player_data",
                        principalTable: "player",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_player_deleted_at",
                schema: "player_data",
                table: "player",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "ix_progress_deleted_at",
                schema: "player_data",
                table: "progress",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "ix_progress_player_id",
                schema: "player_data",
                table: "progress",
                column: "player_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stats_deleted_at",
                schema: "player_data",
                table: "stats",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "ix_stats_player_id",
                schema: "player_data",
                table: "stats",
                column: "player_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "progress",
                schema: "player_data");

            migrationBuilder.DropTable(
                name: "stats",
                schema: "player_data");

            migrationBuilder.DropTable(
                name: "player",
                schema: "player_data");
        }
    }
}
