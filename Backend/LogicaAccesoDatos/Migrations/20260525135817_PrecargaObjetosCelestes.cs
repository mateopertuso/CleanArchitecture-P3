using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class PrecargaObjetosCelestes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ObjetosCelestes",
                columns: new[] { "Id", "MagnitudAparente", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { 1, 0.46m, "Saturno", 0 },
                    { 2, -2.20m, "Júpiter", 0 },
                    { 3, -1.10m, "Marte", 0 },
                    { 4, -4.40m, "Venus", 0 },
                    { 5, 4.00m, "M42", 2 },
                    { 6, 6.00m, "Nebulosa Laguna", 2 },
                    { 7, 3.44m, "Andrómeda", 1 },
                    { 8, 8.40m, "Galaxia del Remolino", 1 },
                    { 9, 1.98m, "Polaris", 3 },
                    { 10, -1.46m, "Sirio", 3 },
                    { 11, 0.42m, "Betelgeuse", 3 },
                    { 12, 9.10m, "NGC 2392", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ObjetosCelestes",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
