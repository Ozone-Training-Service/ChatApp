using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp2.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    clientuniqueid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.clientuniqueid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
