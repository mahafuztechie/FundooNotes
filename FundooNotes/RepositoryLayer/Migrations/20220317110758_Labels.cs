using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    NoteId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Labels_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Labels_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Id",
                table: "Labels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_NoteId",
                table: "Labels",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");
        }
    }
}
