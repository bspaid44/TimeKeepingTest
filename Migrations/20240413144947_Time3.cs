using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTest.Migrations
{
    /// <inheritdoc />
    public partial class Time3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Times_ClientId",
                table: "Times",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Clients_ClientId",
                table: "Times",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Clients_ClientId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_ClientId",
                table: "Times");
        }
    }
}
