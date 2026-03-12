using Api.Models;

namespace Api.Helpers
{
    public class UiHelper
    {
        public static bool CheckForValidationMessages(List<ApiResponseMessages> messages)
        {
            return messages != null && messages.Any(m => m.MessageTypeEnum == MessageTypeEnum.Error || m.MessageTypeEnum == MessageTypeEnum.Warning);
        }
    }
}
