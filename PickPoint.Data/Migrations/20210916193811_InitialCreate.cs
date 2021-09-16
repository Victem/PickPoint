using Microsoft.EntityFrameworkCore.Migrations;

namespace PickPoint.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postamates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postamates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PostamateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Postamates_PostamateId",
                        column: x => x.PostamateId,
                        principalTable: "Postamates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Postamates",
                columns: new[] { "Id", "Address", "Status" },
                values: new object[,]
                {
                    { "0001-001", "Some place 1-1", true },
                    { "0001-002", "Some place 1-2", true },
                    { "0001-003", "Some place 1-3", false },
                    { "0002-010", "Some place 2-1", true },
                    { "0002-020", "Some place 2-2", false },
                    { "0002-030", "Some place 2-3", true },
                    { "0003-100", "Some place 3-1", true },
                    { "0003-200", "Some place 3-2", false },
                    { "0003-300", "Some place 3-3", false },
                    { "0004-120", "Some place 4-1", true },
                    { "0004-230", "Some place 4-2", true },
                    { "0004-340", "Some place 4-3", true }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Customer", "Items", "Phone", "PostamateId", "Price", "Status" },
                values: new object[,]
                {
                    { "Petrov", "[\"product1\",\"product2\"]", "+7111-222-33-44", "0001-001", 2000.50m, 1 },
                    { "Petrov", "[\"product1\",\"product2\",\"product6\"]", "+7111-222-33-44", "0001-001", 5000m, 1 },
                    { "Sidorov", "[\"product4\",\"product2\"]", "+7222-222-33-44", "0002-010", 499.99m, 1 },
                    { "Ivanov", "[\"product2\"]", "+7111-222-33-55", "0003-100", 300m, 1 },
                    { "Toporov", "[\"product5\"]", "+7111-222-33-66", "0004-230", 1000m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PostamateId",
                table: "Orders",
                column: "PostamateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Postamates");
        }
    }
}
