namespace OK.Hookman.Core.Base
{
    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public T Data { get; set; }

        public BaseResponse()
        {
            Status = true;
            Message = "Success";
            Errors = new string[] { };
            Data = default(T);
        }

        public static BaseResponse<U> Success<U>(string message = "Success", U data = default(U)) where U : class
        {
            return new BaseResponse<U>()
            {
                Status = true,
                Message = message,
                Errors = new string[] { },
                Data = data
            };
        }

        public static BaseResponse<object> Fail(string message = "Fail", string[] errors = default(string[]))
        {
            return new BaseResponse<object>()
            {
                Status = false,
                Message = message,
                Errors = errors ?? new string[] { },
                Data = default(object)
            };
        }
    }

    public class BaseResponse : BaseResponse<object>
    {

    }

    public class BasePagedResponse<T> : BaseResponse<T>
    {
        public int RecordCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}