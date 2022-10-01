using Microsoft.AspNetCore.Mvc;
using Staff.Portal.DataAccessLayer.Models;

using Staff.Portal.Repository;
using System.Diagnostics.Metrics;

namespace Staff.Portal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController : ControllerBase
{
    private readonly IGenericRepository _IGenericRepository;

    public GenericController(IGenericRepository MyGenericRepository)
    {
        _IGenericRepository = MyGenericRepository;
    }

    [HttpGet, Route("[action]", Name = "GetGender")]
    public async Task<List<GenderModel>> GetGender()
    {
        List<GenderModel> _Gender = await _IGenericRepository.GetGender();


        return _Gender;
    }

    [HttpGet, Route("[action]", Name = "GetQualification")]
    public async Task<List<QualificationModel>> GetQualification()
    {
        List<QualificationModel> _Qualification = await _IGenericRepository.GetQualification();

        return _Qualification;
    }

}
