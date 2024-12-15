using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTimeStampsToSeconds3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(SECOND, 0, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CAST(SYSDATETIME() AS DATETIME)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "DATEADD(SECOND, 0, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CAST(SYSDATETIME() AS DATETIME)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CAST(SYSDATETIME() AS DATETIME)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(SECOND, 0, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CAST(SYSDATETIME() AS DATETIME)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "DATEADD(SECOND, 0, SYSDATETIME())");
        }
    }
}
