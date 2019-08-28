using System;

namespace OK.Hookman.Core.Models
{
    public class SenderModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}