using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationLevelSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(1941), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(1943) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2470), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2471) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2475), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2475) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2478), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2478) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2480), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2480) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2482), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2483) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2485), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2485) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2487), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2487) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2489), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2489) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2491), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2492) });

            migrationBuilder.UpdateData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2275), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2275) });

            migrationBuilder.UpdateData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2278), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2278) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2520), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2565), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2565) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2522), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2528), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2528) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2525), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2526) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2530), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2581), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2581) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2532), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2533) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2513), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2514) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2569), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2569) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2517), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2518) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2571), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2574), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2574) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2576), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2576) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2583), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2584) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2578), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2579) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2586), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2586) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2603), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2603) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2595), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2596) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2591), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2591) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2588), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2588) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2598), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2599) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2607), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2607) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2601), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2601) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2593), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2593) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2614), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2614) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2620), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2621) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2624), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2624) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2626), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2626) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2629), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2631) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2634), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2634) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2637), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2637) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2640), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2641) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2643), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2643) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2645), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2646) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2648), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2648) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2650), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2651) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2652), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2653) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2655), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2655) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2657), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2658) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2659), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2676), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2676) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2678), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2679) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2609), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2612), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2612) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2681), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2681) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2683), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2684) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2687), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2687) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2689), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2689) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2691), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2692) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2696), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2696) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2698), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2699) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2701), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2701) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2703), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2704) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2705), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2706) });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "ConfigType", "CongigDisplayText", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 64L, "2", "Requisition_form", "Requisition", "Skill", "BUSINESS_UNIT", "Skills", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2662), "System", true, true, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2662), "System", "INTEGER" },
                    { 65L, "2", "Requisition_form", "Requisition", "Skill", "EXPERTISE", "Skills", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2664), "System", true, true, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2665), "System", "INTEGER" },
                    { 66L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "BUSINESS_UNIT", "Skills", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2671), "System", true, true, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2672), "System", "INTEGER" },
                    { 67L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "EXPERTISE", "Skills", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2674), "System", true, true, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2674), "System", "INTEGER" }
                });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2782), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2786) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2789), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2792), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2792) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2794), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2794) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2796), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2797) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2798), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2799) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2800), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2801) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2802), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2803) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2804), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2805) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2807), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2807) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2809), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2809) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2300), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2301) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2304), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2305) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2307), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2307) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2309), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2309) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2311), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2311) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2312), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2313) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2314), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2314) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2316), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2316) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2317), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2318) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2840), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2845), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2845) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2848), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2848) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2850), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2851) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2853), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2855) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "DisplayName", "InternalName", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2857), "User Management", "User Management", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2857) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2860), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2862), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2863) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2865), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2866) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2868), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2869) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2871), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2871) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2874), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2877), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2877) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2879), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "IsDisplay", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2882), true, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2883) });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "IsDisplay", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[] { 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2885), "System", "", "My skills", "My Skills", true, true, false, "", new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2886), "System", 92, "", "/myskill" });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2411), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2411) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2414), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2414) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2416), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2416) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2418), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2418) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2419), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2421), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2422) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2423), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2423) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2425), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2425) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2426), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2427) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2428), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2428) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2430), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2432), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2432) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2433), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2434) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2435), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2435) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2437), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2437) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2438), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2439) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2440), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2442), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2442) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2443), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2444) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2445), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2445) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2904), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2904) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2907), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2907) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2909), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2909) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2911), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2911) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2912), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2913) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2914), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2915) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2916), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2917) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2918), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2919) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2920), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2922), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2922) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2924), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2924) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2926), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2926) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2927), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2928) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2929), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2931), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2932) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2933), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2934) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2935), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2935) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2937), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2937) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2939), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2939) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2940), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2941) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2942), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2943) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2945), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2945) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2946), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2947) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2948), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2949) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2950), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2951) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3022), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3022) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3024), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3025) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3026), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3026) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3028), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3028) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3030), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3032), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3032) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3034), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3035) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3036), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3037) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3038), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3039) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3040), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3041) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3042), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3042) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3044), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3044) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3046), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3046) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3048), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3048) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3051), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3052) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3053), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3054) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3055), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3056) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3057), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3058) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3059), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3059) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3061), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3061) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 47L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3063), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3063) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3065), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3065) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3067), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3067) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 50L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3068), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3071), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3072) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3073), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3074) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3075), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3076) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3077), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3077) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3079), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3079) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3081), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3081) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3083), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3083) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3084), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3085) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3088), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3089) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3090), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3091) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3092), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3092) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3094), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3094) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3096), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3096) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 64L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3098), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3098) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 65L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3099), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 66L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3101), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3102) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 67L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3103), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3104) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 68L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3105), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3106) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 69L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3107), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3107) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3109), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3109) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3111), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3111) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3112), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3113) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3114), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3115) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 74L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3116), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3117) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3118), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3119) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3120), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3122), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3122) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3124), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3124) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 79L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3126), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3126) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 80L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3128), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3128) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 81L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3130), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 82L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3132), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3132) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 83L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3135), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3135) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 84L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3137), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3137) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3198), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3199) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3201), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3202) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3204), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3204) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3206), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3206) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3208), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3209) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3211), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3211) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3213), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3213) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3215), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3215) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3217), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3218) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3219), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3221), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3221) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3223), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3223) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3225), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3225) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3227), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3227) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3229), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3229) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3231) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3232), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3233) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3234), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3236) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3237), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3238) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3239), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3241), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3241) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3243), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3243) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3245), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3245) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3246), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3247) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3248), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3249) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3252), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3252) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3254), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3254) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3256), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3256) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3258), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3258) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3259), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3261), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3262) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3263), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3264) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3265), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3266) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3267), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3267) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3269), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3269) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3271), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3271) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3272), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3273) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3274), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3275) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3276), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3277) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3278), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3279) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3280), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3282), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3282) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3284), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3284) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3285), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3286) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3287), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3288) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3289), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 47L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3291), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3292) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3293), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3295), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3295) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 50L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3297), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3297) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 51L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3298), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3299) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3300), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3301) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3302), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3303) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3304), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3305) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3306), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3307) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3308), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3308) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3310), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3310) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3312), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3312) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3313), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3314) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3317), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3317) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3319), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3320) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3321), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3322) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3323), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3325) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 64L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3326), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3326) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 65L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3328), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3328) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 66L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3330), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3330) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 67L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3360), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3361) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 68L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3363), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3363) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 69L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3364), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3365) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3366), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3367) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3368), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3369) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3370), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3371) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3373), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3374) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 74L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3375), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3375) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3377), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3377) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3379), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3379) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3380), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3381) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3382), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3383) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 79L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3384), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3385) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 80L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3386), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3387) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 81L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3388), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3388) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 82L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3390), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 83L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3392), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3392) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 84L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3393), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3394) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 85L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3395), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3396) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 86L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3397), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3397) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 87L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3399), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3399) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 88L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3402), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3402) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 89L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3404), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3405) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 90L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3406), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3407) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 91L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3408), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3408) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 92L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3410), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3410) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 93L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3412), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3412) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 94L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3413), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3414) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 95L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3415), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3416) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 96L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3417), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3418) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 97L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3419), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3419) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 98L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3421), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3421) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 99L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3422), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3423) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3424), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3425) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3426), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3426) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3428), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3428) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3430), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3431), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3432) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3433), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3434) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3435), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3435) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3437), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3439), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3439) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3440), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3441) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3442), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3443) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3444), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3444) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3446), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3446) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3448), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3448) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3449), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3451), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3452) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3453), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3453) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3455), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3455) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3457), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3457) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3459), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2375), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2376) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2378), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2379) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2380), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2381) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2382), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2383) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2384), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2385) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2386), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2386) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2388), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2388) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2389), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2391), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2391) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2393), new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(2393) });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 120L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3460), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3461), "System", "Leader" },
                    { 121L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3462), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3463), "System", "Admin" },
                    { 122L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3464), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3464), "System", "Additional Delegate" },
                    { 123L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3466), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3466), "System", "Additional EL" },
                    { 124L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3468), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3468), "System", "Request Reviewer" },
                    { 125L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3470), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3471), "System", "Delegate" },
                    { 126L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3472), "System", true, 16L, new DateTime(2024, 2, 19, 17, 14, 31, 252, DateTimeKind.Utc).AddTicks(3473), "System", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.UpdateData(
                table: "ApplicationLevelSettings",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6633), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7187), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7188) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7194), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7194) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7197), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7197) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7200), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7200) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7202), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7203) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7205), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7205) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7208), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7208) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7211), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7211) });

            migrationBuilder.UpdateData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7214), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7214) });

            migrationBuilder.UpdateData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6926), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6932), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(6933) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7279), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7279) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7298), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7298) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7281), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7282) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7288), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7285), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7286) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7292), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7292) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7316), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7317) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7295), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7295) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7270), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7272) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7301), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7301) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7275), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7276) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7304), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7305) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7307), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7308) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7310), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7311) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7319), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7319) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7313), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7314) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7322), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7322) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7345), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7346) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7335), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7335) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7329), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7329) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7326), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7327) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7337), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7339) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7348), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7348) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7342), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7343) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7332), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7332) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7357), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7357) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7360), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7360) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7362), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7363) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7365), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7366) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7368), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7368) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7370), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7371) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7373), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7374) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7377), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7378) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7380), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7383), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7383) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7386), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7386) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7388), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7389) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7392), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7393) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7395), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7395) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7398), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7398) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7400), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7401) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7405), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7412), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7413) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7351), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7352) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7354), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7355) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7419), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7419) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7422), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7422) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7424), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7425) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7427), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7428) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7430), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7434), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7435) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7439), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7443), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7443) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7446), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7446) });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7449), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7449) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7526), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7534), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7534) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7536), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7537) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7539), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7542), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7543) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7546), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7546) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7548), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7549) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7551), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7552) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7554), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7555) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7557), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7558) });

            migrationBuilder.UpdateData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7560), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7561) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7000), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7002) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7005), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7007) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7009), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7009) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7011), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7011) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7013), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7013) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7015), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7015) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7017), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7017) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7019), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7019) });

            migrationBuilder.UpdateData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7021), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7022) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7596), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7597) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7601), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7603) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7606), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7607) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7610), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7611) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7614), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7615) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "DisplayName", "InternalName", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7618), "User Onboarding", "User Onboarding", new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7619) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7622), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7622) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7626), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7626) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7631), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7631) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7634), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7635) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7638), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7638) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7641), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7642) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7644), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7645) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7648), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7648) });

            migrationBuilder.UpdateData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "IsDisplay", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(6683), false, new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(6684) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7109), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7113), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7114) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7118), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7118) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7120), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7121) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7123), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7123) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7125), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7127), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7127) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7129), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7129) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7131), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7131) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7133), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7133) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7135), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7135) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7137), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7137) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7139), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7139) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7141), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7141) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7143), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7143) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7145), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7145) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7147), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7147) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7148), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7149) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7150), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7151) });

            migrationBuilder.UpdateData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7152), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7153) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7680), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7682) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7685), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7686) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7688), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7689) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7691), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7691) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7693), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7693) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7695), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7696) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7698), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7698) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7700), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7702), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7705), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7705) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7708), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7709) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7710), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7711) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7713), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7715), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7716) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7718), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7718) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7720), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7721) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7722), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7723) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7725), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7727), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7732), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7732) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7735), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7735) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7737), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7738) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7739), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7742), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7742) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7775), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7776) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7777), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7778) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7780), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7782), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7782) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7784), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7785) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7787), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7788) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7790), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7792), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7793) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7795), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7795) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7797), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7799), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7802), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7802) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7804), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7805) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7808), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7808) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7810), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7811) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7814), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7814) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7816), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7817) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7818), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7819) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7821), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7821) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7823), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7823) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7825), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7826) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7827), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7828) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 47L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7830), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7832), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7833) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7834), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7835) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 50L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7838), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7838) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7840), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7841) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7842), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7843) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7845), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7845) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7847), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7848) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7849), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7852), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7852) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7854), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7855) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7856), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7857) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7860), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7862), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7862) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7864), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7866), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7867) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7868), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7869) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 64L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7870), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7871) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 65L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7873), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7873) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 66L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7875), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7876) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 67L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7877), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7878) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 68L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7879), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7880) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 69L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7887), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7887) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7889), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7891), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7892) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7893), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7894) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7896), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7896) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 74L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7898), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7898) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7900), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7901) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7902), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7903) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7904), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7905) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7907), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7908) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 79L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7910), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 80L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7912), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7913) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 81L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7914), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7915) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 82L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7917), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7917) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 83L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7919), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 84L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7921), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7922) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8008), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8009) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8014), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8015) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8017), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8018) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8020), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8021) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8023), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8024) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8026), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8026) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8028), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8029) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8030), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8031) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8033), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8034) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8035), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8036) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8038), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8039) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8040), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8043) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8046), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8046) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8048), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8049) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8051), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8051) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8053), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8054) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8055), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8057), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8058) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8059), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8062), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8062) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8064), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8064) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8066), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8066) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8068), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8068) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8070), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8072), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8073) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8075), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8075) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8077), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8077) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8079), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8081), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8082) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8083), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8084) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8085), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8086) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8087), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8088) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8090), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8091) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8092), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8093) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8094), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8095) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8097), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8097) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8099), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8101), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8102) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8103), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8104) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8105), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8106) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8107), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8109), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8111) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8113), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8113) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8115), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8115) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8117), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8117) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8122), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8123) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 47L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8124), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8125) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8127), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8127) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8129), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8130) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 50L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8131), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8132) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 51L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8133), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8134) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8137), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8137) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8139), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8140) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8141), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8142) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8144), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8144) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8146), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8146) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8148), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8148) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8150), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8151) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8152), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8153) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8154), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8155) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8157), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8157) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8160), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8160) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8162), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 64L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8164), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8164) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 65L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8166), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8166) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 66L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8220), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8221) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 67L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8222), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8223) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 68L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8225), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8225) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 69L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8227), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8228) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8229), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8230) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8232), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8232) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8235), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8236) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8237), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8238) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 74L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8240), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8240) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8242), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8243) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8244), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8245) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8247), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8248) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8249), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8250) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 79L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8252), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8252) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 80L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8254), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8255) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 81L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8257), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8258) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 82L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8260), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 83L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8262), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8263) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 84L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8264), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8265) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 85L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8267), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8267) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 86L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8269), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8270) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 87L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8272), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8272) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 88L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8274), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8275) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 89L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8276), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8277) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 90L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8278), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8279) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 91L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8285), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8287) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 92L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8290), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8290) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 93L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8293), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8293) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 94L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8295), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8296) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 95L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8297), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8298) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 96L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8299), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 97L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8302), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8302) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 98L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8304), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8305) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 99L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8308), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8309) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8311), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8311) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8314), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8315) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8317), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8317) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8319), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8320) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8322), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8323) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8325), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8325) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8328), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8329) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8331), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8332) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8335), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8336) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8338), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8338) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8341), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8341) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8343), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8343) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8345), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(8346) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7870), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7871) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7875), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7876) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7881), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7882) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7886), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7887) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7890), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7891) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7894), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7895) });

            migrationBuilder.UpdateData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7898), new DateTime(2024, 2, 10, 9, 50, 19, 366, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7054), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7054) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7057), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7058) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7060), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7061) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7063), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7063) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7065), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7066) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7068), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7070) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7071), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7072) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7073), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7074) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7076), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7077) });

            migrationBuilder.UpdateData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7078), new DateTime(2024, 2, 7, 1, 18, 5, 748, DateTimeKind.Utc).AddTicks(7079) });
        }
    }
}
