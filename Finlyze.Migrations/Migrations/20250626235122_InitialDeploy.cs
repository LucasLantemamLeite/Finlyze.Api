using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finlyze.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialDeploy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogType = table.Column<int>(type: "Int", nullable: false),
                    LogTitle = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    LogDescription = table.Column<string>(type: "Nvarchar(200)", nullable: false),
                    LogCreateAt = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "Nvarchar(254)", nullable: false),
                    Password = table.Column<string>(type: "Nvarchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "Nvarchar(20)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "Date", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Active = table.Column<bool>(type: "Bit", nullable: false),
                    Role = table.Column<int>(type: "Int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionTitle = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    TransactionDescription = table.Column<string>(type: "Nvarchar(300)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeTransaction = table.Column<int>(type: "Int", nullable: false),
                    TransactionCreateAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TransactionUpdateAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserAccountId",
                table: "Transaction",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "Unique_Key_Email_UserAccount",
                table: "UserAccount",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_Key_PhoneNumber_UserAccount",
                table: "UserAccount",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLog");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
