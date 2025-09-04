using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class handeledorderpaymentconfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_PaymentMethodId",
                table: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "SavedPaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_OrderId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "RefundReferenceId",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "PaymentTransactions",
                newName: "OrderId1");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_PaymentMethodId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_OrderId1");

            migrationBuilder.AlterTable(
                name: "PaymentTransactions",
                comment: "Stores all payment transactions information");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PaymentTransactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Payment status: Pending, Success, Failed, Refunded",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: false,
                comment: "Unique identifier for payment transaction",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "PaymentTransactions",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "PaymentTransactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_BuyerId",
                table: "PaymentTransactions",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_CreatedAt",
                table: "PaymentTransactions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_Method",
                table: "PaymentTransactions",
                column: "Method");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_OrderId",
                table: "PaymentTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_ProviderTxnId",
                table: "PaymentTransactions",
                column: "ProviderTxnId",
                unique: true,
                filter: "[ProviderTxnId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_Status",
                table: "PaymentTransactions",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId",
                table: "PaymentTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId1",
                table: "PaymentTransactions",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId1",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_BuyerId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_CreatedAt",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_Method",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_OrderId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_ProviderTxnId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_Status",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "OrderId1",
                table: "PaymentTransactions",
                newName: "PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_OrderId1",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_PaymentMethodId");

            migrationBuilder.AlterTable(
                name: "PaymentTransactions",
                oldComment: "Stores all payment transactions information");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PaymentTransactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Payment status: Pending, Success, Failed, Refunded");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Unique identifier for payment transaction");

            migrationBuilder.AddColumn<string>(
                name: "RefundReferenceId",
                table: "PaymentTransactions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Last4 = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    SavedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedPaymentMethods_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedPaymentMethods_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_OrderId",
                table: "PaymentTransactions",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedPaymentMethods_BuyerId",
                table: "SavedPaymentMethods",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPaymentMethods_PaymentMethodId",
                table: "SavedPaymentMethods",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Orders_OrderId",
                table: "PaymentTransactions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_PaymentMethodId",
                table: "PaymentTransactions",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
