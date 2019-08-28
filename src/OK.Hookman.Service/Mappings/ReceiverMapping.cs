using System.Collections.Generic;
using AutoMapper;
using OK.Hookman.Core.Models;
using OK.Hookman.Core.Requests.Receiver;
using OK.Hookman.Core.Responses.Receiver;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Service.Mappings.Resolvers;

namespace OK.Hookman.Service.Mappings
{
    public class ReceiverMapping : Profile
    {
        public ReceiverMapping()
        {
            CreateMap<ReceiverEntity, ReceiverModel>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<JsonToDictionaryResolver, string>(src => src.QueryStrings));
            CreateMap<ReceiverCreateRequest, ReceiverEntity>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.QueryStrings));
            CreateMap<ReceiverEditRequest, ReceiverEntity>()
                .ForMember(dest => dest.Headers,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.Headers))
                .ForMember(dest => dest.QueryStrings,
                           opt => opt.MapFrom<DictionaryToJsonResolver, IDictionary<string, string>>(src => src.QueryStrings));

            CreateMap<List<ReceiverEntity>, ReceiverListResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ReceiverEntity, ReceiverDetailResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ReceiverEntity, ReceiverCreateResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<ReceiverEntity, ReceiverEditResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}