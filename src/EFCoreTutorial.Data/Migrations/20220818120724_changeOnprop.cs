using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTutorial.Data.Migrations
{
    public partial class changeOnprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_courses_CourseId",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_CourseId",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "dbo",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                schema: "dbo",
                table: "students",
                newName: "birth_date");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "student_book_id_fk",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                schema: "dbo",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_courses_CoursesId",
                        column: x => x.CoursesId,
                        principalSchema: "dbo",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_students_StudentsId",
                        column: x => x.StudentsId,
                        principalSchema: "dbo",
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_addresses",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    district = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    full_address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_addresses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students",
                column: "address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_StudentId",
                schema: "dbo",
                table: "Book",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                schema: "dbo",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "student_address_student_id_fk",
                schema: "dbo",
                table: "students",
                column: "address_id",
                principalSchema: "dbo",
                principalTable: "student_addresses",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "student_address_student_id_fk",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CourseStudent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "student_addresses",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                schema: "dbo",
                table: "students",
                newName: "Birthday");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                schema: "dbo",
                table: "students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_CourseId",
                schema: "dbo",
                table: "students",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_courses_CourseId",
                schema: "dbo",
                table: "students",
                column: "CourseId",
                principalSchema: "dbo",
                principalTable: "courses",
                principalColumn: "id");
        }
    }
}
