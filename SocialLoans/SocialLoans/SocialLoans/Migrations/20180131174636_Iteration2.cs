using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialLoans.Migrations
{
    public partial class Iteration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeIdCustomer",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAuthorized = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    Last4AccountNumber = table.Column<string>(nullable: true),
                    Routing = table.Column<string>(nullable: true),
                    StripeIdBankAccount = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disclosures",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    ResponseIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disclosures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisclosureTemplateType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisclosureTemplateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CarYear = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExpenseCar = table.Column<decimal>(nullable: false),
                    ExpenseCarInsurance = table.Column<decimal>(nullable: false),
                    ExpenseCellPhone = table.Column<decimal>(nullable: false),
                    ExpenseHome = table.Column<decimal>(nullable: false),
                    ExpenseOther1Amount = table.Column<decimal>(nullable: false),
                    ExpenseOther1Name = table.Column<string>(nullable: true),
                    ExpenseOther2Amount = table.Column<decimal>(nullable: false),
                    ExpenseOther2Name = table.Column<string>(nullable: true),
                    ExpenseOther3Amount = table.Column<decimal>(nullable: false),
                    ExpenseOther3Name = table.Column<string>(nullable: true),
                    ExpenseUtilties = table.Column<decimal>(nullable: false),
                    IncomeJob = table.Column<decimal>(nullable: false),
                    IncomeOther1 = table.Column<decimal>(nullable: false),
                    IncomeOther1IsSelfEmployed = table.Column<bool>(nullable: false),
                    IncomeOther1Name = table.Column<string>(nullable: true),
                    IncomeOther1Startdate = table.Column<DateTime>(nullable: false),
                    IncomeOther2 = table.Column<decimal>(nullable: false),
                    IncomeOther2IsSelfEmployed = table.Column<bool>(nullable: false),
                    IncomeOther2Name = table.Column<string>(nullable: true),
                    IncomeOther2Startdate = table.Column<DateTime>(nullable: false),
                    IsHomeOwn = table.Column<bool>(nullable: false),
                    IsSelfEmployed = table.Column<bool>(nullable: false),
                    JobStartdate = table.Column<DateTime>(nullable: false),
                    Jobtitle = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Zipcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsNegative = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsNegative = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisclosureTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    DisclosureTemplateTypeId = table.Column<int>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisclosureTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisclosureTemplates_DisclosureTemplateType_DisclosureTemplateTypeId",
                        column: x => x.DisclosureTemplateTypeId,
                        principalTable: "DisclosureTemplateType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BorrowerId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FirstPaymentDate = table.Column<DateTime>(nullable: false),
                    Interest = table.Column<decimal>(nullable: false),
                    InterestCutPercentage = table.Column<decimal>(nullable: false),
                    LenderId = table.Column<string>(nullable: true),
                    LoanStatusId = table.Column<int>(nullable: false),
                    MaturityDate = table.Column<DateTime>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentAmountCut = table.Column<decimal>(nullable: false),
                    PaymentFrequency = table.Column<string>(nullable: true),
                    Principal = table.Column<decimal>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_AspNetUsers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_AspNetUsers_LenderId",
                        column: x => x.LenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_LoanStatus_LoanStatusId",
                        column: x => x.LoanStatusId,
                        principalTable: "LoanStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    BankAccountId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserId",
                table: "BankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DisclosureTemplates_DisclosureTemplateTypeId",
                table: "DisclosureTemplates",
                column: "DisclosureTemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ApplicationUserId",
                table: "LoanApplications",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BorrowerId",
                table: "Loans",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LenderId",
                table: "Loans",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanStatusId",
                table: "Loans",
                column: "LoanStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountId",
                table: "Transactions",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StatusId",
                table: "Transactions",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disclosures");

            migrationBuilder.DropTable(
                name: "DisclosureTemplates");

            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "DisclosureTemplateType");

            migrationBuilder.DropTable(
                name: "LoanStatus");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "TransactionStatus");

            migrationBuilder.DropColumn(
                name: "StripeIdCustomer",
                table: "AspNetUsers");
        }
    }
}
