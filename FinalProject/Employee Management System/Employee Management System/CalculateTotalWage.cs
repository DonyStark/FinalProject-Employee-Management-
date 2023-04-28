namespace EmployeeManagementSystem
{
    class CalculateTotalWage
    {
        public string EmpName;
        public string EmpDepartment;
        public int EmpWageRate;
        public int EmpWorkedHours;
        public int EmpTotalWage;
        public CalculateTotalWage(string name, string department, int rate, int workedHour)
        {
            EmpName = name;
            EmpDepartment = department;
            EmpWageRate = rate;
            EmpWorkedHours = workedHour;
            EmpTotalWage = workedHour * rate;
        }
    }
}