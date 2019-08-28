using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;

namespace OK.Hookman.Service.Mappings.Resolvers
{
    public class DictionaryToJsonResolver : IMemberValueResolver<object, object, IDictionary<string, string>, string>
    {
        public string Resolve(object source, object destination, IDictionary<string, string> sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember == null || !sourceMember.Any())
            {
                return null;
            }

            return JsonConvert.SerializeObject(sourceMember);
        }
    }
}