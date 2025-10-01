using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class vw_projectroles_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //DROP VIEW public.vw_projectroles;
            string sqlQuery = @"
                        CREATE OR REPLACE VIEW public.vw_projectroles
                         AS
                         SELECT row_number() OVER () AS ""Id"",
                            t.""ProjectId"",
                            t.""User"",
                            t.""UserName"",
                            t.""Role"",
                            t.""ApplicationRole"",
                            t.""Description"",
                            t.""IsActive"",
                            t.""ParentEmail"",
                            t.""ParentName"",
                            t.""CreatedAt"",
                            t.""ModifiedAt"",
                            t.""CreatedBy"",
                            t.""ModifiedBy""
                           FROM ( SELECT ""ProjectRoles"".""ProjectId"",
                                    ""ProjectRoles"".""User"",
                                    ""ProjectRoles"".""UserName"",
                                    ""ProjectRoles"".""Role"",
                                    ""ProjectRoles"".""ApplicationRole"",
                                    ""ProjectRoles"".""Description"",
                                    ""ProjectRoles"".""IsActive"",
                                    ""ProjectRoles"".""DelegateEmail"" AS ""ParentEmail"",
                                    ""ProjectRoles"".""DelegateUserName"" AS ""ParentName"",		 
                                    ""ProjectRoles"".""CreatedAt"" AS ""CreatedAt"",
                                    ""ProjectRoles"".""ModifiedAt"" AS ""ModifiedAt"",
                                    ""ProjectRoles"".""CreatedBy"" AS ""CreatedBy"",
                                    ""ProjectRoles"".""ModifiedBy"" AS ""ModifiedBy""
                                   FROM ""ProjectRoles""
                                UNION
                                 SELECT ""ProjectRoles"".""ProjectId"",
                                    ""ProjectRoles"".""DelegateEmail"" AS ""User"",
                                    ""ProjectRoles"".""DelegateUserName"" AS ""UserName"",
                                    'AdditionalDelegate'::text AS ""Role"",
                                    'AdditionalDelegate'::text AS ""ApplicationRole"",
                                    ""ProjectRoles"".""Description"",
                                    ""ProjectRoles"".""IsActive"",
                                    ""ProjectRoles"".""User"" AS ""ParentEmail"",
                                    ""ProjectRoles"".""UserName"" AS ""ParentName"",
                                    ""ProjectRoles"".""CreatedAt"" AS ""CreatedAt"",
                                    ""ProjectRoles"".""ModifiedAt"" AS ""ModifiedAt"",
                                    ""ProjectRoles"".""CreatedBy"" AS ""CreatedBy"",
                                    ""ProjectRoles"".""ModifiedBy"" AS ""ModifiedBy""
                                   FROM ""ProjectRoles"") t
                          WHERE t.""User"" <> ''::text AND t.""User"" IS NOT NULL;";

            migrationBuilder.Sql(sqlQuery);

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW public.vw_projectroles;");

        }
    }
}
