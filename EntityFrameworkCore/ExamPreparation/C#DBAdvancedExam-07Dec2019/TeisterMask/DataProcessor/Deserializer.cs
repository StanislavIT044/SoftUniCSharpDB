namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Xml.Serialization;
    using TeisterMask.DataProcessor.ImportDto;
    using System.IO;
    using System.Text;
    using TeisterMask.Data.Models;
    using System.Linq;
    using System.Globalization;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models.Enums;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            string rootName = "Projects";

            XmlSerializer serializer = new XmlSerializer(typeof(ImportProjectDto),
                                       new XmlRootAttribute(rootName));

            List<ImportProjectDto> importProjects = (List<ImportProjectDto>)serializer
                .Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            List<Project> projects = new List<Project>();

            foreach (var proj in importProjects)
            {
                if (!IsValid(proj))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? dueDate;

                if (!string.IsNullOrEmpty(proj.DueDate) && !string.IsNullOrWhiteSpace(proj.DueDate))
                {
                    dueDate = DateTime.ParseExact(proj.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dueDate = null;
                }

                Project project = new Project
                {
                    Name = proj.Name,
                    OpenDate = DateTime.ParseExact(proj.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DueDate = dueDate
                };

                foreach (var task in proj.Tasks) 
                {
                    if (!IsValid(task))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task t = new Task();

                    project.Tasks.Add(t);
                }

                projects.Add(project);
                sb.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var importEmployees = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

            StringBuilder sb = new StringBuilder();

            List<Employee> employees = new List<Employee>();

            foreach (var emp in importEmployees)
            {
                if (!IsValid(emp))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee
                {
                    Username = emp.Username,
                    Email = emp.Email,
                    Phone = emp.Phone
                };

                int counter = 0;

                foreach (var task in emp.Tasks.Distinct())
                {
                    Task t = context.Tasks.Find(task);

                    if (t == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    counter++;
                }

                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, counter));
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}