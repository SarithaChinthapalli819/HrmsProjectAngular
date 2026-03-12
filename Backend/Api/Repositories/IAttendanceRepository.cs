using Api.Models;

namespace Api.Repositories
{
    public interface IAttendanceRepository
    {
        public bool UpsertAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages);
        public List<AttendanceModel> GetAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages);
    }
}
