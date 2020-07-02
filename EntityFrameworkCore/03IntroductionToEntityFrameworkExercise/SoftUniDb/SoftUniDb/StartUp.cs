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
            Console.WriteLine(GetEmployeesFullInformation(context));

            //Problem04
            Console.WriteLine(GetEmployeesWithSalaryOver50000(context));

            //Problem05
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));

            //Problem06
            Console.WriteLine(AddNewAddressToEmployee(context));

            //Problem07
            Console.WriteLine(GetEmployeesInPeriod(context));

            //Problem08
            Console.WriteLine(GetAddressesByTown(context));

            //Problem09
            Console.WriteLine(GetEmployee147(context));

            //Problem10
            Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));

            //Problem11
            Console.WriteLine(GetLatestProjects(context));

            //Problem12
            Console.WriteLine(IncreaseSalaries(context));

            //Problem13
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));

            //Problem14
            Console.WriteLine(DeleteProjectById(context));

            //Problem15
            Console.WriteLine(RemoveTown(context));
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

        //Problem08
        public static string GetAddressesByTown(SoftUniContext context) 
        {
            var addressesInfo = context
                .Addresses
                .OrderByDescending(x => x.Employees.Count)
                .ThenBy(x => x.Town.Name)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .Select(x => new
                {
                    x.AddressText,
                    TownName = x.Town.Name,
                    EmployeeCount = x.Employees.Count
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var address in addressesInfo)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeeCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem09
        public static string GetEmployee147(SoftUniContext context)
        {
            var employeeInfo = context
                .Employees
                .Where(x => x.EmployeeId == 147)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects
                                     .Select(x => new
                                     {
                                         x.Project.Name
                                     })
                                     .OrderBy(x => x.Name)
                                     .ToList()
                })
                .First();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{employeeInfo.FirstName} {employeeInfo.LastName} - {employeeInfo.JobTitle}");

            foreach (var project in employeeInfo.Projects)
            {
                sb.AppendLine($"{project.Name}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context) 
        {
            var departmentsInfo = context
                .Departments
                .Where(x => x.Employees.Count() > 5)
                .OrderBy(x => x.Employees.Count())
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    ManagerName = $"{x.Manager.FirstName} {x.Manager.LastName}",
                    Employees = x.Employees
                                    .Select(x => new
                                    {
                                        x.FirstName,
                                        x.LastName,
                                        x.JobTitle
                                    })
                                    .OrderBy(x => x.FirstName)
                                    .ThenBy(x => x.LastName)
                                    .ToList()
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var dep in departmentsInfo)
            {
                sb.AppendLine($"{dep.Name} - {dep.ManagerName}");

                foreach (var emp in dep.Employees)
                {
                    sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projectsInfo = context
                .Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt")
                })
                .OrderBy(x => x.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var project in projectsInfo)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(x => x.Department.Name == "Engineering" ||
                            x.Department.Name == "Tool Design" ||
                            x.Department.Name == "Marketing" ||
                            x.Department.Name == "Information Services")
                .ToList();

            foreach (var emp in employees)
            {
                emp.Salary *= 1.12M;
            }

            context.SaveChanges();

            var employeesWithIncreasedSalaty = context
                .Employees
                .Where(x => x.Department.Name == "Engineering" ||
                            x.Department.Name == "Tool Design" ||
                            x.Department.Name == "Marketing" ||
                            x.Department.Name == "Information Services")
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employeesWithIncreasedSalaty)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            if (!employees.Any())
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem14 --X
        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectsToDelete = context
                .EmployeesProjects
                .Where(x => x.ProjectId == 2)
                .ToList();

            context.EmployeesProjects.RemoveRange(projectsToDelete);

            var targetProject = context
                .Projects
                .Where(x => x.ProjectId == 2)
                .FirstOrDefault();

            context.Projects.Remove(targetProject);

            var takeProjects = context
                .Projects
                .Take(10)
                .Select(x => new { x.Name })
                .ToList();

            var result = new StringBuilder();

            foreach (var proj in takeProjects)
            {
                result.AppendLine(proj.Name);
            }

            return result.ToString().TrimEnd();
        }

        //Problem15 --X
        public static string RemoveTown(SoftUniContext context)
        {
            var empInSettle = context
                .Employees
                .Where(x => x.Address.Town.Name == "Seattle")
                .ToList();

            foreach (var emp in empInSettle)
            {
                emp.AddressId = null;
            }

            var countOfAddesses = context
                .Addresses
                .Where(x => x.Town.Name == "Seattle")
                .Count();

            var removeAddresses = context
                .Addresses
                .Where(x => x.Town.Name == "Seattle")
                .ToList();

            context.Addresses.RemoveRange(removeAddresses);
            context.SaveChanges();

            var city = context
                .Towns
                .Where(x => x.Name == "Seattle")
                .FirstOrDefault();

            context.Remove(city);
            context.SaveChanges();

            return $"{countOfAddesses} addresses in Seattle were deleted";
        }
    }
}
