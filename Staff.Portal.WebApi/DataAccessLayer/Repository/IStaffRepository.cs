using Staff.Portal.DataAccessLayer.Models;

namespace Staff.Portal.Repository
{
    public interface IStaffRepository
    {
        public Task<List<StaffModel>> GetEmployee(string _EmployeeNumber);
        public Task<bool> SaveEmployee(string Option, int Level, StaffModel _StaffModel);
        public Task<StaffModel> GetEmployeeDetail(string _EmployeeNumber);
        public Task<bool> CheckEmploymentNumberIsUnique(string _EmployeeNumber);
    }
}
