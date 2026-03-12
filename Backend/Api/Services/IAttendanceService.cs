using Api.Models;

namespace Api.Services
{
    public interface IAttendanceService
    {
        public bool UpsertAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages);
        public List<AttendanceModel> GetAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages);
    }
}
