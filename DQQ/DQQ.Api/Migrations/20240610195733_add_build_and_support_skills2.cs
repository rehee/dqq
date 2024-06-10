using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DQQ.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_build_and_support_skills2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorBuilds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildDescribe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BodyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GlovesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BootsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainHandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OffHandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AmuletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BeltId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeftRingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RightRingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SkillAndStrategyJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorBuilds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorBuilds");
        }
    }
}
