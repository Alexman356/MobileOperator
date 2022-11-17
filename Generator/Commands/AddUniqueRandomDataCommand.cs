using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows;

namespace MobileOperator
{
    /// <summary>
    /// Command for adding unique entities with random data for database MobileOperator2022.
    /// </summary>
    public class AddUniqueRandomDataCommand // TODO async
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUniqueRandomDataCommand"/> class.
        /// </summary>
        public AddUniqueRandomDataCommand()
        {
            EntityGenerator = new EntityGenerator();
        }

        private delegate List<TEntity> GetUniqueEntity<TEntity>(List<TEntity> entities);

        private EntityGenerator EntityGenerator { get; }

        /// <summary>
        /// Execute query.
        /// </summary>
        public void Execute()
        {
            MessageBox.Show("Entity creation was started");

            var numbers = GetSimpleEntity<Number>(0, GetUniqueNumbers);
            MessageBox.Show($"{numbers.Count()} numbers will be added");
            Context.Get().Numbers.AddRange(numbers);

            var employees = GetEmployees(0);
            MessageBox.Show($"{employees.Count()} employees will be added");
            Context.Get().Employees.AddRange(employees);

            try
            {
                Context.Get().SaveChanges();
                MessageBox.Show("Entity was successfully created");
            }
            catch (DbEntityValidationException exception)
            {
                string errors = GetErrors(exception);
                MessageBox.Show(errors);
            }
        }

        private List<Employee> GetEmployees(int quantity)
        {
            var employees = EntityGenerator.Generate<Employee>(quantity).ToList();
            var uniqueEmployees = GetUniqueEmployees(employees).ToList();
            var uniqueEmployeesWithDependencies = GetUniqueEmployeesWithDependencies(quantity, uniqueEmployees);
            int duplicatesQuantity = employees.Count() - uniqueEmployeesWithDependencies.Count();
            if (duplicatesQuantity != 0)
            {
                MessageBox.Show($"{duplicatesQuantity} generated employees was deleted");
            }

            return uniqueEmployeesWithDependencies;
        }

        private List<Employee> GetUniqueEmployeesWithDependencies(int quantity, List<Employee> uniqueEmployees)
        {
            var persons = GetSimpleEntity<Person>(quantity, GetUniquePersons);
            var users = GetSimpleEntity<User>(quantity, GetUniqueUsers);
            for (var i = 0; i < uniqueEmployees.Count; i++)
            {
                uniqueEmployees[i].Person = persons[i];
                uniqueEmployees[i].User = users[i];
            }

            return uniqueEmployees;
        }

        private List<TEntity> GetSimpleEntity<TEntity>(int quantity, GetUniqueEntity<TEntity> getUniqueEntity)
            where TEntity : IEntity, new()
        {
            var uniqueEntity = new List<TEntity>();
            int quantityToGenerateInCycle = quantity;
            while (quantityToGenerateInCycle != 0)
            {
                var entities = EntityGenerator.Generate<TEntity>(quantityToGenerateInCycle).ToList();
                var uniqueEntitiesInCycle = getUniqueEntity(entities).ToList();
                int duplicatesQuantity = quantityToGenerateInCycle - uniqueEntitiesInCycle.Count;
                quantityToGenerateInCycle = duplicatesQuantity;
                uniqueEntity.AddRange(uniqueEntitiesInCycle);
            }

            return uniqueEntity;
        }

        private List<Person> GetUniquePersons(List<Person> persons)
        {
            var uniquePersons = persons;
            IQueryable<Person> dbPersons = Context.Get().Persons;
            foreach (var dbPerson in dbPersons)
            {
                uniquePersons = uniquePersons
                    .Where(person =>
                        person.Email != dbPerson.Email &&
                        person.Series_passport + person.Number_passport !=
                        dbPerson.Series_passport + dbPerson.Number_passport)
                    .ToList();
            }

            return uniquePersons;
        }

        private List<User> GetUniqueUsers(List<User> users)
        {
            var uniqueUsers = users;
            IQueryable<User> dbUsers = Context.Get().Users;
            foreach (var dbUser in dbUsers)
            {
                uniqueUsers = uniqueUsers
                    .Where(user => user.Login != dbUser.Login)
                    .ToList();
            }

            return uniqueUsers;
        }

        private List<Employee> GetUniqueEmployees(List<Employee> employees)
        {
            var uniqueEmployees = employees;
            IQueryable<Employee> dbEmployees = Context.Get().Employees;
            foreach (var dbEmployee in dbEmployees)
            {
                uniqueEmployees = uniqueEmployees
                    .Where(employee =>
                        employee.user_login != dbEmployee.user_login &&
                        employee.person_ID != dbEmployee.person_ID)
                    .ToList();
            }

            return uniqueEmployees;
        }

        private List<Number> GetUniqueNumbers(List<Number> numbers)
        {
            var dbNumbers = Context.Get().Numbers.Select(number => number.Number_telephone);
            var uniqueNumbers = numbers
                .Where(number => !dbNumbers.Contains(number.Number_telephone))
                .ToList();

            return uniqueNumbers;
        }

        private static string GetErrors(DbEntityValidationException exception)
        {
            var errors = new StringBuilder();
            foreach (var entityValidationError in exception.EntityValidationErrors)
            {
                errors.AppendLine("Object: " + entityValidationError.Entry.Entity);

                foreach (var validationError in entityValidationError.ValidationErrors)
                {
                    errors.AppendLine(validationError.ErrorMessage + " ");
                }
            }

            return errors.ToString();
        }
    }
}