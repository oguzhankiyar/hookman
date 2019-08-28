using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Event;
using OK.Hookman.Core.Responses.Event;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Service.Mappings.Resolvers;

namespace OK.Hookman.Service.Mappings
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<EventEntity, EventModel>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.QueryStrings));
            CreateMap<EventCreateRequest, EventEntity>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.QueryStrings));
            CreateMap<EventEditRequest, EventEntity>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.QueryStrings));

            CreateMap<List<EventEntity>, EventListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<EventEntity, EventDetailResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<EventEntity, EventCreateResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<EventEntity, EventEditResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}