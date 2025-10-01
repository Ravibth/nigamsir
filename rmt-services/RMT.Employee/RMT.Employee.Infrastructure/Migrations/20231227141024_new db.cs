using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePreferences_PreferenceMasters_PreferedValue",
                table: "EmployeePreferences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePreferences_PreferedValue",
                table: "EmployeePreferences");

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 500L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 501L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 502L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 503L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 504L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 505L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 506L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 507L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 508L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 509L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 510L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 511L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 512L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 513L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 514L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 515L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 516L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 517L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 518L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 519L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 520L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 521L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 522L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 523L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 524L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 525L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 526L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 527L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 528L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 529L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 530L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 531L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 532L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 533L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 534L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 535L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 536L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 537L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 538L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 539L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 540L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 541L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 542L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 543L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 544L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 545L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 546L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 547L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 548L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 549L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 550L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 551L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 552L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 553L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 554L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 555L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 556L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 557L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 558L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 559L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 560L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 561L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 562L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 563L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 564L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 565L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 566L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 567L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 568L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 569L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 570L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 571L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 572L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 573L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 574L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 575L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 576L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 577L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 578L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 579L);

            migrationBuilder.DropColumn(
                name: "PreferedValue",
                table: "EmployeePreferences");

            migrationBuilder.DropColumn(
                name: "PreferenceOrder",
                table: "EmployeePreferences");

            migrationBuilder.AddColumn<string>(
                name: "PreferenceId",
                table: "EmployeePreferences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferenceName",
                table: "EmployeePreferences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3391), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3396) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3404), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3404) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3409), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3409) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3413), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3414) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3418), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3418) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3423), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3423) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3428), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3429) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3433), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3433) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3437), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3441), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3441) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3446), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3447) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3451), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3451) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3455), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3456) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3459), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3463), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3464) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3467), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3468) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3472), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3473) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3476), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3477) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3480), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3481) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3486), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3490), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3593), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3594) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3598), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3598) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3602), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3602) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3606), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3606) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3610), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3611) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3614), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3615) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3619), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3619) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3623), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3624) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3628), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3629) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3632), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3633) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3636), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3637) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3640), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3641) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3644), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3644) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3648), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3648) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3652), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3656), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3656) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3661), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3661) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3664), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3665) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3669), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3669) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3672), new DateTime(2023, 12, 27, 14, 10, 24, 98, DateTimeKind.Utc).AddTicks(3673) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferenceId",
                table: "EmployeePreferences");

            migrationBuilder.DropColumn(
                name: "PreferenceName",
                table: "EmployeePreferences");

            migrationBuilder.AddColumn<long>(
                name: "PreferedValue",
                table: "EmployeePreferences",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PreferenceOrder",
                table: "EmployeePreferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "EmployeePreferences",
                columns: new[] { "Id", "Category", "CreatedAt", "CreatedBy", "EmployeeEmail", "IsActive", "ModifiedAt", "ModifiedBy", "PreferedValue", "PreferenceOrder" },
                values: new object[,]
                {
                    { 500L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1913), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1914), "System", 100L, 1 },
                    { 501L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1918), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1918), "System", 106L, 1 },
                    { 502L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1920), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1921), "System", 111L, 1 },
                    { 503L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1922), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1923), "System", 116L, 1 },
                    { 504L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1925), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1926), "System", 121L, 1 },
                    { 505L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1928), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1928), "System", 126L, 1 },
                    { 506L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1931), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1931), "System", 131L, 1 },
                    { 507L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1933), "System", "john.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1933), "System", 136L, 1 },
                    { 508L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1935), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1935), "System", 100L, 1 },
                    { 509L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1977), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1977), "System", 106L, 1 },
                    { 510L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1980), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1980), "System", 111L, 1 },
                    { 511L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1982), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1982), "System", 116L, 1 },
                    { 512L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1984), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1985), "System", 121L, 1 },
                    { 513L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1987), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1987), "System", 126L, 1 },
                    { 514L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1990), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1991), "System", 131L, 1 },
                    { 515L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1992), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1993), "System", 136L, 1 },
                    { 516L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1995), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1995), "System", 100L, 1 },
                    { 517L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1997), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(1997), "System", 106L, 1 },
                    { 518L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2000), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2000), "System", 111L, 1 },
                    { 519L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2002), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2002), "System", 116L, 1 },
                    { 520L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2004), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2004), "System", 121L, 1 },
                    { 521L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2006), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2007), "System", 126L, 1 },
                    { 522L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2008), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2009), "System", 131L, 1 },
                    { 523L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2010), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2011), "System", 136L, 1 },
                    { 524L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2013), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2013), "System", 100L, 1 },
                    { 525L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2015), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2016), "System", 106L, 1 },
                    { 526L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2017), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2018), "System", 111L, 1 },
                    { 527L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2020), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2020), "System", 116L, 1 },
                    { 528L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2022), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2022), "System", 121L, 1 },
                    { 529L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2025), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2025), "System", 126L, 1 },
                    { 530L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2027), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2027), "System", 131L, 1 },
                    { 531L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2029), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2029), "System", 136L, 1 },
                    { 532L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2031), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2031), "System", 100L, 1 },
                    { 533L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2033), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2033), "System", 106L, 1 },
                    { 534L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2035), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2035), "System", 111L, 1 },
                    { 535L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2037), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2037), "System", 116L, 1 },
                    { 536L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2040), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2040), "System", 121L, 1 },
                    { 537L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2042), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2042), "System", 126L, 1 },
                    { 538L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2044), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2044), "System", 131L, 1 },
                    { 539L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2047), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2047), "System", 136L, 1 },
                    { 540L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2049), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2049), "System", 100L, 1 },
                    { 541L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2051), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2052), "System", 106L, 1 },
                    { 542L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2054), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2054), "System", 111L, 1 },
                    { 543L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2056), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2056), "System", 116L, 1 },
                    { 544L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2058), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2059), "System", 121L, 1 },
                    { 545L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2060), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2061), "System", 126L, 1 },
                    { 546L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2063), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2063), "System", 131L, 1 },
                    { 547L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2065), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2065), "System", 136L, 1 },
                    { 548L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2067), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2067), "System", 100L, 1 },
                    { 549L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2069), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2069), "System", 106L, 1 },
                    { 550L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2071), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2071), "System", 111L, 1 },
                    { 551L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2073), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2073), "System", 116L, 1 },
                    { 552L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2075), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2076), "System", 121L, 1 },
                    { 553L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2078), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2078), "System", 126L, 1 },
                    { 554L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2080), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2080), "System", 131L, 1 },
                    { 555L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2082), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2082), "System", 136L, 1 },
                    { 556L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2084), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2084), "System", 100L, 1 },
                    { 557L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2086), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2086), "System", 106L, 1 },
                    { 558L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2088), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2088), "System", 111L, 1 },
                    { 559L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2090), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2091), "System", 116L, 1 },
                    { 560L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2092), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2093), "System", 121L, 1 },
                    { 561L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2095), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2095), "System", 126L, 1 },
                    { 562L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2097), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2097), "System", 131L, 1 },
                    { 563L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2099), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2099), "System", 136L, 1 },
                    { 564L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2101), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2102), "System", 100L, 1 },
                    { 565L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2103), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2104), "System", 106L, 1 },
                    { 566L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2106), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2106), "System", 111L, 1 },
                    { 567L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2108), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2108), "System", 116L, 1 },
                    { 568L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2110), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2111), "System", 121L, 1 },
                    { 569L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2112), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2113), "System", 126L, 1 },
                    { 570L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2114), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2115), "System", 131L, 1 },
                    { 571L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2117), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2118), "System", 136L, 1 },
                    { 572L, "LOCATION", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2119), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2120), "System", 100L, 1 },
                    { 573L, "SME", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2123), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2123), "System", 106L, 1 },
                    { 574L, "REVENUE_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2126), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2126), "System", 111L, 1 },
                    { 575L, "EXPERTISE", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2128), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2128), "System", 116L, 1 },
                    { 576L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2130), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2130), "System", 121L, 1 },
                    { 577L, "BUISNESS_UNIT", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2187), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2187), "System", 126L, 1 },
                    { 578L, "INDUSTRY", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2190), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2190), "System", 131L, 1 },
                    { 579L, "SECTOR", new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2192), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 31, 2, 9, 20, 844, DateTimeKind.Utc).AddTicks(2192), "System", 136L, 1 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePreferences_PreferedValue",
                table: "EmployeePreferences",
                column: "PreferedValue");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePreferences_PreferenceMasters_PreferedValue",
                table: "EmployeePreferences",
                column: "PreferedValue",
                principalTable: "PreferenceMasters",
                principalColumn: "PreferenceMasterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
