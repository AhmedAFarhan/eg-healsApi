namespace BuildingBlocks.Exceptions
{
    public class AppException : Exception
    {
        public string Title { get; set; } = default!;
        public int StatusCode { get; set; }
        public List<string> Errors{ get; set; } = default!;

        public AppException(string message) : base(message) { }

        public AppException(string title, int statusCode, List<string> errors) : base(errors.First())
        {
            Title = title;
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
