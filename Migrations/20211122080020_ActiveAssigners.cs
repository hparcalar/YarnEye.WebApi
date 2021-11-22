using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace YarnEye.WebApi.Migrations
{
    public partial class ActiveAssigners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveAssigner",
                columns: table => new
                {
                    ActiveAssignerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IpAddr = table.Column<string>(type: "text", nullable: true),
                    SelectedLines = table.Column<string>(type: "text", nullable: true),
                    AssignerStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveAssigner", x => x.ActiveAssignerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveAssigner");
        }
    }
}
