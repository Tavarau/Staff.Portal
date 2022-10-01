namespace Staff.Portal.DataAccessLayer.Models;

public class StaffModel
{
    public string? employment_number { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public DateTime  birth_date { get; set; }
    public double salary { get; set; }
    public int years_work_experience { get; set; }
    public int gender_id { get; set; }
    public int qualification_id { get; set; }
    public string? gender { get; set; }
    public string? qualification { get; set; }


}


