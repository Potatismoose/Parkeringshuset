using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parkeringshuset.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ptypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Handicap = table.Column<string>(type: "TEXT", nullable: true),
                    Regular = table.Column<string>(type: "TEXT", nullable: true),
                    Motorbike = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ptypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegistrationNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ptickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckedInTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckedOutTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: true),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ptickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ptickets_Ptypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Ptypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ptickets_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ptickets_TypeId",
                table: "Ptickets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ptickets_VehicleId",
                table: "Ptickets",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ptickets");

            migrationBuilder.DropTable(
                name: "Ptypes");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
