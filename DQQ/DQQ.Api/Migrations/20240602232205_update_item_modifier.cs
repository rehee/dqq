using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class update_item_modifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffixesJson",
                table: "ItemEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AttackRating",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BlockChance",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BlockRecovery",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChaosDamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChaosResistance",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ColdDamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ColdResistance",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Defence",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DefencePercentage",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DodgeChance",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FireDamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FireResistance",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LightningDamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LightningResistance",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PhysicsDamageModifier",
                table: "ItemEntities",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PhysicsResistance",
                table: "ItemEntities",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffixesJson",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "AttackRating",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "BlockChance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "BlockRecovery",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "ChaosDamageModifier",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "ChaosResistance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "ColdDamageModifier",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "ColdResistance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "Defence",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "DefencePercentage",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "DodgeChance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "FireDamageModifier",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "FireResistance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "LightningDamageModifier",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "LightningResistance",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "PhysicsDamageModifier",
                table: "ItemEntities");

            migrationBuilder.DropColumn(
                name: "PhysicsResistance",
                table: "ItemEntities");
        }
    }
}
