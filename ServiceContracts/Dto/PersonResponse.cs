using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Dto
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool RecieveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(PersonResponse)) 
                return false;

            PersonResponse person = (PersonResponse)obj;

            return PersonId == person.PersonId && Name == person.Name && Email == person.Email && Dob == person.Dob 
                && Gender == person.Gender && CountryId == person.CountryId && Address == person.Address 
                && person.RecieveNewsLetters == person.RecieveNewsLetters;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class PersonExtension
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
                    {
                        PersonId = person.PersonId,
                        Name = person.Name,
                        Email = person.Email,
                        Dob = person.Dob,
                        Gender = person.Gender,
                        Address = person.Address,
                        RecieveNewsLetters = person.RecieveNewsLetters,
                        Age = person.Dob.HasValue ? Math.Round((DateTime.Now - person.Dob.Value).TotalDays / 365.25) : null
                    };
        }
    }
}
