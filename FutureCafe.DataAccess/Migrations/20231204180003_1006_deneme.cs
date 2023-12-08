using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1006_deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userOperationClaims_OperationClaims_OperationClaimId",
                table: "userOperationClaims");

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
                name: "FK_userOperationClaims_OperationClaims_OperationClaimId",
                table: "userOperationClaims",
                column: "OperationClaimId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userOperationClaims_OperationClaims_OperationClaimId",
                table: "userOperationClaims");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 127, 41, 4, 250, 164, 136, 179, 180, 131, 145, 176, 153, 29, 45, 98, 167, 237, 22, 158, 213, 235, 1, 73, 107, 123, 151, 3, 203, 205, 159, 75, 222, 95, 144, 242, 54, 36, 129, 22, 251, 246, 141, 171, 170, 51, 36, 196, 24, 225, 0, 193, 120, 128, 217, 123, 79, 222, 95, 217, 178, 245, 71, 188, 59 }, new byte[] { 210, 97, 221, 178, 188, 36, 125, 175, 145, 165, 45, 26, 93, 187, 237, 225, 185, 187, 152, 61, 123, 42, 64, 246, 135, 12, 229, 168, 152, 191, 17, 219, 150, 31, 95, 29, 27, 84, 171, 228, 200, 238, 213, 38, 8, 150, 143, 86, 191, 236, 212, 197, 150, 213, 153, 194, 217, 14, 111, 166, 134, 124, 193, 251, 55, 40, 255, 149, 53, 138, 201, 220, 207, 98, 98, 49, 16, 156, 229, 128, 39, 51, 2, 111, 215, 195, 169, 137, 172, 232, 114, 215, 7, 38, 122, 58, 34, 244, 29, 49, 85, 12, 108, 197, 13, 50, 19, 125, 85, 89, 134, 62, 121, 182, 49, 5, 102, 209, 119, 92, 31, 214, 71, 173, 148, 241, 100, 42 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 217, 47, 151, 172, 64, 15, 141, 52, 152, 164, 114, 174, 137, 168, 106, 243, 85, 124, 88, 239, 155, 9, 17, 196, 206, 197, 23, 7, 80, 160, 193, 98, 63, 59, 124, 34, 86, 60, 222, 29, 6, 176, 55, 98, 162, 116, 130, 141, 158, 20, 17, 122, 105, 139, 167, 33, 251, 168, 226, 59, 73, 213, 57, 249 }, new byte[] { 38, 110, 211, 147, 237, 137, 93, 30, 180, 237, 20, 135, 165, 219, 242, 210, 203, 139, 90, 35, 42, 71, 230, 194, 113, 5, 23, 231, 80, 77, 65, 187, 63, 165, 236, 129, 247, 5, 131, 68, 92, 66, 161, 106, 121, 167, 221, 235, 92, 199, 18, 62, 29, 104, 62, 38, 5, 164, 87, 61, 73, 165, 150, 28, 119, 198, 196, 181, 142, 22, 223, 247, 172, 215, 248, 130, 10, 199, 112, 250, 83, 100, 56, 117, 177, 122, 235, 130, 217, 193, 57, 233, 26, 220, 107, 201, 2, 145, 244, 33, 224, 18, 238, 79, 123, 30, 181, 10, 255, 179, 247, 128, 50, 170, 56, 71, 191, 93, 254, 104, 92, 231, 172, 136, 239, 183, 233, 47 } });

            migrationBuilder.AddForeignKey(
                name: "FK_userOperationClaims_OperationClaims_OperationClaimId",
                table: "userOperationClaims",
                column: "OperationClaimId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
