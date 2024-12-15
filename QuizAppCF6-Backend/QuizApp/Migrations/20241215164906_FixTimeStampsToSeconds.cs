using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class FixTimeStampsToSeconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(DATETIME, SYSDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(DATETIME, SYSDATETIME())");
        }
    }
}
