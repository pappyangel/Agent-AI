using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFRefresher.Migrations
{
    /// <inheritdoc />
    public partial class SetIdentitySeedValues1000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1000, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1000, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1000, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1000, 1")
                .OldAnnotation("SqlServer:Identity", "1001, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1001, 1")
                .OldAnnotation("SqlServer:Identity", "1000, 1");
        }
    }
}
