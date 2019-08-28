using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Sender;
using OK.Hookman.Core.Responses.Sender;
using OK.Hookman.Persistence.Core.Entities;

namespace OK.Hookman.Service.Mappings
{
    public class SenderMapping : Profile
    {
        public SenderMapping()
        {
            CreateMap<SenderEntity, SenderModel>();
            CreateMap<SenderCreateRequest, SenderEntity>();
            CreateMap<SenderEditRequest, SenderEntity>();

            CreateMap<List<SenderEntity>, SenderListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<SenderEntity, SenderDetailResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<SenderEntity, SenderCreateResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<SenderEntity, SenderEditResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}