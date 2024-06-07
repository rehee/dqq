using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class update_item_rarity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rarity",
                table: "ItemEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rarity",
                table: "ItemEntities");
        }
    }
}
