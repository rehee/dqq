using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_build_target_priority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetPriority",
                table: "ActorBuilds",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetPriority",
                table: "ActorBuilds");
        }
    }
}
