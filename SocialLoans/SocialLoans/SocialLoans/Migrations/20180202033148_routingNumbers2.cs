using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialLoans.Migrations
{
    public partial class routingNumbers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutingNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AbaNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    BankPhone = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    DateOfLastRevision = table.Column<DateTime>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingNumbers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutingNumbers");
        }
    }
}
