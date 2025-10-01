using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataseedingpreferenceordercol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferenceOrder",
                table: "EmployeePreferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 500L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1913), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1914), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 501L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1918), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1918), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 502L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1920), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1921), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 503L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1922), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1923), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 504L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1925), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1926), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 505L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1928), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1928), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 506L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1931), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1931), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 507L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1933), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1933), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 508L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1935), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1935), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 509L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1977), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1977), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 510L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1980), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1980), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 511L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1982), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1982), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 512L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1984), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1985), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 513L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1987), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1987), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 514L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1990), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1991), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 515L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1992), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1993), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 516L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1995), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1995), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 517L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1997), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1997), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 518L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2000), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2000), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 519L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2002), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2002), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 520L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2004), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2004), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 521L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2006), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2007), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 522L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2008), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2009), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 523L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2010), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2011), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 524L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2013), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2013), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 525L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2015), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2016), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 526L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2017), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2018), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 527L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2020), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2020), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 528L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2022), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2022), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 529L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2025), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2025), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 530L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2027), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2027), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 531L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2029), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2029), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 532L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2031), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2031), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 533L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2033), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2033), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 534L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2035), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2035), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 535L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2037), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2037), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 536L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2040), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2040), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 537L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2042), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2042), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 538L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2044), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2044), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 539L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2047), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2047), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 540L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2049), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2049), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 541L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2051), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2052), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 542L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2054), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2054), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 543L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2056), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2056), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 544L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2058), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2059), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 545L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2060), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2061), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 546L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2063), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2063), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 547L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2065), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2065), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 548L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2067), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2067), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 549L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2069), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2069), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 550L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2071), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2071), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 551L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2073), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2073), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 552L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2075), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2076), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 553L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2078), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2078), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 554L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2080), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2080), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 555L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2082), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2082), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 556L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2084), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2084), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 557L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2086), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2086), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 558L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2088), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2088), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 559L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2090), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2091), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 560L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2092), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2093), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 561L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2095), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2095), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 562L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2097), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2097), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 563L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2099), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2099), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 564L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2101), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2102), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 565L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2103), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2104), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 566L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2106), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2106), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 567L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2108), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2108), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 568L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2110), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2111), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 569L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2112), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2113), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 570L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2114), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2115), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 571L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2117), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2118), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 572L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2119), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2120), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 573L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2123), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2123), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 574L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2126), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2126), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 575L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2128), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2128), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 576L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2130), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2130), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 577L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2187), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2187), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 578L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2190), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2190), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 579L,
                columns: new[] { "CreatedAt", "ModifiedAt", "PreferenceOrder" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2192), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2192), 1 });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2314), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2314) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2317), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2318) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2320), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2322), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2322) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2323), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2324) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2325), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2325) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2327), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2327) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2330), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2332), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2332) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2335), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2335) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2336), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2337) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2339), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2339) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2341), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2341) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2343), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2343) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2345), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2345) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2347), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2347) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2349), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2349) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2351), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2351) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2354), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2354) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2356), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2356) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2358), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2358) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2360), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2362), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2362) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2365), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2365) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2367), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2367) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2369), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2372), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2372) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2374), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2375) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2376), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2377) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2379), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2379) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2381), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2381) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2383), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2383) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2385), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2385) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2387), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2388) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2389), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2389) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2391), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2391) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2393), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2393) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2395), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2395) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2397), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2397) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2399), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2399) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2401), new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2401) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferenceOrder",
                table: "EmployeePreferences");

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 500L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5179), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5182) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 501L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5189), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 502L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5194), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5195) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 503L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5198), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 504L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5204), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5205) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 505L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5210), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5211) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 506L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5215), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5215) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 507L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5219), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 508L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5224), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5224) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 509L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5228), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5229) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 510L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5232), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5233) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 511L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5237), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5237) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 512L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5241), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5242) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 513L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5246), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5248) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 514L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5252), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5252) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 515L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5256), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5256) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 516L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5260), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5261) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 517L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5264), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5265) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 518L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5268), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5269) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 519L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5273), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5274) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 520L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5278), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5278) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 521L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5282), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5282) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 522L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5286), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5287) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 523L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5292), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5292) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 524L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5296), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5297) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 525L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5300), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5301) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 526L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5305), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5305) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 527L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5309), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 528L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5313), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5314) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 529L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5317), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5319) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 530L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5322), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5323) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 531L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5326), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5327) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 532L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5330), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5331) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 533L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5338), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5339) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 534L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5344), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5345) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 535L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5349), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 536L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5355), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5356) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 537L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5360), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5361) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 538L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5367), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5368) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 539L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5373), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5374) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 540L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5378), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5379) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 541L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5384), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5385) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 542L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5390), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 543L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5397), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5398) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 544L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5404), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5404) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 545L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5409), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5410) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 546L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5414), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5415) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 547L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5419), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 548L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5424), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5424) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 549L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5428), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5429) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 550L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5433), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5434) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 551L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5440), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5441) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 552L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5446), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5447) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 553L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5453), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5454) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 554L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5460), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5461) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 555L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5674), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5674) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 556L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5679), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 557L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5684), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5684) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 558L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5688), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5688) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 559L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5692), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5693) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 560L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5697), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5697) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 561L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5702), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5703) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 562L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5707), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5707) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 563L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5711), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5712) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 564L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5716), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5717) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 565L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5720), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5721) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 566L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5724), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5725) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 567L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5729), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5729) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 568L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5733), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5734) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 569L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5738), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5738) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 570L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5742), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5742) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 571L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5746), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5747) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 572L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5750), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5751) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 573L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5755), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5755) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 574L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5759), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 575L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5763), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5764) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 576L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5768), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5768) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 577L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5772), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5773) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 578L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5776), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5777) });

            migrationBuilder.UpdateData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 579L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5781), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5781) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6047), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6048) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6053), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6054) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6058), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6059) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6062), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6063) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6066), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6067) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6071), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6071) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6075), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6076) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6080), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6084), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6084) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6088), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6088) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6092), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6092) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6096), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6097) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6101), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6101) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6105), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6106) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6109), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6113), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6114) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6117), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6118) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6122), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6122) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6126), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6127) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6130), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6131) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6134), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6135) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6139), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6260), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6261) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6265), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6270), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6274), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6275) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6279), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6279) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6284), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6284) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6288), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6289) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6293), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6293) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6298), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6299) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6303), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6303) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6307), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6307) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6311), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6312) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6315), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6316) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6319), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6323), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6324) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6327), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6328) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6331), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6332) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6336), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6336) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6339), new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6340) });
        }
    }
}
