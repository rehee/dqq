using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_DamageModifier_for_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamageModifier",
                table: "ItemEntities");
        }
    }
}
