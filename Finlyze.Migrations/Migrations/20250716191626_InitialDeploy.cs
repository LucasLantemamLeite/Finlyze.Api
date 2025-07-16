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
                name: "SystemLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<int>(type: "Int", nullable: false),
                    Title = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(200)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLog", x => x.Id);
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
                name: "FinancialTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "Int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(300)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TranType = table.Column<int>(type: "Int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UserAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_UserAccountId",
                table: "FinancialTransaction",
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
                name: "FinancialTransaction");

            migrationBuilder.DropTable(
                name: "SystemLog");

            migrationBuilder.DropTable(
                name: "UserAccount");
        }
    }
}
