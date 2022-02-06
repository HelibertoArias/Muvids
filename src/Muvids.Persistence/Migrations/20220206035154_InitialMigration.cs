using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muvids.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsPublic", "LastModifiedBy", "LastModifiedDate", "Rating", "ReleaseYear", "Title" },
                values: new object[] { new Guid("a7f8c400-1f0b-4b45-83ce-5eef8ebb3641"), "00000000-0000-0000-0000-000000000000", new DateTime(2022, 2, 5, 22, 51, 54, 730, DateTimeKind.Local).AddTicks(6161), "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.", true, "00000000-0000-0000-0000-000000000000", new DateTime(2022, 2, 5, 22, 51, 54, 730, DateTimeKind.Local).AddTicks(6169), "PG-13", 2010, "Inception" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
