using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCatalog.Migrations
{
    public partial class NullableCopyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopyPatron_Copy_CopyId",
                table: "BookCopyPatron");

            migrationBuilder.AlterColumn<int>(
                name: "CopyId",
                table: "BookCopyPatron",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopyPatron_Copy_CopyId",
                table: "BookCopyPatron",
                column: "CopyId",
                principalTable: "Copy",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopyPatron_Copy_CopyId",
                table: "BookCopyPatron");

            migrationBuilder.AlterColumn<int>(
                name: "CopyId",
                table: "BookCopyPatron",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopyPatron_Copy_CopyId",
                table: "BookCopyPatron",
                column: "CopyId",
                principalTable: "Copy",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
