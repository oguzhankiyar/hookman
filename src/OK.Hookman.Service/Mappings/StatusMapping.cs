using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Responses.Status;
using OK.Hookman.Persistence.Core.Entities;

namespace OK.Hookman.Service.Mappings
{
    public class StatusMapping : Profile
    {
        public StatusMapping()
        {
            CreateMap<StatusEntity, StatusModel>();

            CreateMap<List<StatusEntity>, StatusListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}