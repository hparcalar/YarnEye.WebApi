using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace YarnEye.WebApi.Migrations
{
    public partial class InitScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorAssignment",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AssignmentCode = table.Column<string>(type: "text", nullable: true),
                    SampleImage = table.Column<byte[]>(type: "bytea", nullable: true),
                    SetHue = table.Column<decimal>(type: "numeric", nullable: true),
                    SetSaturation = table.Column<decimal>(type: "numeric", nullable: true),
                    SetValue = table.Column<decimal>(type: "numeric", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorAssignment", x => x.AssignmentId);
                });

            migrationBuilder.CreateTable(
                name: "ProdLine",
                columns: table => new
                {
                    ProdLineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProdLineCode = table.Column<string>(type: "text", nullable: true),
                    ProdLineName = table.Column<string>(type: "text", nullable: true),
                    OrderNo = table.Column<int>(type: "integer", nullable: true),
                    AssignmentId = table.Column<int>(type: "integer", nullable: true),
                    ColorAssignmentAssignmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdLine", x => x.ProdLineId);
                    table.ForeignKey(
                        name: "FK_ProdLine_ColorAssignment_ColorAssignmentAssignmentId",
                        column: x => x.ColorAssignmentAssignmentId,
                        principalTable: "ColorAssignment",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YarnCheckResult",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SerialNo = table.Column<string>(type: "text", nullable: true),
                    ProdLineId = table.Column<int>(type: "integer", nullable: true),
                    AssignmentId = table.Column<int>(type: "integer", nullable: true),
                    TestResult = table.Column<int>(type: "integer", nullable: true),
                    ColorHue = table.Column<decimal>(type: "numeric", nullable: true),
                    ColorSaturation = table.Column<decimal>(type: "numeric", nullable: true),
                    ColorValue = table.Column<decimal>(type: "numeric", nullable: true),
                    DiffHue = table.Column<decimal>(type: "numeric", nullable: true),
                    DiffSaturation = table.Column<decimal>(type: "numeric", nullable: true),
                    DiffValue = table.Column<decimal>(type: "numeric", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ColorAssignmentAssignmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnCheckResult", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_YarnCheckResult_ColorAssignment_ColorAssignmentAssignmentId",
                        column: x => x.ColorAssignmentAssignmentId,
                        principalTable: "ColorAssignment",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YarnCheckResult_ProdLine_ProdLineId",
                        column: x => x.ProdLineId,
                        principalTable: "ProdLine",
                        principalColumn: "ProdLineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdLine_ColorAssignmentAssignmentId",
                table: "ProdLine",
                column: "ColorAssignmentAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_YarnCheckResult_ColorAssignmentAssignmentId",
                table: "YarnCheckResult",
                column: "ColorAssignmentAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_YarnCheckResult_ProdLineId",
                table: "YarnCheckResult",
                column: "ProdLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YarnCheckResult");

            migrationBuilder.DropTable(
                name: "ProdLine");

            migrationBuilder.DropTable(
                name: "ColorAssignment");
        }
    }
}
