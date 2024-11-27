using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class sqlitelocal_migration_415 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogShelters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogShelters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Breed = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    IsAvailableForAdoption = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShelterId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogs_DogShelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "DogShelters",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DogShelters",
                columns: new[] { "Id", "Address", "ContactNumber", "Name" },
                values: new object[,]
                {
                    { 1, "123 Shelter St.", "123-456-7890", "Shelter One" },
                    { 2, "456 Shelter Ave.", "987-654-3210", "Shelter Two" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Age", "Breed", "IsAvailableForAdoption", "Name", "ShelterId", "Weight" },
                values: new object[,]
                {
                    { 1, 3, "Golden Retriever", true, "Buddy", 1, 30.5 },
                    { 2, 2, "Beagle", true, "Charlie", 1, 20.0 },
                    { 3, 4, "Bulldog", false, "Max", 2, 25.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_ShelterId",
                table: "Dogs",
                column: "ShelterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropTable(
                name: "DogShelters");
        }
    }
}
