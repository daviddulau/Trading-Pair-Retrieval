using System.Net;

namespace PriceRetrieval.Providers
{
    public record Result
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.OK;
        public string? Message { get; }

        protected Result()
        {
        }

        public Result(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Message = string.Empty;
        }

        public Result(HttpStatusCode status, string? message)
        {
            StatusCode = status;
            Message = message;
        }

        public bool IsSuccessStatusCode => this.StatusCode == HttpStatusCode.OK;

        public static Result<TData> Success<TData>(TData data)
        {
            return new Result<TData>(data);
        }

        public static Result<TData> Error<TData>(string message)
        {
            return new Result<TData>(HttpStatusCode.InternalServerError, message);
        }
    }

    public record Result<TResult> : Result
    {
        public TResult? Data { get; }

        public Result(HttpStatusCode status)
            : base(status)
        {
        }

        public Result(HttpStatusCode status, string? message)
            : base(status, message)
        {
        }

        public Result(TResult data)
        {
            Data = data;
        }
    }
}
