using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace MobileOperator
{
    /// <summary>
    /// Persons data generator.
    /// </summary>
    public class PersonsDataGenerator : IEntityDataGenerator<Person>
    {
        private readonly string[] genders = { "Мужской", "Женский" };

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbersDataGenerator"/> class.
        /// </summary>
        /// <param name="faker">Creator of person random data.</param>
        public PersonsDataGenerator(Faker<Person> faker)
        {
            Faker = faker;
        }

        private Faker<Person> Faker { get; }

        /// <summary>
        /// Get entities with random unique data.
        /// </summary>
        /// <param name="quantity">Quantity of entities.</param>
        /// <returns>Entities with random unique data.</returns>
        public IEnumerable<Person> GenerateEntities(int quantity)
        {
            var entities = new List<Person>();
            for (var i = 0; i < quantity; i++)
            {
                entities.Add(GenerateNewUniqueEntityFor(entities));
            }

            return entities;
        }

        private Person GenerateNewUniqueEntityFor(List<Person> entities)
        {
            while (true)
            {
                var newEntity = GenerateNewEntity();
                if (IsUniquePerson(newEntity, entities))
                {
                    return newEntity;
                }
            }
        }

        private Person GenerateNewEntity()
        {
            Faker.RuleFor(person => person.Address, faker => faker.Address.FullAddress())
                .RuleFor(person => person.Email, faker => faker.Internet.Email())
                .RuleFor(person => person.Gender, faker => faker.PickRandom(genders))
                .RuleFor(person => person.First_name, (faker, person) => faker.Name.FirstName())
                .RuleFor(person => person.Last_name, faker => faker.Name.LastName())
                .RuleFor(person => person.Middle_name, faker => faker.Name.LastName())
                .RuleFor(person => person.Series_passport, faker => faker.Phone.PhoneNumber("####"))
                .RuleFor(person => person.Number_passport, faker => faker.Phone.PhoneNumber("######"))
                .RuleFor(person => person.Birthdate, faker => faker.Date
                    .Between(new DateTime(1, 1, 1), new DateTime(2022, 12, 28)));

            var newPerson = Faker.Generate();

            return newPerson;
        }

        private bool IsUniquePerson(Person newUser, List<Person> persons)
        {
            return persons.All(person =>
                person.Email != newUser.Email &&
                person.Series_passport + person.Number_passport != newUser.Series_passport + newUser.Number_passport);
        }
    }
}