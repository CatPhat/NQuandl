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
                name: "Database",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    DatabaseCode = table.Column<string>(nullable: true),
                    DatasetsCount = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Downloads = table.Column<long>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Premium = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Dataset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Code = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    DatabaseCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RefreshedAt = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataset", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "RawResponse",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    RequestUri = table.Column<string>(nullable: true),
                    ResponseContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawResponse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Database");
            migrationBuilder.DropTable("Dataset");
            migrationBuilder.DropTable("RawResponse");
        }
    }
}
