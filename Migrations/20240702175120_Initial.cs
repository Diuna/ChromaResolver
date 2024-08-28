using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ChromaResolver.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECMBase",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Element = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    IcpValue = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECMBase", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "ECM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fe = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cr = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ni = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cu = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", nullable: false),
                    Ah = table.Column<int>(type: "INTEGER", nullable: false),
                    AboveHeight = table.Column<double>(type: "REAL", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", nullable: true),
                    FeGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    CrGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    NiGuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    CuGuid = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_CrGuid",
                        column: x => x.CrGuid,
                        principalTable: "ECMBase",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_CuGuid",
                        column: x => x.CuGuid,
                        principalTable: "ECMBase",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_FeGuid",
                        column: x => x.FeGuid,
                        principalTable: "ECMBase",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_NiGuid",
                        column: x => x.NiGuid,
                        principalTable: "ECMBase",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECM_CrGuid",
                table: "ECM",
                column: "CrGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_CuGuid",
                table: "ECM",
                column: "CuGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_FeGuid",
                table: "ECM",
                column: "FeGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_NiGuid",
                table: "ECM",
                column: "NiGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECM");

            migrationBuilder.DropTable(
                name: "ECMBase");
        }
    }
}
