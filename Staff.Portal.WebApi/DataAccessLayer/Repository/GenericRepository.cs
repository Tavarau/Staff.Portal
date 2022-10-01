using Dapper;
using Staff.Portal.BusinessAccess;
using Staff.Portal.DataAccessLayer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

namespace Staff.Portal.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection db;
        private static string _ConString = "";

        public GenericRepository()
        {
            IConfiguration conf = (new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());
            _ConString = conf["DBConString"].ToString();
            db = new SqlConnection(_ConString);

        }
        public async Task<List<GenderModel>> GetGender()
        {

            string query = @"select gender_id,gender_description from app_gender";

            var _Gendermodel = await db.QueryAsync<GenderModel>(query);

            return _Gendermodel.ToList();
        }
        public async Task<List<QualificationModel>> GetQualification()
        {
            string query = @"SELECT QUALIFICATION_ID,QUALIFICATION_LEVEL,QUALIFICATION_DESCRIPTION FROM APP_QUALIFICATION";

            var Qualification = await db.QueryAsync<QualificationModel>(query);

            return Qualification.ToList();
        }
        public  int GetQualificationLevel(int QualificationID)
        {
            string query = @"SELECT QUALIFICATION_LEVEL FROM APP_QUALIFICATION where QUALIFICATION_ID=@QUALIFICATION_ID";

            var _Qualimodel =  db.Query<QualificationModel>(query, new { QUALIFICATION_ID = QualificationID }).ToList();

            if(_Qualimodel!=null)
                return _Qualimodel[0].QUALIFICATION_LEVEL;


            return 0;


        }

    }
}
