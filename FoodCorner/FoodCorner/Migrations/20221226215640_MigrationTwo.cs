using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCorner.Migrations
{
    public partial class MigrationTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantID",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_OrderedById",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Food_FoodID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Restaurant_RestaurantID",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Restaurant",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "Foods");

            migrationBuilder.RenameIndex(
                name: "IX_Review_RestaurantID",
                table: "Reviews",
                newName: "IX_Reviews_RestaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderedById",
                table: "Orders",
                newName: "IX_Orders_OrderedById");

            migrationBuilder.RenameIndex(
                name: "IX_Order_FoodID",
                table: "Orders",
                newName: "IX_Orders_FoodID");

            migrationBuilder.RenameIndex(
                name: "IX_Food_RestaurantID",
                table: "Foods",
                newName: "IX_Foods_RestaurantID");

            migrationBuilder.AlterColumn<string>(
                name: "OrderedById",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "RestaurantID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
                column: "FoodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Restaurants_RestaurantID",
                table: "Foods",
                column: "RestaurantID",
                principalTable: "Restaurants",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_OrderedById",
                table: "Orders",
                column: "OrderedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Foods_FoodID",
                table: "Orders",
                column: "FoodID",
                principalTable: "Foods",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantID",
                table: "Reviews",
                column: "RestaurantID",
                principalTable: "Restaurants",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Restaurants_RestaurantID",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_OrderedById",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Foods_FoodID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantID",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurant");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "Food");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RestaurantID",
                table: "Review",
                newName: "IX_Review_RestaurantID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderedById",
                table: "Order",
                newName: "IX_Order_OrderedById");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_FoodID",
                table: "Order",
                newName: "IX_Order_FoodID");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_RestaurantID",
                table: "Food",
                newName: "IX_Food_RestaurantID");

            migrationBuilder.AlterColumn<string>(
                name: "OrderedById",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant",
                column: "RestaurantID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "FoodID");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantID",
                table: "Food",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_OrderedById",
                table: "Order",
                column: "OrderedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Food_FoodID",
                table: "Order",
                column: "FoodID",
                principalTable: "Food",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Restaurant_RestaurantID",
                table: "Review",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
