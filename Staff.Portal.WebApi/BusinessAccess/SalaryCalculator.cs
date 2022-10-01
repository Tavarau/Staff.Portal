namespace Staff.Portal.BusinessAccess;

public class SalaryCalculator: ISalaryCalculator
{
    private double _Salary = 0d;

    public SalaryCalculator()
    {
    }

    public double CalculateSalary(int QualificationLevel, int YearOfExperience)
    {
        _Salary = Math.Round((QualificationLevel / 10.0) * (YearOfExperience / 5.0) * 100000.0, 2, MidpointRounding.AwayFromZero);

        return _Salary;
    }
}
