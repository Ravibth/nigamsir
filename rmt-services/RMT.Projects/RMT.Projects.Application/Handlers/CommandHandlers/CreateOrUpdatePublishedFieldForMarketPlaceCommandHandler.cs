using MediatR;
using Newtonsoft.Json.Linq;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class CreateOrUpdatePublishedFieldForMarketPlaceCommand : IRequest<List<PublishedFieldForMarketPlace>>
    {
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }

        public List<string> FieldNameList { get; set; }
        public List<bool> IsActiveList { get; set; }
    }

    public class CreateOrUpdatePublishedFieldForMarketPlaceCommandHandler : IRequestHandler<CreateOrUpdatePublishedFieldForMarketPlaceCommand, List<PublishedFieldForMarketPlace>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public CreateOrUpdatePublishedFieldForMarketPlaceCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<PublishedFieldForMarketPlace>> Handle(CreateOrUpdatePublishedFieldForMarketPlaceCommand request, CancellationToken cancellationToken)
        {
            List<PublishedFieldForMarketPlace> publishedFieldMarketPlaceList = new List<PublishedFieldForMarketPlace>();
            for (int i = 0; i < request.FieldNameList.Count; i++)
            {
                var _publishedFieldMarketPlace = await _ProjectRepo.CreateOrUpdatePublishedFieldForMarketPlace(request.PipelineCode, request.JobCode, request.FieldNameList[i], request.IsActiveList[i]);
                publishedFieldMarketPlaceList.Add(_publishedFieldMarketPlace);
            }
            //var _publishedFieldMarketPlace = await _ProjectRepo.CreateOrUpdatePublishedFieldForMarketPlace(request.ProjectCode, request.FieldName, request.IsActive);

            return publishedFieldMarketPlaceList;
        }
    }

}

