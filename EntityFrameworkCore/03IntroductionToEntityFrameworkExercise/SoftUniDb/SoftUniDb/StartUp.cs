using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            //Problem03
            Console.WriteLine(GetEmployeesFullInformation(context));
        }

        //Problem03
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var EmployeesInfo = context
                .Employees
                .Select(x => new { x.EmployeeId, x.FirstName, x.LastName, x.MiddleName, x.JobTitle, x.Salary, })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            foreach (var employee in EmployeesInfo)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
