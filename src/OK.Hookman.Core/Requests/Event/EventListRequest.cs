namespace OK.Hookman.Core.Requests.Event
{
    public class EventListRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}