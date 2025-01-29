namespace Meepl.Util
{
    public class Result<T>(Result<List<ulong>> friends)
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public List<ulong> Value { get; set; }
    }
}