
using Microsoft.AspNetCore.Mvc;
using Staff.Portal.DataAccessLayer.Models;
using Staff.Portal.Repository;
using System.Runtime.InteropServices;

namespace Staff.Portal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController : ControllerBase
{
    private readonly IStaffRepository _IStaffRepository;
   

    public StaffController(IStaffRepository MyIStaffRepository, IGenericRepository MyGenericRepository)
    {
        this._IStaffRepository = MyIStaffRepository;
        
    }

    [HttpGet(), Route("[action]", Name = "GetStaffs")]
    public async Task<List<StaffModel>> GetStaffs(string emp)
    {
        if(string.IsNullOrWhiteSpace(emp))
        {
            emp = "";
        }
        List<StaffModel> _Staff = await _IStaffRepository.GetEmployee(emp);

        return _Staff;
    }

    [HttpGet, Route("[action]", Name = "GetStaffDetail")]
    public async Task<StaffModel> GetStaffDetail(string emp)
    {
        StaffModel _Staff = await _IStaffRepository.GetEmployeeDetail(emp);

        return _Staff;
    }

    [HttpGet, Route("[action]", Name = "CheckEmploymentNumberIsUnique")]
    public async Task<bool> CheckEmploymentNumberIsUnique(string emp)
    {
        return await _IStaffRepository.CheckEmploymentNumberIsUnique(emp); 
    }

    [HttpGet, Route("[action]", Name = "SaveStaff")]
    public async Task<bool> SaveStaff(string Options, string EmpNumber, string FirstName,
        string LastName, string DateofBirth, int YearOfExperience, int GenderID, int QualificationID)
    {
        StaffModel _Staff = new();
     

        _Staff.employment_number = EmpNumber;
        _Staff.first_name = FirstName;
        _Staff.last_name = LastName;
        _Staff.birth_date = Convert.ToDateTime(DateofBirth);
        _Staff.years_work_experience = YearOfExperience;
        _Staff.gender_id = GenderID;
        _Staff.qualification_id = QualificationID;


     


        return await _IStaffRepository.SaveEmployee(Options, QualificationID, _Staff); ;
    }




}
