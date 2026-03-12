using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        public bool UpsertAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages)
        {
            return _attendanceRepository.UpsertAttendanceData(attendance, validationMessages);
        }

        public List<AttendanceModel> GetAttendanceData(AttendanceModel attendance,List<ApiResponseMessages> validationMessages)
        {
            return _attendanceRepository.GetAttendanceData(attendance, validationMessages);
        }
    }
}
