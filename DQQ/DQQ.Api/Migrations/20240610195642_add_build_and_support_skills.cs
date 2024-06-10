using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_build_and_support_skills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupportSkills",
                table: "SkillEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportSkills",
                table: "SkillEntities");
        }
    }
}
