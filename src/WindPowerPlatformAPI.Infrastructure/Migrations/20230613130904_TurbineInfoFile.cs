using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WindPowerPlatformAPI.Infrastructure.Migrations
{
    public partial class TurbineInfoFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TurbineInfoFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileExtension = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TurbineId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurbineInfoFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurbineInfoFiles_Turbines_TurbineId",
                        column: x => x.TurbineId,
                        principalTable: "Turbines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurbineInfoFiles_TurbineId",
                table: "TurbineInfoFiles",
                column: "TurbineId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurbineInfoFiles");
        }
    }
}
