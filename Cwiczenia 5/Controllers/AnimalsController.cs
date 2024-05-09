

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Cwiczenia_5.Services;

namespace Cwiczenia_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalDbService _animalDbService;

        public AnimalsController(IAnimalDbService animalDbService)
        {
            _animalDbService = animalDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string orderBy)
        {
            var result = _animalDbService.GetAnimals(orderBy);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal animal)
        {
            var result = _animalDbService.AddAnimal(animal);

            if (result == 1)
            {
                return Ok($"Animal was added to db, added values name: {animal.Name}, description {animal.Description}, category {animal.Category}, area: {animal.Area}");

            }

            return NotFound($"Something went wrong when adding animal with id {animal.IdAnimal} to db");
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal(Animal animal, int idAnimal)
        {
            var result = _animalDbService.UpdateAnimal(animal, idAnimal);
            if (result == 1)
            {
                return Ok("Animal updated");
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when updating this animal");
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            var result = _animalDbService.DeleteAnimal(idAnimal);

            if (result == 1)
            {
                return Ok($"Animal with id {idAnimal} was deleted");

            }

            return StatusCode((int)HttpStatusCode.InternalServerError, $"Something went wrong when deleting animal with id {idAnimal} from database");
        }
    }
}
