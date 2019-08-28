using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Hook;
using OK.Hookman.Core.Responses.Hook;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Service.Mappings.Resolvers;

namespace OK.Hookman.Service.Mappings
{
    public class HookMapping : Profile
    {
        public HookMapping()
        {
            CreateMap<HookEntity, HookModel>()
                .ForMember(dest => dest.RequestHeaders,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.RequestHeaders))
                .ForMember(dest => dest.ResponseHeaders,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.ResponseHeaders));
            CreateMap<HookCreateRequest, HookEntity>();

            CreateMap<List<HookEntity>, HookListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<HookEntity, HookDetailResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<HookEntity, HookCreateResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}