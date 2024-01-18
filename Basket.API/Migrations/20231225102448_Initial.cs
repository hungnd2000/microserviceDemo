using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerBasket",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBasket", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    BasketItemId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CustomerBasketId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.BasketItemId);
                    table.ForeignKey(
                        name: "FK_BasketItem_CustomerBasket_CustomerBasketId",
                        column: x => x.CustomerBasketId,
                        principalTable: "CustomerBasket",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_CustomerBasketId",
                table: "BasketItem",
                column: "CustomerBasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_ProductId",
                table: "BasketItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "CustomerBasket");
        }
    }
}
