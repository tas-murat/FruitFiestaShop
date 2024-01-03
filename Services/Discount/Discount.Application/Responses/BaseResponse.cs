namespace Discount.Application.Response
{
    public class BaseResponse
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public string ProjectName { get; set; }
        public string ErrorType { get; set; }
        public BaseResponse()
        {
            
        }
        public BaseResponse(object? result)
        {
            Result = result;
        }
        public BaseResponse(bool isSuccess,string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public BaseResponse(string message,string errorType, string projectName) :this(false,message)
        {
           ProjectName = projectName;
            ErrorType = errorType;
        }
    }
}
