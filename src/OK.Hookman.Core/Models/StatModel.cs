using System.Collections.Generic;

namespace OK.Hookman.Core.Models
{
    public class StatTopActionModel
    {
        public string Action { get; set; }
        public List<StatTopActionDateValueModel> Values { get; set; }
    }

    public class StatTopActionDateValueModel
    {
        public string Date { get; set; }
        public string Value { get; set; }
    }
}