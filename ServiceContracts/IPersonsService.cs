using ServiceContracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        PersonResponse AddPerson(PersonAddRequest? request);
        List<PersonResponse> GetAllPersons();
        PersonResponse? GetPerson(Guid? personId);
    }
}
