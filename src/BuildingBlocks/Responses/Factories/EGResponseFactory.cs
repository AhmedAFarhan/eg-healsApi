namespace BuildingBlocks.Responses.Factories
{
    public static class EGResponseFactory
    {
        public static EGResponse<T> Success<T>(T data, string message) => new()
        {
            Success = true,
            Code = 200,
            Data = data,
            Message = message,
            Errors = new()
        };

        public static EGResponse<T> Fail<T>(string message, List<string> errors, int code, string traceId) => new()
        {
            Success = false,
            Message = message,
            Errors = errors ?? new(),
            TraceId = traceId,
            Code = code
        };
    }
}
