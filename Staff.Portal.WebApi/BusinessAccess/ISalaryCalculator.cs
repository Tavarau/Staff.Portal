namespace Staff.Portal.BusinessAccess
{
    public interface ISalaryCalculator
    {
        public double CalculateSalary(int QualificationLevel, int YearOfExperience);
    }
}
