using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FutureCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1005_UserOperationClaimsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CanteenKeeper", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@futurecafe.com", "Muratcan", "Agiç", new byte[] { 127, 41, 4, 250, 164, 136, 179, 180, 131, 145, 176, 153, 29, 45, 98, 167, 237, 22, 158, 213, 235, 1, 73, 107, 123, 151, 3, 203, 205, 159, 75, 222, 95, 144, 242, 54, 36, 129, 22, 251, 246, 141, 171, 170, 51, 36, 196, 24, 225, 0, 193, 120, 128, 217, 123, 79, 222, 95, 217, 178, 245, 71, 188, 59 }, new byte[] { 210, 97, 221, 178, 188, 36, 125, 175, 145, 165, 45, 26, 93, 187, 237, 225, 185, 187, 152, 61, 123, 42, 64, 246, 135, 12, 229, 168, 152, 191, 17, 219, 150, 31, 95, 29, 27, 84, 171, 228, 200, 238, 213, 38, 8, 150, 143, 86, 191, 236, 212, 197, 150, 213, 153, 194, 217, 14, 111, 166, 134, 124, 193, 251, 55, 40, 255, 149, 53, 138, 201, 220, 207, 98, 98, 49, 16, 156, 229, 128, 39, 51, 2, 111, 215, 195, 169, 137, 172, 232, 114, 215, 7, 38, 122, 58, 34, 244, 29, 49, 85, 12, 108, 197, 13, 50, 19, 125, 85, 89, 134, 62, 121, 182, 49, 5, 102, 209, 119, 92, 31, 214, 71, 173, 148, 241, 100, 42 }, true, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "canteen@futurecafe.com", "Anıl", "Aktepe", new byte[] { 217, 47, 151, 172, 64, 15, 141, 52, 152, 164, 114, 174, 137, 168, 106, 243, 85, 124, 88, 239, 155, 9, 17, 196, 206, 197, 23, 7, 80, 160, 193, 98, 63, 59, 124, 34, 86, 60, 222, 29, 6, 176, 55, 98, 162, 116, 130, 141, 158, 20, 17, 122, 105, 139, 167, 33, 251, 168, 226, 59, 73, 213, 57, 249 }, new byte[] { 38, 110, 211, 147, 237, 137, 93, 30, 180, 237, 20, 135, 165, 219, 242, 210, 203, 139, 90, 35, 42, 71, 230, 194, 113, 5, 23, 231, 80, 77, 65, 187, 63, 165, 236, 129, 247, 5, 131, 68, 92, 66, 161, 106, 121, 167, 221, 235, 92, 199, 18, 62, 29, 104, 62, 38, 5, 164, 87, 61, 73, 165, 150, 28, 119, 198, 196, 181, 142, 22, 223, 247, 172, 215, 248, 130, 10, 199, 112, 250, 83, 100, 56, 117, 177, 122, 235, 130, 217, 193, 57, 233, 26, 220, 107, 201, 2, 145, 244, 33, 224, 18, 238, 79, 123, 30, 181, 10, 255, 179, 247, 128, 50, 170, 56, 71, 191, 93, 254, 104, 92, 231, 172, 136, 239, 183, 233, 47 }, true, null }
                });

            migrationBuilder.InsertData(
                table: "userOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_userOperationClaims_OperationClaimId",
                table: "userOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_userOperationClaims_UserId",
                table: "userOperationClaims",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userOperationClaims");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
