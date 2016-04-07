using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace NQuandl.Services.PostgresEF7.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Dataset_Database_DatabaseId", table: "dataset");
            migrationBuilder.DropForeignKey(name: "FK_DatasetColumnName_Dataset_DatasetId", table: "DatasetColumnName");
            migrationBuilder.CreateTable(
                name: "DatabaseDatasetListEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    DatabaseCode = table.Column<string>(nullable: true),
                    DatasetCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    QuandlCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseDatasetListEntry", x => x.Id);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Dataset_Database_DatabaseId",
                table: "dataset",
                column: "DatabaseId",
                principalTable: "database",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_DatasetColumnName_Dataset_DatasetId",
                table: "DatasetColumnName",
                column: "DatasetId",
                principalTable: "dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Dataset_Database_DatabaseId", table: "dataset");
            migrationBuilder.DropForeignKey(name: "FK_DatasetColumnName_Dataset_DatasetId", table: "DatasetColumnName");
            migrationBuilder.DropTable("DatabaseDatasetListEntry");
            migrationBuilder.AddForeignKey(
                name: "FK_Dataset_Database_DatabaseId",
                table: "dataset",
                column: "DatabaseId",
                principalTable: "database",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_DatasetColumnName_Dataset_DatasetId",
                table: "DatasetColumnName",
                column: "DatasetId",
                principalTable: "dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
