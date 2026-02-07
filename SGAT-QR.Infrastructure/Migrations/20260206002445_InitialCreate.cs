using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAT_QR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cat_Dependencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_Dependencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_TiposEquipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_TiposEquipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cat_TiposPerifericos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_TiposPerifericos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tab_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tab_Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tab_Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomenclatura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoEquipoId = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SerialEnvia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SistemaOperativo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionSO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MAC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Procesador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioAsignado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DependenciaId = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRCodeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioRegistroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_Equipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tab_Equipos_cat_Dependencias_DependenciaId",
                        column: x => x.DependenciaId,
                        principalTable: "cat_Dependencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tab_Equipos_cat_TiposEquipo_TipoEquipoId",
                        column: x => x.TipoEquipoId,
                        principalTable: "cat_TiposEquipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_tab_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tab_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_tab_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "tab_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_tab_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "tab_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_tab_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "tab_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rel_UsuarioRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rel_UsuarioRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_rel_UsuarioRoles_tab_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tab_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rel_UsuarioRoles_tab_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "tab_Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tab_Perifericos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPerifericoId = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRCodeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_Perifericos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tab_Perifericos_cat_TiposPerifericos_TipoPerifericoId",
                        column: x => x.TipoPerifericoId,
                        principalTable: "cat_TiposPerifericos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tab_Perifericos_tab_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "tab_Equipos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tab_Novedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    PerifericoId = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Severidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioReportaId = table.Column<int>(type: "int", nullable: false),
                    FechaReporte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SolucionAplicada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaResolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_Novedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tab_Novedades_tab_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "tab_Equipos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tab_Novedades_tab_Perifericos_PerifericoId",
                        column: x => x.PerifericoId,
                        principalTable: "tab_Perifericos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_rel_UsuarioRoles_RoleId",
                table: "rel_UsuarioRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Equipos_DependenciaId",
                table: "tab_Equipos",
                column: "DependenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Equipos_Nomenclatura",
                table: "tab_Equipos",
                column: "Nomenclatura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tab_Equipos_TipoEquipoId",
                table: "tab_Equipos",
                column: "TipoEquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Novedades_EquipoId",
                table: "tab_Novedades",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Novedades_PerifericoId",
                table: "tab_Novedades",
                column: "PerifericoId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Perifericos_EquipoId",
                table: "tab_Perifericos",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_tab_Perifericos_TipoPerifericoId",
                table: "tab_Perifericos",
                column: "TipoPerifericoId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "tab_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "tab_Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "tab_Usuarios",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "rel_UsuarioRoles");

            migrationBuilder.DropTable(
                name: "tab_Novedades");

            migrationBuilder.DropTable(
                name: "tab_Roles");

            migrationBuilder.DropTable(
                name: "tab_Usuarios");

            migrationBuilder.DropTable(
                name: "tab_Perifericos");

            migrationBuilder.DropTable(
                name: "cat_TiposPerifericos");

            migrationBuilder.DropTable(
                name: "tab_Equipos");

            migrationBuilder.DropTable(
                name: "cat_Dependencias");

            migrationBuilder.DropTable(
                name: "cat_TiposEquipo");
        }
    }
}
