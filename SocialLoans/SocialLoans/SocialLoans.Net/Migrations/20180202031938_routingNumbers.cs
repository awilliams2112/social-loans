using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialLoans.Migrations
{
    public partial class routingNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_ImportType_ImportTypeId",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Import_RoutingNumbers_Import_ImportId",
                table: "Import_RoutingNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Import",
                table: "Import");

            migrationBuilder.RenameTable(
                name: "Import",
                newName: "Imports");

            migrationBuilder.RenameIndex(
                name: "IX_Import_ImportTypeId",
                table: "Imports",
                newName: "IX_Imports_ImportTypeId");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Import_RoutingNumbers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLatest",
                table: "Imports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imports",
                table: "Imports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_RoutingNumbers_Imports_ImportId",
                table: "Import_RoutingNumbers",
                column: "ImportId",
                principalTable: "Imports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Imports_ImportType_ImportTypeId",
                table: "Imports",
                column: "ImportTypeId",
                principalTable: "ImportType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_RoutingNumbers_Imports_ImportId",
                table: "Import_RoutingNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Imports_ImportType_ImportTypeId",
                table: "Imports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Imports",
                table: "Imports");

            migrationBuilder.DropColumn(
                name: "IsLatest",
                table: "Imports");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Import_RoutingNumbers");

            migrationBuilder.RenameTable(
                name: "Imports",
                newName: "Import");

            migrationBuilder.RenameIndex(
                name: "IX_Imports_ImportTypeId",
                table: "Import",
                newName: "IX_Import_ImportTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Import",
                table: "Import",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_ImportType_ImportTypeId",
                table: "Import",
                column: "ImportTypeId",
                principalTable: "ImportType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Import_RoutingNumbers_Import_ImportId",
                table: "Import_RoutingNumbers",
                column: "ImportId",
                principalTable: "Import",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
