using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAT_QR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsuarioIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_tab_Roles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_tab_Usuarios_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_tab_Usuarios_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_tab_Usuarios_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_tab_Equipos_Nomenclatura",
                table: "tab_Equipos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "tab_UsuarioTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "tab_UsuarioLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "tab_UsuarioClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "tab_RolClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "tab_UsuarioLogins",
                newName: "IX_tab_UsuarioLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "tab_UsuarioClaims",
                newName: "IX_tab_UsuarioClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "tab_RolClaims",
                newName: "IX_tab_RolClaims_RoleId");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "tab_Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "tab_Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "tab_Usuarios",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "tab_Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "tab_Equipos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tab_UsuarioTokens",
                table: "tab_UsuarioTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tab_UsuarioLogins",
                table: "tab_UsuarioLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tab_UsuarioClaims",
                table: "tab_UsuarioClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tab_RolClaims",
                table: "tab_RolClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tab_RolClaims_tab_Roles_RoleId",
                table: "tab_RolClaims",
                column: "RoleId",
                principalTable: "tab_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_UsuarioClaims_tab_Usuarios_UserId",
                table: "tab_UsuarioClaims",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_UsuarioLogins_tab_Usuarios_UserId",
                table: "tab_UsuarioLogins",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_UsuarioTokens_tab_Usuarios_UserId",
                table: "tab_UsuarioTokens",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tab_RolClaims_tab_Roles_RoleId",
                table: "tab_RolClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_UsuarioClaims_tab_Usuarios_UserId",
                table: "tab_UsuarioClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_UsuarioLogins_tab_Usuarios_UserId",
                table: "tab_UsuarioLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_UsuarioTokens_tab_Usuarios_UserId",
                table: "tab_UsuarioTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tab_UsuarioTokens",
                table: "tab_UsuarioTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tab_UsuarioLogins",
                table: "tab_UsuarioLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tab_UsuarioClaims",
                table: "tab_UsuarioClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tab_RolClaims",
                table: "tab_RolClaims");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "tab_Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "tab_Usuarios");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "tab_Usuarios");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "tab_Usuarios");

            migrationBuilder.RenameTable(
                name: "tab_UsuarioTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "tab_UsuarioLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "tab_UsuarioClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "tab_RolClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_tab_UsuarioLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tab_UsuarioClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tab_RolClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "tab_Equipos",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Equipos_Nomenclatura",
                table: "tab_Equipos",
                column: "Nomenclatura",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_tab_Roles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "tab_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_tab_Usuarios_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_tab_Usuarios_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_tab_Usuarios_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "tab_Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
