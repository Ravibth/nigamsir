using MediatR;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetDistinctFilterParameterQuery:IRequest<string>
    {
    }
    public class GetDistinctFilterParameterQueryHandlers : IRequestHandler<GetDistinctFilterParameterQuery, string>
    {
        private readonly IProjectRepository _projectRepository;
        public GetDistinctFilterParameterQueryHandlers(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public Task<string> Handle(GetDistinctFilterParameterQuery request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
