using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trello_back.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(50)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    idProject = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.id);
                    table.ForeignKey(
                        name: "FK_List_Project_idProject",
                        column: x => x.idProject,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(50)", nullable: true),
                    description = table.Column<string>(type: "varchar(50)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    idList = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.id);
                    table.ForeignKey(
                        name: "FK_Card_List_idList",
                        column: x => x.idList,
                        principalTable: "List",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "varchar(50)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    idCard = table.Column<int>(type: "INT", nullable: true),
                    user = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comment_Card_idCard",
                        column: x => x.idCard,
                        principalTable: "Card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_idList",
                table: "Card",
                column: "idList");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_idCard",
                table: "Comment",
                column: "idCard");

            migrationBuilder.CreateIndex(
                name: "IX_List_idProject",
                table: "List",
                column: "idProject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
