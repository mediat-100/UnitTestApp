using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Dto
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please input a valid email address")]
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public GenderOptions Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool RecieveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person {
                PersonId = Guid.NewGuid(),
                Name = Name, 
                Email = Email, 
                Dob = Dob, 
                Gender = Gender.ToString(), 
                CountryId = CountryId,
                Address = Address,
                RecieveNewsLetters = RecieveNewsLetters,
            };    
        }
    }
}
