using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3ecexamen.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Talks",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ConferenceId_Name",
                table: "Rooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Talks",
                table: "Talks",
                columns: new[] { "SpeakerId", "RoomId", "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ConferenceId",
                table: "Rooms",
                column: "ConferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Talks",
                table: "Talks");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ConferenceId",
                table: "Rooms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Talks",
                table: "Talks",
                columns: new[] { "SpeakerId", "RoomId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ConferenceId_Name",
                table: "Rooms",
                columns: new[] { "ConferenceId", "Name" },
                unique: true);
        }
    }
}
