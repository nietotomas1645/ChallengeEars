using ChallengeTecnico_Ears.Exceptions;
using ChallengeTecnico_Ears.IService;
using ChallengeTecnico_Ears.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChallengeTecnico_Ears.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public ActionResult<List<PersonModel>> Get()
        {
            try
            {
                var persons = _personService.GetPersonList();
                return Ok(persons);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex, "Error en la base de datos al obtener la lista de personas.");
                return StatusCode(500, "Error en la base de datos al obtener la lista de personas");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el servidor al obtener las personas.");
                return StatusCode(500, "Error en el servidor al obtener las personas.");
            }
        }
    }
}
