using MediatR;
using RMT.MarketPlace.Application.HttpServices.DTOs;
using RMT.MarketPlace.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.IHttpServices
{
    public interface IAllocationServiceHttpApi
    {
        Task<List<ResourceAllocationResponse>> GetAllocationsByEmail(string empEmailId);
        Task<List<GetAllRequisitionByProjectCodeResponse>> GetAllRequisitionByProjectCode(string? PipelineCode, string? JobCode, int? limit, int? pagination, string? currentEmp, bool? ScoreCalculationForRequisitionIdsAllowed, bool? IsRequsitionFilterByProjectRoles);
    }
}
