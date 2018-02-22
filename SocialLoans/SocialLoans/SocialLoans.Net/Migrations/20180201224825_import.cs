using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialLoans.Migrations
{
    public partial class import : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Import",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ImporTypeId = table.Column<int>(nullable: false),
                    ImportTypeId = table.Column<int>(nullable: true),
                    InsertSql = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RollBackSql = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Import_ImportType_ImportTypeId",
                        column: x => x.ImportTypeId,
                        principalTable: "ImportType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Import_RoutingNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AchServicesTelephone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    DateOfLastRevision = table.Column<string>(nullable: true),
                    ImportId = table.Column<int>(nullable: false),
                    NewRoutingNumbers = table.Column<string>(nullable: true),
                    RoutingNumbers = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_RoutingNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Import_RoutingNumbers_Import_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Import",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Import_ImportTypeId",
                table: "Import",
                column: "ImportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Import_RoutingNumbers_ImportId",
                table: "Import_RoutingNumbers",
                column: "ImportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Import_RoutingNumbers");

            migrationBuilder.DropTable(
                name: "Import");

            migrationBuilder.DropTable(
                name: "ImportType");
        }
    }
}
