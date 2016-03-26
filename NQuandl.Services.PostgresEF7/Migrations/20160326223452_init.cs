using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace NQuandl.Services.PostgresEF7.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuandlDatabase",
                columns: table => new
                {
                    DatabaseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuandlDatabase", x => x.DatabaseId);
                });
            migrationBuilder.CreateTable(
                name: "QuandlDataset",
                columns: table => new
                {
                    DatasetId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    QuandlDatabaseDatabaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuandlDataset", x => x.DatasetId);
                    table.ForeignKey(
                        name: "FK_QuandlDataset_QuandlDatabase_QuandlDatabaseDatabaseId",
                        column: x => x.QuandlDatabaseDatabaseId,
                        principalTable: "QuandlDatabase",
                        principalColumn: "DatabaseId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("QuandlDataset");
            migrationBuilder.DropTable("QuandlDatabase");
        }
    }
}
