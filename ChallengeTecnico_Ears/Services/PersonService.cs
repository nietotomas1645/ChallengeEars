using ChallengeTecnico_Ears.Context;
using ChallengeTecnico_Ears.IService;
using ChallengeTecnico_Ears.Models;
using Microsoft.EntityFrameworkCore;
using ChallengeTecnico_Ears.Exceptions;
using Microsoft.Extensions.Logging;

namespace ChallengeTecnico_Ears.Services
{
    public class PersonService : IPersonService
    {

        private readonly ContextChallenge _context;
        private readonly ILogger<PersonService> _logger;
        public PersonService(ContextChallenge dbContext, ILogger<PersonService> logger)
        {
            _context = dbContext;
            _logger = logger;
        }


        public List<PersonModel> GetPersonList()
        {
            List<PersonModel> personList = new List<PersonModel>();

            try
            {
                personList = _context.T_Person
                    .Include(p => p.Offices)
                    .Where(p => p.Active && p.EmployeeFile > 1003)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de personas.");
                throw new DatabaseException("Error al obtener la lista de personas.", ex);
            }

            return personList;
        }
    }
}
