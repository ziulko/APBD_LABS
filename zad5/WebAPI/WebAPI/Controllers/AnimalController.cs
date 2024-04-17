using Microsoft.EntityFrameworkCore;
using WebAPI.Classes;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly AnimalContext _context;

    public AnimalsController(AnimalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return _context.Animals.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animal = _context.Animals.Find(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    [HttpPost]
    public ActionResult<Animal> PostAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
    }

    [HttpPut("{id}")]
    public IActionResult PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest();
        }

        _context.Entry(animal).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = _context.Animals.Find(id);

        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        _context.SaveChanges();

        return NoContent();
    }
}
