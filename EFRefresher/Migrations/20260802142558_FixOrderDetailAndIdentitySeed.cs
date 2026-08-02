using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFRefresher.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderDetailAndIdentitySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderDetailId1",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderDetailId1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId1",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1000, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId1",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderDetailId1",
                table: "OrderDetails",
                column: "OrderDetailId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderDetails_OrderDetailId1",
                table: "OrderDetails",
                column: "OrderDetailId1",
                principalTable: "OrderDetails",
                principalColumn: "OrderDetailId");
        }
    }
}
