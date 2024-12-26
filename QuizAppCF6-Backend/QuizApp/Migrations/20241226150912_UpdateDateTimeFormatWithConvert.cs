﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTimeFormatWithConvert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "QuizScores",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertedAt",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CONVERT(VARCHAR, SYSDATETIME(), 120)");
        }
    }
}
