using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Roles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Roles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesName, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesName",
                        column: x => x.RolesName,
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }
    }
}
