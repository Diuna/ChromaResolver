using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Element = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    PercentageValue = table.Column<double>(type: "REAL", nullable: false),
                    IcpValue = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECMBase", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ECM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fe = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cr = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ni = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cu = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DaysAgo = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", nullable: false),
                    Ah = table.Column<int>(type: "INTEGER", nullable: false),
                    AboveHeight = table.Column<double>(type: "REAL", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", nullable: true),
                    StemsItemSource = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_Cr",
                        column: x => x.Cr,
                        principalTable: "ECMBase",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_Cu",
                        column: x => x.Cu,
                        principalTable: "ECMBase",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_Fe",
                        column: x => x.Fe,
                        principalTable: "ECMBase",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECM_ECMBase_Ni",
                        column: x => x.Ni,
                        principalTable: "ECMBase",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECM_Cr",
                table: "ECM",
                column: "Cr");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_Cu",
                table: "ECM",
                column: "Cu");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_Fe",
                table: "ECM",
                column: "Fe");

            migrationBuilder.CreateIndex(
                name: "IX_ECM_Ni",
                table: "ECM",
                column: "Ni");
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
