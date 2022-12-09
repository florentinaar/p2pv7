namespace p2pv7.Services
{
    public class ServiceResponse<T>
    {
        public Guid IDs { get; set; }
        public string Token { get; set; }
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

    }
}
