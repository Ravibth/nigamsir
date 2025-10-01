using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedvw_projectroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlQuery = @"
                        CREATE OR REPLACE VIEW public.vw_projectroles
                         AS
                         SELECT row_number() OVER () AS ""Id"",
                            ""ProjectId"",
                            ""User"",
                            ""UserName"",
                            ""Role"",
                            ""ApplicationRole"",
                            ""Description"",
                            ""IsActive"",
                            ""ParentEmail"",
                            ""ParentName"",
                            ""CreatedAt"",
                            ""ModifiedAt"",
                            ""CreatedBy"",
                            ""ModifiedBy""
                           FROM ( SELECT ""ProjectRoles"".""ProjectId"",
                                    ""ProjectRoles"".""User"",
                                    ""ProjectRoles"".""UserName"",
                                    ""ProjectRoles"".""Role"",
                                    ""ProjectRoles"".""ApplicationRole"",
                                    ""ProjectRoles"".""Description"",
                                    ""ProjectRoles"".""IsActive"",
                                    ""ProjectRoles"".""DelegateEmail"" AS ""ParentEmail"",
                                    ""ProjectRoles"".""DelegateUserName"" AS ""ParentName"",
                                    ""ProjectRoles"".""CreatedAt"",
                                    ""ProjectRoles"".""ModifiedAt"",
                                    ""ProjectRoles"".""CreatedBy"",
                                    ""ProjectRoles"".""ModifiedBy""
                                   FROM ""ProjectRoles""
                                UNION
                                 SELECT ""ProjectRoles"".""ProjectId"",
                                    ""ProjectRoles"".""DelegateEmail"" AS ""User"",
                                    ""ProjectRoles"".""DelegateUserName"" AS ""UserName"",
                                        CASE
                                            WHEN ""ProjectRoles"".""ApplicationRole"" = 'SuperCoach'::text THEN 'SuperCoach'::text
                                            ELSE 'AdditionalDelegate'::text
                                        END AS ""Role"",
                                        CASE
                                            WHEN ""ProjectRoles"".""ApplicationRole"" = 'SuperCoach'::text THEN 'SuperCoach'::text
                                            ELSE 'AdditionalDelegate'::text
                                        END AS ""ApplicationRole"",
                                    ""ProjectRoles"".""Description"",
                                    ""ProjectRoles"".""IsActive"",
                                    ""ProjectRoles"".""User"" AS ""ParentEmail"",
                                    ""ProjectRoles"".""UserName"" AS ""ParentName"",
                                    ""ProjectRoles"".""CreatedAt"",
                                    ""ProjectRoles"".""ModifiedAt"",
                                    ""ProjectRoles"".""CreatedBy"",
                                    ""ProjectRoles"".""ModifiedBy""
                                   FROM ""ProjectRoles"") t
                          WHERE ""User"" <> ''::text AND ""User"" IS NOT NULL;";

            migrationBuilder.Sql(sqlQuery);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW public.vw_projectroles;");

        }
    }
}
