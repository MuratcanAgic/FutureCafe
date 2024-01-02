using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1007_BanProductToUserEntitiesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCategories_Categories_CategoryId",
                table: "StudentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCategories_Products_ProductId",
                table: "StudentCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "StudentCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "StudentCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudentProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentProduct_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Kantinci");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 78, 176, 70, 210, 43, 126, 0, 7, 141, 159, 47, 84, 27, 211, 134, 55, 16, 105, 147, 95, 130, 102, 198, 48, 151, 135, 239, 102, 48, 212, 226, 23, 52, 179, 206, 63, 193, 112, 184, 201, 104, 35, 64, 49, 154, 60, 110, 178, 36, 103, 79, 194, 27, 124, 217, 100, 118, 171, 0, 253, 4, 99, 138, 94 }, new byte[] { 231, 66, 243, 9, 225, 8, 143, 173, 141, 107, 16, 9, 143, 182, 169, 90, 100, 98, 192, 35, 80, 45, 228, 24, 93, 249, 34, 34, 70, 17, 127, 168, 229, 153, 73, 14, 51, 24, 116, 214, 50, 56, 184, 221, 72, 194, 49, 243, 140, 112, 234, 157, 51, 168, 212, 187, 209, 52, 125, 62, 175, 175, 236, 211, 0, 104, 239, 96, 84, 33, 118, 93, 14, 214, 188, 60, 226, 44, 177, 228, 48, 72, 156, 97, 31, 237, 48, 34, 188, 203, 112, 177, 236, 125, 162, 3, 252, 203, 194, 97, 8, 89, 89, 206, 23, 196, 21, 168, 56, 27, 169, 230, 243, 241, 242, 133, 76, 244, 200, 180, 146, 32, 109, 155, 131, 134, 35, 93 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 245, 97, 224, 242, 101, 137, 225, 37, 33, 29, 17, 73, 113, 106, 80, 144, 196, 97, 131, 155, 103, 239, 1, 142, 168, 45, 250, 108, 22, 223, 164, 58, 108, 67, 177, 142, 91, 63, 107, 17, 28, 78, 204, 34, 245, 100, 186, 124, 249, 236, 157, 67, 87, 214, 26, 104, 193, 14, 173, 20, 160, 218, 133, 93 }, new byte[] { 237, 93, 44, 117, 23, 121, 233, 235, 34, 1, 72, 193, 37, 176, 78, 229, 200, 10, 235, 8, 146, 3, 173, 154, 175, 47, 24, 3, 192, 157, 23, 31, 250, 248, 241, 39, 244, 239, 33, 151, 177, 231, 180, 119, 60, 168, 120, 207, 10, 25, 136, 199, 105, 122, 109, 203, 166, 63, 144, 48, 152, 229, 177, 222, 170, 160, 117, 158, 45, 115, 19, 145, 206, 210, 111, 124, 52, 128, 183, 253, 141, 203, 183, 115, 98, 100, 249, 165, 28, 56, 253, 123, 245, 28, 135, 224, 82, 116, 184, 103, 174, 247, 63, 40, 27, 56, 207, 142, 148, 189, 161, 197, 114, 144, 22, 254, 144, 80, 232, 202, 109, 27, 230, 120, 185, 191, 17, 126 } });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProduct_ProductId",
                table: "StudentProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProduct_StudentId",
                table: "StudentProduct",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCategories_Categories_CategoryId",
                table: "StudentCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCategories_Products_ProductId",
                table: "StudentCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCategories_Categories_CategoryId",
                table: "StudentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCategories_Products_ProductId",
                table: "StudentCategories");

            migrationBuilder.DropTable(
                name: "StudentProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "StudentCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "StudentCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "CanteenKeeper");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 74, 81, 222, 42, 249, 101, 100, 110, 88, 8, 18, 6, 130, 250, 34, 47, 133, 136, 216, 197, 37, 5, 116, 245, 109, 116, 130, 91, 134, 213, 109, 106, 43, 135, 122, 77, 252, 132, 92, 126, 104, 5, 240, 108, 158, 179, 207, 134, 242, 96, 246, 73, 14, 129, 108, 216, 75, 149, 82, 40, 31, 140, 91, 79 }, new byte[] { 234, 2, 190, 149, 156, 94, 130, 234, 3, 47, 245, 252, 122, 149, 118, 34, 79, 135, 110, 215, 195, 74, 2, 100, 186, 23, 252, 177, 80, 20, 7, 143, 62, 99, 155, 219, 200, 122, 174, 135, 158, 52, 115, 112, 11, 20, 72, 248, 253, 103, 253, 159, 87, 124, 225, 117, 4, 183, 248, 152, 224, 179, 129, 236, 212, 200, 148, 238, 180, 54, 234, 247, 194, 42, 159, 59, 138, 146, 255, 237, 131, 132, 148, 19, 122, 165, 27, 134, 185, 248, 22, 255, 248, 140, 34, 102, 1, 189, 85, 94, 187, 179, 111, 89, 218, 207, 203, 166, 147, 243, 147, 174, 48, 236, 184, 16, 137, 98, 173, 81, 244, 9, 223, 229, 187, 51, 110, 182 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 202, 243, 30, 229, 151, 247, 97, 219, 30, 218, 121, 35, 227, 246, 53, 172, 145, 224, 237, 30, 131, 179, 243, 175, 28, 192, 11, 228, 168, 74, 229, 40, 99, 134, 35, 90, 65, 168, 185, 51, 94, 213, 241, 145, 207, 211, 65, 193, 208, 186, 95, 255, 162, 67, 36, 45, 204, 133, 194, 186, 146, 36, 217, 148 }, new byte[] { 252, 191, 246, 170, 214, 5, 208, 76, 163, 200, 247, 165, 137, 84, 62, 92, 3, 61, 67, 21, 167, 234, 107, 76, 204, 4, 217, 122, 202, 116, 128, 188, 248, 78, 58, 107, 92, 104, 190, 77, 165, 123, 207, 229, 200, 15, 180, 249, 170, 222, 87, 168, 207, 95, 21, 141, 123, 128, 223, 6, 153, 130, 188, 72, 211, 93, 245, 71, 187, 182, 107, 109, 163, 112, 112, 233, 24, 185, 49, 12, 124, 182, 47, 49, 154, 45, 170, 88, 139, 97, 202, 56, 103, 2, 194, 196, 206, 11, 230, 97, 179, 25, 199, 212, 27, 96, 245, 67, 145, 110, 162, 142, 129, 160, 20, 6, 76, 118, 57, 179, 8, 165, 44, 139, 21, 251, 195, 166 } });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCategories_Categories_CategoryId",
                table: "StudentCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCategories_Products_ProductId",
                table: "StudentCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
