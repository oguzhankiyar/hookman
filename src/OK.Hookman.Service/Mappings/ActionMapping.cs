using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Action;
using OK.Hookman.Core.Responses.Action;
using OK.Hookman.Persistence.Core.Entities;

namespace OK.Hookman.Service.Mappings
{
    public class ActionMapping : Profile
    {
        public ActionMapping()
        {
            CreateMap<ActionEntity, ActionModel>();
            CreateMap<ActionCreateRequest, ActionEntity>();
            CreateMap<ActionEditRequest, ActionEntity>();

            CreateMap<List<ActionEntity>, ActionListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ActionEntity, ActionDetailResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ActionEntity, ActionCreateResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ActionEntity, ActionEditResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}