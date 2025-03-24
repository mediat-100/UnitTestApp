using ServiceContracts;
using ServiceContracts.Dto;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry

        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        // When the countryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest { CountryName = null };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }

        // When the countryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest request1 = new() { CountryName = "Ghana" };
            CountryAddRequest request2 = new() { CountryName = "Ghana" };           

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        // When you supply proper country name, it should insert(add) the country to the existing list of countries
        [Fact]
        public void AddCountry_InsertCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new() { CountryName = "Ghana" };

            // Act
            CountryResponse? response = _countriesService.AddCountry(request1);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            // Assert
            Assert.True(response?.CountryId != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);
        }

        #endregion

        #region GetAllCountries

        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // act
            var actual_country_response_list = _countriesService.GetAllCountries();

            // assert
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest(){ CountryName = "USA"},
                new CountryAddRequest(){ CountryName = "UK"}
            };

            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            // act
            foreach (CountryAddRequest country_request in country_request_list)
            {
                countries_list_from_add_country.Add(
                _countriesService.AddCountry(country_request));
            }

            List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

            // assert
            foreach(CountryResponse expectedCountryResponse in countries_list_from_add_country)
            {
                Assert.Contains(expectedCountryResponse, actualCountryResponseList);
            }
        }

        #endregion

        #region GetCountry

        [Fact]
        public void GetCountry_NullCountryId()
        {
            // arrange
            Guid? countryId = null;

            // act
            CountryResponse? expected_null_response = _countriesService.GetCountry(countryId);

            // assert
            Assert.Null(expected_null_response);
        }

        [Fact]
        public void GetCountry_CountryDetails() 
        {
            // arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            // act
            var actual_response = _countriesService.GetCountry(countryResponse.CountryId);

            // assert
            Assert.Equal(countryResponse, actual_response);
        }

        #endregion
    }
}
