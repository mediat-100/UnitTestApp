using Entities;
using ServiceContracts;
using ServiceContracts.Dto;
using Services.Helpers;
using System;

namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }


        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
                throw new ArgumentNullException(nameof(personAddRequest));

            // Model Validation
            ValidationHelper.ModelValidation(personAddRequest);

            Person person = personAddRequest.ToPerson();

            person.PersonId = Guid.NewGuid();

            _persons.Add(person);

           return ConvertPersonToPersonResponse(person);
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountry(person.CountryId)?.CountryName;

            return personResponse;
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(x => x.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPerson(Guid? personId)
        {
            if (personId is null)
                return null;

            var person = _persons.FirstOrDefault(x => x.PersonId == personId);
            if (person is null)
                return null;

            return person.ToPersonResponse();
        }
    }
}
