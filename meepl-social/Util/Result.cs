namespace Meepl.Util
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}