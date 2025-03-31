using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteTécnicoIdeal.API.Migrations
{
    /// <inheritdoc />
    public partial class Fixing_Name_Convention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Surname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "LastName");
        }
    }
}
