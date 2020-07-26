namespace PUL.GS.Models.Common
{
    public class Response<T>
    {
        public T objectResult { get; set; }
        public bool Success { get; set; }
        public Error Error { get; set; }
    }
}
