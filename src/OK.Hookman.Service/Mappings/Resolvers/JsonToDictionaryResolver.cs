using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;

namespace OK.Hookman.Service.Mappings.Resolvers
{
    public class JsonToDictionaryResolver : IMemberValueResolver<object, object, string, IDictionary<string, string>>
    {
        public IDictionary<string, string> Resolve(object source, object destination, string sourceMember, IDictionary<string, string> destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(sourceMember))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IDictionary<string, string>>(sourceMember);
        }
    }
}