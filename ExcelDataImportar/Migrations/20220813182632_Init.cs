using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExcelDataImportar.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExcelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Col16 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditData_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcelMasterDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelMasterDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelMasterDatas_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInformation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditData_UserId",
                table: "AuditData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcelMasterDatas_UserId",
                table: "ExcelMasterDatas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_UserId",
                table: "UserInformation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditData");

            migrationBuilder.DropTable(
                name: "ExcelMasterDatas");

            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
