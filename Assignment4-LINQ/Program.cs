using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Program
{
    List<Employee> employeeList;
    List<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
    }

    public void Task1()
    {
        var totalSalaries = from e in employeeList
                            join s in salaryList on e.EmployeeID equals s.EmployeeID
                            group s by new { e.EmployeeFirstName, e.EmployeeLastName } into g
                            select new
                            {
                                Name = g.Key.EmployeeFirstName + " " + g.Key.EmployeeLastName,
                                TotalSalary = g.Sum(s => s.Amount)
                            };

        var orderedSalaries = totalSalaries.OrderBy(ts => ts.TotalSalary);

        foreach (var item in orderedSalaries)
        {
            Console.WriteLine($"Name: {item.Name}, Total Salary: {item.TotalSalary}");
        }

    }

    public void Task2()
    {
        var secondOldest = employeeList.OrderByDescending(e => e.Age)
        .Skip(1)
        .FirstOrDefault();

        if (secondOldest != null)
        {
            var totalSalary = salaryList
                .Where(s => s.EmployeeID == secondOldest.EmployeeID && s.Type == SalaryType.Monthly)
                .Sum(s => s.Amount);

            Console.WriteLine($"Employee: {secondOldest.EmployeeFirstName} {secondOldest.EmployeeLastName}, Age: {secondOldest.Age}, Total Monthly Salary: {totalSalary}");
        }

    }

    public void Task3()
    {
        var olderEmployees = employeeList
        .Where(e => e.Age > 30)
        .Select(e => e.EmployeeID)
        .ToList();

        var means = from s in salaryList
                    where olderEmployees.Contains(s.EmployeeID)
                    group s by s.Type into g
                    select new
                    {
                        SalaryType = g.Key,
                        MeanAmount = g.Average(s => s.Amount)
                    };

        foreach (var item in means)
        {
            Console.WriteLine($"Salary Type: {item.SalaryType}, Mean Amount: {item.MeanAmount}");
        }
    }
}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}