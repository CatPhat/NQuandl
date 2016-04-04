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
                name: "database",
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
                name: "raw_response",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "current_timestamp"),
                    RequestUri = table.Column<string>(nullable: true),
                    ResponseContent = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawResponse", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "dataset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Code = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    DatabaseCode = table.Column<string>(nullable: true),
                    DatabaseId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RefreshedAt = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dataset_Database_DatabaseId",
                        column: x => x.DatabaseId,
                        principalTable: "database",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "DatasetColumnName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    ColumnIndex = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasetColumnName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatasetColumnName_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("DatasetColumnName");
            migrationBuilder.DropTable("raw_response");
            migrationBuilder.DropTable("dataset");
            migrationBuilder.DropTable("database");
        }
    }
}
