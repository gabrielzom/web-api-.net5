using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoriaWEBAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_and_surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    register_of_physical_person = table.Column<string>(type: "CHAR(11)", nullable: true),
                    date_of_born = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.client_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
