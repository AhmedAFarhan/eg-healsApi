namespace BuildingBlocks.Responses.Abstractions
{
    public abstract class EGResponseBase
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public string? TraceId { get; set; }
        public int Code { get; set; }
    }
}
