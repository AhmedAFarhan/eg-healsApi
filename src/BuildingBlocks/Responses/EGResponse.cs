using BuildingBlocks.Responses.Abstractions;

namespace BuildingBlocks.Responses
{
    public class EGResponse<T> : EGResponseBase
    {
        public T? Data { get; set; }
    }
}
