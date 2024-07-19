using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarcloudTask.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class ErrorsDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ErrorsLog",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ErrorsLog");
        }
    }
}
