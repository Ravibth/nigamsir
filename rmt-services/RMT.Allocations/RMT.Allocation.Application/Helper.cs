using Azure.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static RMT.Allocation.Domain.ConstantsDomain;
using static RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application
{
    public class Helper
    {
        public static string UrlBuilderByQuery(string baseUrl, Dictionary<string, dynamic> queries)
        {
            var urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            //query.
            foreach (var item in queries)
            {
                if (item.Value is string)
                {
                    query.Add(item.Key, item.Value);
                }
                else if (item.Value is List<string>)
                {
                    foreach (var item1 in item.Value)
                    {
                        query.Add(item.Key, item1);
                    }
                }
                else if (item.Value is DateTime)
                {
                    query.Add(item.Key, item.Value);
                }
                else if (item.Value is List<DateTime>)
                {
                    foreach (var item1 in item.Value)
                    {
                        query.Add(item.Key, item1);
                    }
                }
                else if (item.Value is null)
                {
                    query.Add(item.Key, "null");
                }
                else
                {
                    query.Add(item.Key, item.Value);
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }
        /// <summary>
        /// CHECK IF USER IS RESOURCE_REQUESTOR
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static bool IsResourceRequestor(List<string> userRoles)
        {
            List<string> ResourceRequestorRolesList = new List<string>()
            {
                UserRolesConstant.ResourceRequestor,
                UserRolesConstant.JobManager,
                UserRolesConstant.EO,
                UserRolesConstant.EngagementLeader,
                UserRolesConstant.ProposedEL
            };
            bool isRR = ResourceRequestorRolesList.Where((ur) => userRoles.Any(r => r != null && r.Equals(ur, StringComparison.OrdinalIgnoreCase))).Any();
            return isRR;
        }


        public static bool IsSystemAdmin(List<string> userRoles)
        {
            List<string> systemAdminRolesList = new List<string>()
            {
                UserRolesConstant.SystemAdmin
            };
            bool isSystemAdmin = systemAdminRolesList.Where((ur) => userRoles.Any(r => r != null && r.Equals(ur, StringComparison.OrdinalIgnoreCase))).Any();
            return isSystemAdmin;
        }


        /// <summary>
        /// CHECK IF USER IS REVIEWER
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static bool IsReviewer(List<string> userRoles)
        {
            List<string> ReviewerRolesList = new List<string>()
            {
                UserRolesConstant.CSP,
                UserRolesConstant.ProposedCSP,
                UserRolesConstant.SMEGLeader
            };
            bool isReviewer = ReviewerRolesList.Where((ur) => userRoles.Any(r => r != null && r.Equals(ur, StringComparison.OrdinalIgnoreCase))).Any();
            return isReviewer;
        }
        /// <summary>
        /// CHECK IS USER IS DELGATE
        /// </summary>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static bool IsDelegate(List<string> userRoles)
        {
            List<string> DelegateRoleList = new List<string>()
            {
                UserRolesConstant.Delegate
            };
            bool isDelegate = DelegateRoleList.Where((ur) => userRoles.Any(r => r != null && r.Equals(ur, StringComparison.OrdinalIgnoreCase))).Any();
            return isDelegate;
        }
        public static bool IsAdditionalElOrAdditionalDelegate(List<ProjectRoles> projectRoles)
        {
            List<string> AdditionalElRoleList = new List<string>()
            {
                UserRolesConstant.AdditionalEl
            };
            bool isAdditionalELOrAdditionalDelegate = AdditionalElRoleList
                                        .Where((ur) => projectRoles.Any(pr => pr.Role.Equals(ur, StringComparison.OrdinalIgnoreCase)))
                                        .Any();
            return isAdditionalELOrAdditionalDelegate;
        }
        public static bool IsEmployeeOnly(List<string> userRoles)
        {
            if (userRoles.Count == 1 && userRoles.Any(e => e.Equals(UserRolesConstant.Employee, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            return false;
        }
        public static async Task<List<GetAllRequisitionByProjectCodeResponse>> GetRequsitionListByCurrentUserRole(List<GetAllRequisitionByProjectCodeResponse> AllRequsitions, string PipelineCode, string? JobCode, string userEmail, IProjectServiceHttpApi projectServiceHttpApi, IConfigurationHttpService configurationHttpService, UserDecorator? UserInfo)
        {
            ProjectDTO projectDTO = await projectServiceHttpApi.GetProjectDetailsByCode(PipelineCode, JobCode);
            var currentUserRoles = projectDTO.ProjectRoles.Where(d => !string.IsNullOrEmpty(d.User) && d.User.Equals(userEmail, StringComparison.OrdinalIgnoreCase) ||
                                                    !string.IsNullOrEmpty(d.DelegateEmail) && d.DelegateEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase)
                                                    ).ToList();
            List<string> userRoles = currentUserRoles.Select(e => e.Role).ToList();

            if (Helper.IsResourceRequestor(userRoles) || Helper.IsSystemAdmin(UserInfo?.roles?.ToList()))
            {
                foreach (var req in AllRequsitions)
                {
                    req.hasPermissionToDelete = true;
                    req.hasPermissionToEdit = true;
                }
                return AllRequsitions;
            }
            else if (Helper.IsDelegate(userRoles))
            {
                foreach (var req in AllRequsitions)
                {
                    req.hasPermissionToDelete = true;
                    req.hasPermissionToEdit = true;
                }
                return AllRequsitions;
            }
            else if (Helper.IsReviewer(userRoles))
            {
                foreach (var req in AllRequsitions)
                {
                    req.hasPermissionToDelete = false;
                    req.hasPermissionToEdit = false;
                }
                return AllRequsitions;
            }
            else if (Helper.IsAdditionalElOrAdditionalDelegate(currentUserRoles))
            {
                //AS DELEGATE
                var user = currentUserRoles
                                .Where((cur) => cur.Role.Equals(UserRolesConstant.AdditionalEl, StringComparison.OrdinalIgnoreCase))
                                .FirstOrDefault();
                string AdditionalElEmail = user.User == null ? string.Empty : user.User;
                string AdditionalDelegateEmail = user.DelegateEmail == null ? string.Empty : user.DelegateEmail;
                //TODO: GET CONFIG FOR ADDITIONAL EL
                //TODO: GET CONFIG FOR ADDITIONAL DELEGATE
                var configDataForAddEl = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_EL", "GLOBAL");
                if (configDataForAddEl.Count == 0)
                {
                    throw new Exception($"Configuration Group Permission_for_Additional_EL not found ");
                }
                var configDataForAddDel = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_Delegate", "GLOBAL");
                foreach (var req in AllRequsitions)
                {
                    if (req.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) || req.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase))
                    {
                        req.hasPermissionToDelete = true;
                        req.hasPermissionToEdit = true;
                    }
                    else
                    {
                        req.hasPermissionToDelete = false;
                        req.hasPermissionToEdit = false;
                    }
                }
                if (AdditionalElEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int64.Parse(configDataForAddEl.FirstOrDefault().AllValue) > 0)
                {
                    //current user is AdditionalEL and had configuration to view all records

                    return AllRequsitions;
                }
                if (AdditionalDelegateEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int64.Parse(configDataForAddDel.FirstOrDefault().AllValue) > 0)
                {
                    return AllRequsitions;
                }
                return AllRequsitions.Where((req) => req.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) ||
                                                            req.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase))
                                             .ToList();
            }

            if (UserInfo != null && UserInfo.roles != null && UserInfo.roles.Any(m => m.ToLower().Trim() == UserRoles.CEOCOO.ToLower().Trim() || m.ToLower().Trim() == UserRoles.SystemAdmin.ToLower().Trim() || m.ToLower().Trim() == UserRoles.Admin.ToLower().Trim()))
            {
                return AllRequsitions;
            }
            return new List<GetAllRequisitionByProjectCodeResponse>();
        }
        public static async Task<List<GetResourceAllocationDetailsListByCurrentUserRoleResponse>> GetResourceAllocationDetailsListByCurrentUserRole(List<ResourceAllocationDetailsResponse> ResourceAllocationDetails, string PipelineCode, string JobCode, string userEmail, IProjectServiceHttpApi projectServiceHttpApi, IConfigurationHttpService configurationHttpService, IIdentityUserDetailsHttpApi identityUserDetailsHttpApi, string[]? userAppRoles)
        {
            bool isAdmin = false;
            isAdmin = userAppRoles != null ? userAppRoles.Contains(UserRolesConstant.Admin) : false;

            bool isLeader = false;
            isLeader = userAppRoles != null ? userAppRoles.Contains(UserRolesConstant.Leaders) : false;

            bool isCeoCoo = false;
            isCeoCoo = userAppRoles != null ? userAppRoles.Contains(UserRolesConstant.CEOCOO) : false;

            ProjectDTO projectDTO = await projectServiceHttpApi.GetProjectDetailsByCode(PipelineCode, JobCode);
            var currentUserRoles = projectDTO.ProjectRoles.Where(d => !string.IsNullOrEmpty(d.User) && d.User.Equals(userEmail, StringComparison.OrdinalIgnoreCase) ||
                                                    !string.IsNullOrEmpty(d.DelegateEmail) && d.DelegateEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase)).ToList();

            List<string> userRoles = currentUserRoles.Select(e => e.Role).ToList();

            List<GetResourceAllocationDetailsListByCurrentUserRoleResponse> _response = ResourceAllocationDetails
                .Select(resourceAllocation => GetResourceAllocationDetailsListByCurrentUserRoleResponse.MapToGetResourceAllocationDetailsList(resourceAllocation))
                .ToList();

            List<GetResourceAllocationDetailsListByCurrentUserRoleResponse> _supercoachAllocations = new();
            //userRoles contains SuperCoach
            //userRole allocations for SC
            bool containsSuperCoachRole = userRoles.Any(e => e.Equals(UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase));
            if (containsSuperCoachRole)
            {
                var users_list = await identityUserDetailsHttpApi.GetSupercoachUserListByAllocationSupercoachDelegate(userEmail);
                List<string> userEmails = new();
                userEmails.Add(userEmail);
                if (users_list != null && users_list.Count > 0)
                {
                    foreach (var item in users_list)
                    {
                        userEmails.Add(item.email_id);
                    }
                }
                //my-supercoach-list
                var supercoachUsersList = await identityUserDetailsHttpApi.GetUsersBySuperCoachEmailDataHttpApiQuery(userEmails.Distinct().ToList());
                _supercoachAllocations = _response.Where(e => supercoachUsersList.Any(x => x.email_id.Equals(e.EmpEmail, StringComparison.OrdinalIgnoreCase))).ToList();
            }



            if (Helper.IsResourceRequestor(userRoles) || Helper.IsSystemAdmin(userAppRoles?.ToList()))
            {
                foreach (var rad in _response)
                {
                    rad.hasPermissionToReleaseAllocation = true;
                    rad.hasPermisssionToUpdateAllocation = true;
                }
                return _response;
            }
            else if (Helper.IsDelegate(userRoles))
            {
                foreach (var rad in _response)
                {
                    rad.hasPermissionToReleaseAllocation = true;
                    rad.hasPermisssionToUpdateAllocation = true;
                }
                return _response;
            }
            else if (Helper.IsAdditionalElOrAdditionalDelegate(currentUserRoles))
            {
                //AS DELEGATE
                ProjectRoles? user = currentUserRoles
                                .Where((cur) => cur.Role != null && cur.Role.Equals(UserRolesConstant.AdditionalEl, StringComparison.OrdinalIgnoreCase))
                                .FirstOrDefault();
                string AdditionalElEmail = string.Empty;
                string AdditionalDelegateEmail = string.Empty;

                if (user != null)
                {
                    if (user.User != null)
                    {
                        AdditionalElEmail = user?.User;
                    }
                    if (user.DelegateEmail != null)
                    {
                        AdditionalDelegateEmail = user?.DelegateEmail;
                    }
                }
                //TODO: GET CONFIG FOR ADDITIONAL EL
                //TODO: GET CONFIG FOR ADDITIONAL DELEGATE
                var configDataForAddEl = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_EL", "GLOBAL");
                if (configDataForAddEl.Count == 0)
                {
                    throw new Exception($"Configuration Group Permission_for_Additional_EL not found ");
                }
                var configDataForAddDel = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_Delegate", "GLOBAL");
                if (configDataForAddDel.Count == 0)
                {
                    throw new Exception($"Configuration Group Permission_for_Additional_Delegate not found ");
                }
                foreach (var rad in _response)
                {
                    if (rad.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) || rad.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase))
                    {
                        rad.hasPermissionToReleaseAllocation = true;
                        rad.hasPermisssionToUpdateAllocation = true;
                    }
                    else
                    {
                        rad.hasPermissionToReleaseAllocation = false;
                        rad.hasPermisssionToUpdateAllocation = false;
                    }
                }
                if (AdditionalElEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int64.Parse(configDataForAddEl.FirstOrDefault().AllValue) > 0)
                {
                    //current user is AdditionalEL and had configuration to view all records

                    return _response;
                }
                if (AdditionalDelegateEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int64.Parse(configDataForAddDel.FirstOrDefault().AllValue) > 0)
                {
                    return _response;
                }
                if (isAdmin || isLeader || isCeoCoo)
                {
                    return _response;
                }
                else
                {
                    var delegateAllocations = _response.Where((req) => req.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) ||
                                                                req.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase) ||
                                                                req.EmpEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                                                 .ToList();
                    if (containsSuperCoachRole)
                    {
                        delegateAllocations = delegateAllocations.Concat(_supercoachAllocations).DistinctBy(x => x.Id).ToList();
                    }
                    return delegateAllocations;
                }
            }
            else if (containsSuperCoachRole)
            {
                return _supercoachAllocations;
            }
            else if (Helper.IsEmployeeOnly(userRoles))
            {
                List<GetResourceAllocationDetailsListByCurrentUserRoleResponse> rads;
                if (isAdmin || isLeader || isCeoCoo)
                {
                    rads = _response;
                }
                else
                {
                    rads = _response.Where(e => e.EmpEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase) &&
                                               e.AllocationStatus.ToLower().Trim() != AllocationStatuses.DRAFT.ToLower().Trim() &&
                                               e.ParentPublishedResAllocDetailsId == null)
                                   .ToList();
                }

                foreach (var rad in rads)
                {
                    rad.hasPermissionToReleaseAllocation = false;
                    rad.hasPermisssionToUpdateAllocation = false;
                }
                return rads;
            }
            else
            {
                foreach (var rad in _response)
                {
                    rad.hasPermissionToReleaseAllocation = false;
                    rad.hasPermisssionToUpdateAllocation = false;
                }
                return _response;
            }
        }

        public static async Task<GetHolidayLeaveResignedAbsconded> GetHolidayLeaveResignedAbscondedByEmailIds(IConfiguration configurationObj, List<string> emailIds, HttpClient httpClientObj, IWCGTMasterHttpApi wCGTMasterHttpApiObj, DateTime start_date, DateTime end_date)
        {

            string baseurl = configurationObj.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getEmployeeLeaves = configurationObj.GetSection("MicroserviceApiSettings").GetSection("GetLeavesInfo").Value;

            //string getsLeaves = configurationObj.GetSection("MicroserviceApiSettings").GetSection("GetEmployeeLeavePath").Value;

            // 1608 -- optimization -- call directly as a parameter argument
            //var employeeLeaveDto = new LeaveParamsDTO()
            //{
            //    emp_emailid = emailIds.ToList(),
            //};
            var employeeLeaveDto = new LeaveParamsDTO()
            {
                emp_mid = emailIds.Select(e => e.Contains("__") ? e.Split("__")[0] : e).ToList(),
                start_date = start_date,
                end_date = end_date,
            };

            var content = new StringContent(JsonConvert.SerializeObject(employeeLeaveDto), Encoding.UTF8, "application/json");

            // 1608 -- String concatenation is not correct, use String.append or format
            // 1608 Wrong implementation, both calls must be made from a single class of WCGT
            Task<HttpResponseMessage> leavesResponseTask = httpClientObj.PostAsync(baseurl + getEmployeeLeaves, content);

            //1608 -- Do null check above at start of the function
            Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> resignedAbscondedResponse =
                wCGTMasterHttpApiObj.GetResignedAndAbscondedUsersByEmails(emailIds.ToList() != null ? emailIds.ToList() : new List<string>());

            await Task.WhenAll(leavesResponseTask, resignedAbscondedResponse);

            if (!leavesResponseTask.Result.IsSuccessStatusCode)
            {
                string response = await leavesResponseTask.Result.Content.ReadAsStringAsync();
                throw new Exception("Unable to fetch details" + response);
            }

            var result = new GetHolidayLeaveResignedAbsconded
            {
                ResignedAbscondedResponse = resignedAbscondedResponse.Result
            };
            if (leavesResponseTask.Result.IsSuccessStatusCode)
            {
                List<GTLeaveBaseDTO> leaveList = await leavesResponseTask.Result.Content.ReadFromJsonAsync<List<GTLeaveBaseDTO>>();
                result.LeavesResponseTask = leaveList;
            }
            return result;
        }

        /// <summary>
        /// NEED TO CHECK
        /// </summary>
        /// <param name="holidayType"></param>
        /// <returns></returns>
        public static Boolean IsHolidayOrLeave(string holidayType)
        {
            List<string> myList = new List<string> { Infrastructure.Constants.TimelineDisplayText.FULL_DAY_LEAVE.ToLower(), Infrastructure.Constants.TimelineDisplayText.HOLIDAY.ToLower() };
            int index = myList.IndexOf(holidayType);
            return index != -1 ? true : false;
        }
    }
}
