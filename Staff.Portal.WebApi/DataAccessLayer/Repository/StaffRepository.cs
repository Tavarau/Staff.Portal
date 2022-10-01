using Dapper;
using Staff.Portal.BusinessAccess;
using Staff.Portal.DataAccessLayer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

namespace Staff.Portal.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IDbConnection db;
        private static string _ConString = "";

        private ISalaryCalculator _MySalaryCalculator { get; set; }
        private readonly IGenericRepository _IGenericRepository;

        public StaffRepository(ISalaryCalculator MySalaryCalculator, IGenericRepository iGenericRepository)
        {
            IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
            _ConString = conf["DBConString"].ToString();
            db = new SqlConnection(_ConString);


            this._MySalaryCalculator = MySalaryCalculator;
            _IGenericRepository = iGenericRepository;
        }
        public async Task<List<StaffModel>> GetEmployee(string _EmployeeNumber)
        {
            string query = @" SELECT [EMPLOYMENT_NUMBER],[FIRST_NAME],[LAST_NAME],[BIRTH_DATE],[SALARY],[YEARS_WORK_EXPERIENCE],G.GENDER_ID,Q.QUALIFICATION_ID,g.GENDER_DESCRIPTION as Gender,q.QUALIFICATION_DESCRIPTION as Qualification from APP_STAFF  H 
                            LEFT JOIN APP_GENDER G ON G.GENDER_ID=H.GENDER_ID
                            LEFT JOIN APP_QUALIFICATION Q ON Q.QUALIFICATION_ID=H.QUALIFICATION_ID
                            where EMPLOYMENT_NUMBER Like '%' + @EmployeeNumber + '%'";

            var _Staffmodel = await db.QueryAsync<StaffModel>(query, new { EmployeeNumber = _EmployeeNumber });

            return _Staffmodel.ToList();
        }
        public async Task<StaffModel> GetEmployeeDetail(string _EmployeeNumber)
        {

            string query = @" SELECT [EMPLOYMENT_NUMBER],[FIRST_NAME],[LAST_NAME],[BIRTH_DATE],[SALARY],[YEARS_WORK_EXPERIENCE],[GENDER_ID],[QUALIFICATION_ID] from APP_STAFF  H 
                            where EMPLOYMENT_NUMBER = @EmployeeNumber";

            var _Staffmodel = await db.QueryFirstAsync<StaffModel>(query, new { EmployeeNumber = _EmployeeNumber });

            return _Staffmodel;
        }

        public async Task<bool> CheckEmploymentNumberIsUnique(string _EmployeeNumber)
        {
            bool IsUnique = true;
            // StaffsModel _Staffmodel = new StaffsModel();
            string query = @" SELECT [EMPLOYMENT_NUMBER] FROM APP_STAFF
                            where EMPLOYMENT_NUMBER = @EmployeeNumber";

            var _Staffmodel = await db.QueryAsync<StaffModel>(query, new { EmployeeNumber = _EmployeeNumber });

            if (_Staffmodel.Any())
            {
                IsUnique = false;
            }

            return IsUnique;
        }

        public async Task<bool> SaveEmployee(string Option, int QualificationID, StaffModel _StaffModel)
        {
            bool IsSuccess = false;
            string Statement = "";

            object _ObjectParam = new();

            if (Option == "Add")
            {

                //Get Level
                int Level = _IGenericRepository.GetQualificationLevel(QualificationID);
                //Calculate Salary
                double _Salary = _MySalaryCalculator.CalculateSalary(Level, _StaffModel.years_work_experience);

                _ObjectParam =
              new
              {
                  EmployeeNumber = _StaffModel.employment_number,
                  FirstName = _StaffModel.first_name,
                  LastName = _StaffModel.last_name,
                  DateOfBirth = _StaffModel.birth_date.ToString("yyyy-MM-dd"),
                  Salary = _Salary,
                  YearOfExperience = _StaffModel.years_work_experience,
                  GenderID = _StaffModel.gender_id,
                  QualificationID = _StaffModel.qualification_id,
              };

                Statement = @"INSERT INTO APP_STAFF (EMPLOYMENT_NUMBER,FIRST_NAME,LAST_NAME,BIRTH_DATE,SALARY,YEARS_WORK_EXPERIENCE,GENDER_ID,QUALIFICATION_ID)
                              VALUES (@EmployeeNumber,@FirstName,@LastName,@DateOfBirth,@Salary,@YearOfExperience,@GenderID,@QualificationID)";

            }
            else if (Option == "Update")
            {

                _ObjectParam =
             new
             {
                 EmployeeNumber = _StaffModel.employment_number,
                 FirstName = _StaffModel.first_name,
                 LastName = _StaffModel.last_name,
                 DateOfBirth = _StaffModel.birth_date.ToString("yyyy-MM-dd")
             };

                Statement = @"UPDATE APP_STAFF SET FIRST_NAME=@FirstName,LAST_NAME=@LastName,BIRTH_DATE=@DateOfBirth WHERE  EMPLOYMENT_NUMBER=@EmployeeNumber";
            }
            else if (Option == "Delete")
            {
                _ObjectParam =
               new
               {
                   EmployeeNumber = _StaffModel.employment_number
               };

                Statement = @"DELETE APP_STAFF WHERE EMPLOYMENT_NUMBER=@EmployeeNumber";
            }

            int RowAffected = await db.ExecuteAsync(Statement, _ObjectParam);

            if (RowAffected > 0)
                IsSuccess = true;


            return IsSuccess;
        }


    }
}
