using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Data.Migrations
{
    public partial class ver4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "ProductDetails");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateCreated",
            //    table: "Products",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateModified",
            //    table: "Products",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Products",
                type: "ntext",
                nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "UserCreated",
            //    table: "Products",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "UserModified",
            //    table: "Products",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "ProductDetails",
                type: "ntext",
                nullable: true);
        }
    }
}
