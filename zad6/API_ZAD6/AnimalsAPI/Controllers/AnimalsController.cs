using API_ZAD6.Models;
using API_ZAD6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_ZAD6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalsRepository _repository;

        public AnimalsController(AnimalsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAnimals()
        {
            var animals = _repository.GetAllAnimals();
            return Ok(animals);
        }

        [HttpPost]
        public IActionResult PostAnimal(Animal animal)
        {
            _repository.AddAnimal(animal);
            return CreatedAtAction("GetAnimals", new { id = animal.Id }, animal);
        }
    }
}