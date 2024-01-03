namespace Common.BaseCore.Results.Concreate
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false)
        {
        }
        public ErrorResult( string message) : base(false, message)
        {
        }
        public ErrorResult( string message, string errorType, string projectName) : base(false, message, errorType, projectName)
        {
        }
    }
}
