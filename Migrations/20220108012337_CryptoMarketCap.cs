using Microsoft.EntityFrameworkCore.Migrations;

namespace MagazinOnline.Migrations
{
    public partial class CryptoMarketCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellerID",
                table: "Cryptocurrency",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MarketCap",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketCapValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketCap", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CryptoMarketCap",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CryptocurrencyID = table.Column<int>(type: "int", nullable: false),
                    MarketCapID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoMarketCap", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CryptoMarketCap_Cryptocurrency_CryptocurrencyID",
                        column: x => x.CryptocurrencyID,
                        principalTable: "Cryptocurrency",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CryptoMarketCap_MarketCap_MarketCapID",
                        column: x => x.MarketCapID,
                        principalTable: "MarketCap",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrency_SellerID",
                table: "Cryptocurrency",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoMarketCap_CryptocurrencyID",
                table: "CryptoMarketCap",
                column: "CryptocurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoMarketCap_MarketCapID",
                table: "CryptoMarketCap",
                column: "MarketCapID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cryptocurrency_Seller_SellerID",
                table: "Cryptocurrency",
                column: "SellerID",
                principalTable: "Seller",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cryptocurrency_Seller_SellerID",
                table: "Cryptocurrency");

            migrationBuilder.DropTable(
                name: "CryptoMarketCap");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "MarketCap");

            migrationBuilder.DropIndex(
                name: "IX_Cryptocurrency_SellerID",
                table: "Cryptocurrency");

            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "Cryptocurrency");
        }
    }
}
