namespace TeisterMask.DataProcessor
{
    using System;
    using Data;
    using System.Linq;
    using System.Globalization;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            throw new NotImplementedException();
        }

        /*Select the top 10 employees who have at least one task that its open date is after or equal 
         * to the given date with their tasks that meet the same requirement 
         * (to have their open date after or equal to the giver date).
         * For each employee, export their username and their tasks. For each task,
         * export its name and open date (must be in format "d"), 
         * due date (must be in format "d"), label and execution type. 
         * Order the tasks by due date (descending), then by name (ascending). 
         * Order the employees by all tasks count (descending), then by username (ascending).*/

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
              .Employees
              .Select(e => new
              {
                  Username = e.Username,
                  Tasks = e.EmployeesTasks
                      .Where(t => t.Task.OpenDate >= date)
                      .OrderByDescending(t => t.Task.DueDate)
                      .ThenBy(t => t.Task.Name)
                      .Select(t => new
                      {
                          TaskName = t.Task.Name,
                          OpenDate = t.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                          DueDate = t.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                          LabelType = t.Task.LabelType.ToString(),
                          ExecutionType = t.Task.ExecutionType.ToString()
                      })
                      .ToList()
              })
              .ToList()
              .OrderByDescending(e => e.Tasks.Count)
              .ThenBy(e => e.Username)
              .Take(10)
              .ToList();

            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;
        }
    }
}