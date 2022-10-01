using Staff.Portal.DataAccessLayer.Models;

namespace Staff.Portal.Repository
{
    public interface IGenericRepository
    {
        public Task<List<GenderModel>> GetGender();
        public Task<List<QualificationModel>> GetQualification();
        public int GetQualificationLevel(int QualificationID);

    }
}
