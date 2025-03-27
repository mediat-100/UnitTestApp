using Entities;
using ServiceContracts;
using ServiceContracts.Dto;
using ServiceContracts.Enums;
using Services;

using System;

namespace Test
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;

        public PersonsServiceTest()
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region AddPerson

        [Fact]
        public void AddPerson_NullPerson()
        {
            // arrange
            PersonAddRequest? personAddRequest = null;

            // assert
            Assert.Throws<ArgumentNullException>(
                // act
                () => _personsService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_PersonNameIsNull() 
        {
            // arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = null
            };

            // act
            Assert.Throws<ArgumentException>(() =>
            _personsService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_PersonEmailIsNull()
        {
            // arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Email = null
            };

            // act
            Assert.Throws<ArgumentException>(() =>
            _personsService.AddPerson(personAddRequest));
        }

        [Fact]
        public void AddPerson_PersonDetails()
        {
            // arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                Name = "Test test",
                Email = "testemail@gmail.com",
                Gender = GenderOptions.Male,
                RecieveNewsLetters = true,
                Dob = new DateTime(1999, 01, 01),
                Address = "some address",
                CountryId = Guid.NewGuid()
            };

            // act
            PersonResponse person = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> personsList = _personsService.GetAllPersons();

            // assert
            Assert.True(person.PersonId != Guid.Empty);
            Assert.Contains(person, personsList);
        }

        #endregion

        #region GetPerson

        [Fact]
        public void GetPerson_NullPersonId()
        {
            // arrange
            Guid? personId = null;

            // act
            PersonResponse? personResponse= _personsService.GetPerson(personId);

            // assert
            Assert.Null(personResponse);
        }

        [Fact]
        public void GetPerson_PersonDetails() 
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };

            CountryResponse? countryResponse = _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                Name = "Test test",
                Address = "Some Address",
                Dob = new DateTime(1991, 01, 01),
                Gender = GenderOptions.Male,
                RecieveNewsLetters = true,
                Email = "test@yopmail.com",
                CountryId = countryResponse?.CountryId
            };

            // act
            PersonResponse addPersonResponse = _personsService.AddPerson(personAddRequest);
            PersonResponse? getPersonResponse = _personsService.GetPerson(addPersonResponse.PersonId);
            //List<PersonResponse> getAllPersons = _personsService.GetAllPersons();

            // assert
            Assert.Equal(addPersonResponse, getPersonResponse);
            //Assert.Contains(getPersonResponse, getAllPersons);
        }

        #endregion

        #region GetAllPersons

        [Fact]
        public void GetAllPersons_EmptyListResponse()
        {
            // act
            var persons = _personsService.GetAllPersons();

            // assert
            Assert.Empty(persons);
        }

        [Fact]
        public void GetAllPersons_NotEmptyListResponse()
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest(){ CountryName = "Canada" };
            CountryResponse? countryResponse = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                Name = "Test test",
                Address = "Some Address",
                Dob = new DateTime(1991, 01, 01),
                Gender = GenderOptions.Male,
                RecieveNewsLetters = true,
                Email = "test@yopmail.com",
                CountryId = countryResponse?.CountryId
            };

            PersonAddRequest personAddRequest2 = new PersonAddRequest()
            {
                Name = "Test 2",
                Address = "Some Address 2",
                Dob = new DateTime(1992, 01, 01),
                Gender = GenderOptions.Female,
                RecieveNewsLetters = false,
                Email = "test2@yopmail.com",
                CountryId = countryResponse?.CountryId
            };

            List<PersonAddRequest> personRequests = new List<PersonAddRequest>() { personAddRequest1, personAddRequest2 };
            List<PersonResponse> personResponses = new List<PersonResponse>();

            foreach (var request in personRequests)
            {
                personResponses.Add(
                _personsService.AddPerson(request));
            }

            // act
            var expectedResponse = _personsService.GetAllPersons();
            // assert

            foreach (var response in personResponses)
            {
                Assert.Contains(response, expectedResponse);
            }
        }
        #endregion
    }
}
