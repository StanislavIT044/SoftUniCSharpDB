using System;
using System.Collections.Generic;
using System.Globalization;
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
            //Console.WriteLine(GetEmployeesFullInformation(context));

            //Problem04
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));

            //Problem05
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));

            //Problem06
            //Console.WriteLine(AddNewAddressToEmployee(context));

            //Problem07
            Console.WriteLine(GetEmployeesInPeriod(context));
        }

        //Problem03
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesInfo = context
                .Employees
                .Select(x => 
                new 
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.MiddleName, 
                    x.JobTitle,
                    x.Salary, 
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            foreach (var employee in employeesInfo)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem04
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesInfo = context
                .Employees
                .Select(x =>
                new
                {
                    x.FirstName,
                    x.Salary
                })
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .ToList();

            foreach (var employee in employeesInfo)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem05
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesInfo = context
                .Employees
                .Join(context.Departments,
                      employee => employee.DepartmentId,
                      dep => dep.DepartmentId,
                      (employee, department) => new
                      {
                          FirstName = employee.FirstName,
                          LastName = employee.LastName,
                          Salary = employee.Salary,
                          DepartmentName = department.Name
                      })
                .Where(x => x.DepartmentName == "Research and Development")
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName);

            foreach (var employee in employeesInfo)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem06
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address newAddress = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
            context.Add(newAddress);
            context.SaveChanges();

            var targetEmployee = context
                .Employees
                .Where(x => x.LastName == "Nakov")
                .FirstOrDefault();
            targetEmployee.AddressId = newAddress.AddressId;
            context.SaveChanges();

            var allEmployeeAddresses = context
                .Employees
                .Join(context.Addresses,
                      employees => employees.AddressId,
                      address => address.AddressId,
                      (employee, address) => new
                      {
                          AddressId = employee.AddressId,
                          AddressText = address.AddressText
                      })
                .OrderByDescending(x => x.AddressId)
                .Take(10);

            StringBuilder sb = new StringBuilder();

            foreach (var address in allEmployeeAddresses)
            {
                sb.AppendLine(address.AddressText);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem07
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var targetEmployee = context
                .Employees
                .Where(x => x.EmployeesProjects
                                .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    ManagerFirstName = x.Manager.FirstName,
                    ManagerLastName = x.Manager.LastName,
                    Projects = x.EmployeesProjects
                                    .Select(e => new
                                    {
                                        e.Project.Name,
                                        StartDate = e.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                                        EndDate = e.Project.EndDate.HasValue
                                                                        ? e.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                                                                        : "not finished"
                                    })
                                    .ToList()
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var employee in targetEmployee)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                
                foreach (var project in employee.Projects)
                {
                    sb.AppendLine($"--{project.Name} - {project.StartDate} - {project.EndDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
