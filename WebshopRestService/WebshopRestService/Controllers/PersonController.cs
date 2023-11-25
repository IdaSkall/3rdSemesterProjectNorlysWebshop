﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebshopRestService.BusinessLogicLayer;
using WebshopRestService.DTOs;

namespace WebshopRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonData _personDataControl;
        private readonly IConfiguration _configuration;

        //Constructor with Dependency Injection
        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
            _personDataControl = new PersonDataControl(_configuration);
        }

        [HttpGet, Route("api/person")]
        public ActionResult<List<PersonDTO>>? Get()
        {
            ActionResult<List<PersonDTO>> foundReturn;
            //Retrieve data converted to DTO
            List<PersonDTO>? foundPersons = _personDataControl.Get();
            //evaluate
            if (foundPersons != null)
            {
                if (foundPersons.Count > 0)
                {
                    foundReturn = Ok(foundPersons); //OK found list statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204); //OK found list but no content 204
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500); //Internal server error   
            }
            return foundReturn; //send return to client
        }

        [HttpGet, Route("api/person/{personId}")]
        public ActionResult<PersonDTO> GetPersonById(int personId)
        {
            ActionResult<PersonDTO> foundReturn;
            try
            {
                //Retieve data converted to DTO
                PersonDTO? foundPersonsById = _personDataControl.GetPersonById(personId);

                //Evaluate
                if (foundPersonsById != null)
                {
                    foundReturn = Ok(foundPersonsById); //OK found person by ID statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204); // OK not found, no content statuscode 204
                }
            }
            catch
            {
                foundReturn = new StatusCodeResult(500); // Internal server error   
            }
            return foundReturn; // Send return to client
        }

        // URL: api/person
        [HttpPost, Route("api/person")]
        public ActionResult<int> PostNewPerson(PersonDTO personDTO)
        {
            ActionResult<int> foundReturn;
            int insertedId = -1;
            if (personDTO != null)
            {
                insertedId = _personDataControl.Add(personDTO);
            }
            // Evaluate
            if (insertedId > 0)
            {
                foundReturn = Ok(insertedId);
            }
            else if (insertedId == 0)
            {
                foundReturn = BadRequest();     // missing input
            }
            else
            {
                foundReturn = new StatusCodeResult(500);    // Internal server error
            }
            return foundReturn;
        }

        //JEG ER IKKE SIKKER PÅ DE FØLGENDE METODER GRRRRRRRRR

        [HttpDelete, Route("api/person")]
        public ActionResult Delete(int personId)
        {
            ActionResult foundReturn;
            bool wasOk = _personDataControl.Delete(personId);
            if (wasOk)
            {
                foundReturn = Ok();
            }
            else
            {
                foundReturn = new StatusCodeResult(500);    // Internal server error
            }
            return foundReturn;
        }

        [HttpPut, Route("api/person")]
        public ActionResult<bool> Put(PersonDTO personDTO)
        {
            ActionResult foundReturn;

            WebshopModel.ModelLayer.Person? person = ModelConversion.PersonDTOConversion.ToPerson(personDTO);

            if (personDTO != null)
            {
                bool wasOk = _personDataControl.Put(personDTO);

                if (wasOk)
                {
                    foundReturn = Ok();
                }
                else
                {
                    foundReturn = new StatusCodeResult(500);
                }
            }
            else
            {
                foundReturn = BadRequest();
            }

            return foundReturn;
        }
    
        [HttpGet, Route("api/person/{userId")]
        public ActionResult<PersonDTO?> GetPersonByUserId(string userId)
        {
            ActionResult<PersonDTO?> foundReturn;

            // Retrieve and convert data
            PersonDTO? foundPerson = _personDataControl.GetPersonByUserId(userId);

            // Evaluate
            if (foundPerson != null)
            {
                if (!String.IsNullOrEmpty(foundPerson.UserId))
                {
                    foundReturn = Ok(foundPerson);                 // Found - Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content - Statuscode 204
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error - Statuscode 500
            }

            // send response back to client
            return foundReturn;
        }

    }

}
