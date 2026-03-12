namespace Api.Models
{

    public class HrmsJsonResult
    {
        public bool success { get; set; }
        public object? Data { get; set; }
        public List<ApiResponseMessages> ApiResponseMessages { get; set; } = new();

    }
    public class ApiResponseMessages{
        public MessageTypeEnum MessageTypeEnum { get; set; }
        public string FieldName { get; set; }
        public string Message { get; set; }

    }
    public enum MessageTypeEnum
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }

}
