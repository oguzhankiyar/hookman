namespace OK.Hookman.Core.Requests.Action
{
    public class ActionListRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}