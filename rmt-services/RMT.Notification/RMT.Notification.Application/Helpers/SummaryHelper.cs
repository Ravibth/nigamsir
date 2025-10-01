using Newtonsoft.Json.Linq;
using RMT.Notification.Application.HttpServices.DTO;
using RMT.Notification.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.Helpers
{
    public static class SummaryHelper
    {


        public static string GenerateAllocationEmailBody(List<PublishedAllocationResponse> allocations, string title)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (allocations.Count > 0)
            {
                tableContent.Append("<br />  <h2>").Append(title).Append("</h2> <br />");
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Pipeline Code</th>" +
                       "<th>Pipeline Name</th>" +
                      "<th>Job Code </th>" +
                      "<th>Job Name </th>" +
                    "<th>Employee Name</th>" +
                    "<th>Designation</th>" +
                    "<th>Grade</th>" +
                    "<th>BU</th>" +
                    "<th>Offering</th>" +
                    "<th>Solution </th> " +
                     "<th>Created By </th>" +
                     "<th>Created On </th>");
                if (title == UPDATE_ALLOCATION)
                {
                    tableContent.Append("" +
                    "<th>Updated By </th>" +
                    "<th>Updated On </th>");
                }

                else if (title == RELEASE_ALLOCATION)
                {
                    tableContent.Append(
                    "<th>Release By </th>" +
                    "<th>Release On </th>"
                );
                }
                tableContent.Append(
                    " </tr>");
                foreach (var item in allocations)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.Requisition.PipelineCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.JobCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.JobName).Append("</td>");
                    tableContent.Append("<td>").Append(item.EmpName).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.Designation).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.Grade).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.BusinessUnit).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.Offerings).Append("</td>");
                    tableContent.Append("<td>").Append(item.Requisition.Solutions).Append("</td>");

                    tableContent.Append("<td>").Append(item.CreatedBy).Append("</td>");
                    tableContent.Append("<td>").Append(item.CreatedAt).Append("</td>");

                    if (title == UPDATE_ALLOCATION || title == RELEASE_ALLOCATION)
                    {
                        tableContent.Append("<td>").Append(item.ModifiedBy).Append("</td>");
                        tableContent.Append("<td>").Append(item.ModifiedAt).Append("</td>");
                    }
                    // tableContent.Append("<td>").Append(item.Description).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");

                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }

        public static string GenerateAllocationEmailBody(List<ResourceAllocationDetailsResponseForWorkflowMeta> allocations, string title)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (allocations.Count > 0)
            {
                tableContent.Append("<br />  <h2>").Append(title).Append("</h2> <br />");
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Pipeline Code</th>" +
                       "<th>Pipeline Name</th>" +
                      "<th>Job Code </th>" +
                      "<th>Job Name </th>" +
                    "<th>Employee Name</th>" +
                    "<th>Designation</th>" +
                    "<th>Grade</th>" +
                    "<th>BU</th>" +
                    "<th>Offering</th>" +
                    "<th>Solution </th> " +
                     "<th>Created By </th>" +
                     "<th>Created On </th>" +
                     "<th>Updated By </th>");
                
                tableContent.Append(
                    " </tr>");
                foreach (var item in allocations)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.PipelineCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobName).Append("</td>");
                    tableContent.Append("<td>").Append(item.EmpName).Append("</td>");
                    tableContent.Append("<td>").Append(item.Designation).Append("</td>");
                    tableContent.Append("<td>").Append(item.Grade).Append("</td>");
                    tableContent.Append("<td>").Append(JObject.Parse(item.Requisition.ToString())["BusinessUnit"]).Append("</td>");
                    tableContent.Append("<td>").Append(JObject.Parse(item.Requisition.ToString())["Offerings"]).Append("</td>");
                    tableContent.Append("<td>").Append(JObject.Parse(item.Requisition.ToString())["Solutions"]).Append("</td>");

                    tableContent.Append("<td>").Append(item.CreatedBy).Append("</td>");
                    tableContent.Append("<td>").Append(item.CreatedAt).Append("</td>");
                    tableContent.Append("<td>").Append(item.ModifiedBy).Append("</td>");

                    // tableContent.Append("<td>").Append(item.Description).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");

                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }

        public static string GenerateRequistionEmailBody(List<RequisitionResponse> requisitions, string title)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (requisitions.Count > 0)
            {
                tableContent.Append("<br />  <h2>").Append(title).Append("</h2> <br />");
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Pipeline Code</th>" +
                      "<th>Pipeline Name</th>" +
                      "<th>Job Code </th>" +
                      "<th>Job Name </th>" +
                    "<th>Requistion ID</th>" +
                    "<th>Designation</th>" +
                    "<th>Grade</th>" +
                    "<th>BU</th>" +
                    "<th>Offering</th>" +
                    "<th>Solution</th>" +
                    "<th>Created By </th>" +
                    "<th>Created On </th>");

                if (title == UPDATE_REQUISTION)
                {
                    tableContent.Append(
                    "<th>Updated By </th>" +
                    "<th>Updated On </th>");
                }
                else if (title == RELEASE_REQUISTION)
                {
                    tableContent.Append(
                     "<th>Deleted By  </th>" +
                    "<th>Deleted  On </th>"
                );
                }
                tableContent.Append(
                " </tr>");
                foreach (var item in requisitions)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.PipelineCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobName).Append("</td>");
                    tableContent.Append("<td>").Append(item.Id).Append("</td>");
                    tableContent.Append("<td>").Append(item.Designation).Append("</td>");
                    tableContent.Append("<td>").Append(item.Grade).Append("</td>");
                    tableContent.Append("<td>").Append(item.BusinessUnit).Append("</td>");
                    tableContent.Append("<td>").Append(item.Offerings).Append("</td>");
                    tableContent.Append("<td>").Append(item.Solutions).Append("</td>");

                    tableContent.Append("<td>").Append(item.CreatedBy).Append("</td>");
                    tableContent.Append("<td>").Append(item.CreatedAt).Append("</td>");

                    if (title == RELEASE_REQUISTION || title == UPDATE_REQUISTION)
                    {
                        tableContent.Append("<td>").Append(item.ModifiedBy).Append("</td>");
                        tableContent.Append("<td>").Append(item.ModifiedAt).Append("</td>");
                    }

                    //tableContent.Append("<td>").Append(item.Description).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");



                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }
        public static string GenerateMarketPlaceEmailBody(List<MarketPlaceSummaryDTO> marketplace)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (marketplace.Count > 0)
            {
                tableContent.Append("<br />  <h2>").Append("Marketplace Interests").Append("</h2> <br />");
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Pipeline Code</th>" +
                      "<th>Pipeline Name</th>" +
                      "<th>Job Code </th>" +
                      "<th>Job Name </th>" +
                    "<th>Number of Interest Received</th>" +
                    "<th>Date of Publish</th>" +
                    "<th>Date of Expiry</th>" +
                    " </tr>");
                foreach (var item in marketplace)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(item.PipelineCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobCode).Append("</td>");
                    tableContent.Append("<td>").Append(item.JobName).Append("</td>");
                    tableContent.Append("<td>").Append(item.NoOfInterest).Append("</td>");
                    tableContent.Append("<td>").Append(item.PublishDate).Append("</td>");
                    tableContent.Append("<td>").Append(item.ExitDate).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");

                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }
    }
}
