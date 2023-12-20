using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1008_BanProductToUserEntitiesUpdatedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProduct_Products_ProductId",
                table: "StudentProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProduct_Students_StudentId",
                table: "StudentProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProduct",
                table: "StudentProduct");

            migrationBuilder.RenameTable(
                name: "StudentProduct",
                newName: "StudentProducts");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProduct_StudentId",
                table: "StudentProducts",
                newName: "IX_StudentProducts_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProduct_ProductId",
                table: "StudentProducts",
                newName: "IX_StudentProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProducts",
                table: "StudentProducts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 42, 221, 211, 234, 129, 229, 243, 38, 32, 230, 144, 108, 119, 77, 166, 81, 168, 163, 69, 211, 175, 213, 248, 65, 148, 24, 132, 143, 182, 104, 181, 60, 102, 207, 70, 67, 70, 21, 161, 46, 7, 253, 146, 172, 206, 201, 115, 229, 179, 149, 249, 207, 59, 230, 170, 66, 50, 91, 71, 7, 83, 114, 200, 106 }, new byte[] { 28, 63, 219, 150, 60, 137, 2, 239, 221, 177, 255, 5, 208, 112, 118, 83, 187, 165, 167, 116, 32, 152, 213, 26, 74, 26, 157, 99, 92, 246, 96, 70, 56, 247, 217, 69, 150, 123, 173, 230, 109, 216, 246, 33, 123, 77, 220, 64, 158, 68, 146, 93, 13, 2, 144, 31, 244, 96, 243, 122, 243, 219, 41, 154, 219, 145, 222, 163, 189, 47, 168, 23, 158, 31, 220, 96, 155, 213, 228, 255, 41, 4, 30, 96, 28, 198, 52, 251, 30, 35, 162, 163, 105, 151, 72, 218, 92, 201, 171, 18, 82, 109, 124, 153, 189, 122, 157, 178, 74, 112, 222, 229, 84, 184, 240, 21, 81, 1, 13, 14, 48, 176, 229, 133, 129, 155, 64, 30 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 255, 119, 69, 126, 218, 42, 107, 239, 125, 28, 234, 126, 251, 167, 58, 74, 211, 40, 44, 100, 152, 172, 128, 76, 252, 98, 108, 224, 31, 252, 212, 116, 50, 34, 121, 236, 174, 205, 67, 167, 138, 107, 128, 244, 238, 209, 64, 29, 52, 4, 154, 185, 202, 181, 200, 236, 139, 81, 207, 194, 74, 174, 246, 63 }, new byte[] { 71, 132, 46, 21, 255, 130, 1, 185, 28, 91, 47, 85, 102, 200, 1, 172, 209, 97, 208, 230, 28, 107, 234, 87, 244, 183, 41, 199, 115, 215, 1, 238, 72, 188, 22, 54, 175, 69, 135, 194, 137, 243, 46, 53, 179, 219, 173, 193, 84, 33, 45, 223, 39, 183, 52, 173, 189, 172, 222, 88, 162, 94, 47, 50, 93, 240, 179, 127, 0, 159, 24, 139, 126, 163, 92, 3, 181, 138, 89, 5, 56, 22, 164, 125, 250, 94, 57, 48, 98, 80, 153, 84, 14, 77, 63, 237, 20, 249, 99, 117, 158, 139, 170, 103, 220, 162, 160, 203, 95, 41, 96, 103, 144, 83, 65, 207, 228, 83, 151, 214, 225, 93, 156, 85, 253, 70, 100, 122 } });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProducts_Products_ProductId",
                table: "StudentProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProducts_Students_StudentId",
                table: "StudentProducts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProducts_Products_ProductId",
                table: "StudentProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentProducts_Students_StudentId",
                table: "StudentProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProducts",
                table: "StudentProducts");

            migrationBuilder.RenameTable(
                name: "StudentProducts",
                newName: "StudentProduct");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProducts_StudentId",
                table: "StudentProduct",
                newName: "IX_StudentProduct_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentProducts_ProductId",
                table: "StudentProduct",
                newName: "IX_StudentProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProduct",
                table: "StudentProduct",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProduct_Products_ProductId",
                table: "StudentProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProduct_Students_StudentId",
                table: "StudentProduct",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
