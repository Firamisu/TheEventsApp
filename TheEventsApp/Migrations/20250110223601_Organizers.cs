using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEventsApp.Migrations
{
    /// <inheritdoc />
    public partial class Organizers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEvent_Events_EventsId",
                table: "ApplicationUserEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserEvent",
                table: "ApplicationUserEvent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserEvent_ParticipantsId",
                table: "ApplicationUserEvent");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "ApplicationUserEvent",
                newName: "ParticipatedEventsId");

            migrationBuilder.AddColumn<string>(
                name: "OrganizerId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserEvent",
                table: "ApplicationUserEvent",
                columns: new[] { "ParticipantsId", "ParticipatedEventsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEvent_ParticipatedEventsId",
                table: "ApplicationUserEvent",
                column: "ParticipatedEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEvent_Events_ParticipatedEventsId",
                table: "ApplicationUserEvent",
                column: "ParticipatedEventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEvent_Events_ParticipatedEventsId",
                table: "ApplicationUserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserEvent",
                table: "ApplicationUserEvent");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserEvent_ParticipatedEventsId",
                table: "ApplicationUserEvent");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ParticipatedEventsId",
                table: "ApplicationUserEvent",
                newName: "EventsId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserEvent",
                table: "ApplicationUserEvent",
                columns: new[] { "EventsId", "ParticipantsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEvent_ParticipantsId",
                table: "ApplicationUserEvent",
                column: "ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEvent_Events_EventsId",
                table: "ApplicationUserEvent",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
