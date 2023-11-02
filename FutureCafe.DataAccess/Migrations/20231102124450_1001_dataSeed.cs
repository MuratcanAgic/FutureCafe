using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FutureCafe.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _1001_dataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(754), null, "Gazlı içeçek", null },
                    { 2, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(765), null, "Çikolata", null },
                    { 3, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(766), null, "Kuruyemiş", null },
                    { 4, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(767), null, "Şakuteri", null }
                });

            migrationBuilder.InsertData(
                table: "Credits",
                columns: new[] { "Id", "Amount", "CreatedDate", "DeletedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 100m, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(2230), null, null },
                    { 2, 230m, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(2236), null, null }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "BuyingPrice", "CreatedDate", "DeletedDate", "SalePrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 15.2m, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(3627), null, 17.5m, null },
                    { 2, 60m, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(3634), null, 86.7m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "ImageUrl", "Name", "ProductBarcodNo", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(6224), null, null, null, "Beypazarı Maden Suyu", "8691381000486", null },
                    { 2, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(6228), null, null, null, "Ahir Tulum Peyniri", "8699118050490", null }
                });

            migrationBuilder.InsertData(
                table: "SchoolClasses",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(7336), null, "A", null },
                    { 2, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(7341), null, "B", null },
                    { 3, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(7341), null, "101", null },
                    { 4, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(7342), null, "102", null }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(4833), null, 1, null },
                    { 2, 3, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(4837), null, 2, null },
                    { 3, 4, new DateTime(2023, 11, 2, 15, 44, 49, 911, DateTimeKind.Local).AddTicks(4838), null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "ProductPrices",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "PriceId", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(6043), null, 1, 1, null },
                    { 2, new DateTime(2023, 11, 2, 15, 44, 49, 912, DateTimeKind.Local).AddTicks(6048), null, 2, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CardNumber", "CreatedDate", "DeletedDate", "ImageUrl", "Name", "SchoolClassId", "StudentSchoolNumber", "SurName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "50467084778", new DateTime(2023, 11, 2, 15, 44, 49, 913, DateTimeKind.Local).AddTicks(8960), null, null, "Fatmanur", 1, "3001", "Agiç", null },
                    { 2, "511750565", new DateTime(2023, 11, 2, 15, 44, 49, 913, DateTimeKind.Local).AddTicks(8969), null, null, "Muratcan", 1, "2894", "Agiç", null }
                });

            migrationBuilder.InsertData(
                table: "StudentCredits",
                columns: new[] { "Id", "CreatedDate", "CreditId", "DeletedDate", "StudentId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 2, 15, 44, 49, 914, DateTimeKind.Local).AddTicks(297), 1, null, 1, null },
                    { 2, new DateTime(2023, 11, 2, 15, 44, 49, 914, DateTimeKind.Local).AddTicks(302), 2, null, 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductPrices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductPrices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SchoolClasses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SchoolClasses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SchoolClasses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StudentCredits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentCredits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Credits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Credits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SchoolClasses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
