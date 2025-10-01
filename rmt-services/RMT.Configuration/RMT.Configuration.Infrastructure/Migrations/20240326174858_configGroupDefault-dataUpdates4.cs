using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configGroupDefaultdataUpdates4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bu_Experties_Grps");

            migrationBuilder.DropTable(
                name: "BusinessUnitMasters");

            migrationBuilder.DropTable(
                name: "ExpertiesMasters");

            migrationBuilder.DropTable(
                name: "RUMaster");

            migrationBuilder.DropTable(
                name: "SMEGMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessUnitMasters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuId = table.Column<string>(type: "text", nullable: false),
                    BusinessUnit = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnitMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiesMasters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Experties = table.Column<string>(type: "text", nullable: false),
                    ExpertiesId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiesMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RUMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    RU = table.Column<string>(type: "text", nullable: false),
                    RUId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RUMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMEGMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    SMEG = table.Column<string>(type: "text", nullable: false),
                    SMEGId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMEGMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bu_Experties_Grps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessUnitId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertiesId = table.Column<long>(type: "bigint", nullable: false),
                    RUId = table.Column<long>(type: "bigint", nullable: true),
                    SMEGId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessUnitName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ExpertiesName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bu_Experties_Grps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bu_Experties_Grps_BusinessUnitMasters_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnitMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bu_Experties_Grps_ExpertiesMasters_ExpertiesId",
                        column: x => x.ExpertiesId,
                        principalTable: "ExpertiesMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bu_Experties_Grps_RUMaster_RUId",
                        column: x => x.RUId,
                        principalTable: "RUMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bu_Experties_Grps_SMEGMaster_SMEGId",
                        column: x => x.SMEGId,
                        principalTable: "SMEGMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BusinessUnitMasters",
                columns: new[] { "Id", "BuId", "BusinessUnit", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, "SG000010", "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, "SG000013", "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "ExpertiesMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Experties", "ExpertiesId", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NDO", "SL000060", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "SL000015", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "SL000043", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "SL000046", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Governance Risk & Operations (GRO)", "SL000049", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NErcO", "SL000061", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Cyber & IT Risk (Cyber)", "SL000022", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "FS Risk", "SL000027", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Forensic", "SL000021", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "RUMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "RU", "RUId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "RV007793" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Asset tracing & recovery", "RV007630" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Transaction Tax", "RV007599" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Compliance", "RV007603" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Litigation", "RV007604" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Assessment", "RV007605" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Attest", "RV007606" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Regulatory", "RV007607" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Advisory", "RV007608" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Due Diligence", "RV007612" },
                    { 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO - Analytics / CCM / RPA", "RV007749" },
                    { 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "(CSRS) - Energy Advisory", "RV007750" },
                    { 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO - Internal Audit", "RV007751" },
                    { 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO- Standard operating procedure", "RV007752" },
                    { 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Governance Risk & Operations (GRO)", "RV007753" },
                    { 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "RV007794" },
                    { 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - Cyber", "RV007801" },
                    { 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - FS Risk", "RV007802" },
                    { 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Forensic", "RV007808" },
                    { 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC GD UK JV", "RV007812" }
                });

            migrationBuilder.InsertData(
                table: "SMEGMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "SMEG", "SMEGId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "IS001165" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "IS001124" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "IS001116" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "IS001120" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO", "IS001154" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "IS001166" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD - Cyber", "IS001148" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD - FS Risk", "IS001153" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Forensic", "IS001150" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD UK JV - FS Risk", "IS001152" }
                });

            migrationBuilder.InsertData(
                table: "Bu_Experties_Grps",
                columns: new[] { "Id", "BusinessUnitId", "BusinessUnitName", "CreatedAt", "CreatedBy", "ExpertiesId", "ExpertiesName", "IsActive", "ModifiedAt", "ModifiedBy", "RUId", "SMEGId" },
                values: new object[,]
                {
                    { 1L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 1L, "NDO", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 1L, 1L },
                    { 2L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 2L, "Recovery & Reorganisation", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 2L, 2L },
                    { 3L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 3L, "Transaction Tax", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 3L, 3L },
                    { 10L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 4L, "Due Diligence", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 10L, 4L },
                    { 11L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 5L, "Governance Risk & Operations (GRO)", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 11L, 5L },
                    { 16L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 6L, "NErcO", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 16L, 6L },
                    { 17L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 7L, "Cyber & IT Risk (Cyber)", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 17L, 7L },
                    { 18L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 8L, "FS Risk", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 18L, 8L },
                    { 19L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 9L, "Forensic", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 19L, 9L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bu_Experties_Grps_BusinessUnitId",
                table: "Bu_Experties_Grps",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Bu_Experties_Grps_ExpertiesId",
                table: "Bu_Experties_Grps",
                column: "ExpertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bu_Experties_Grps_RUId",
                table: "Bu_Experties_Grps",
                column: "RUId");

            migrationBuilder.CreateIndex(
                name: "IX_Bu_Experties_Grps_SMEGId",
                table: "Bu_Experties_Grps",
                column: "SMEGId");
        }
    }
}
