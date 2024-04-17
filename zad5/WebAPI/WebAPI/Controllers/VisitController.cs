using Microsoft.AspNetCore.Mvc;
using WebAPI.Classes;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitsController : ControllerBase
{
    private readonly AnimalContext _context;

    public VisitsController(AnimalContext context)
    {
        _context = context;
    }

    [HttpGet("animal/{animalId}")]
    public ActionResult<IEnumerable<Visit>> GetVisitsByAnimal(int animalId)
    {
        var visits = _context.Visits.Where(v => v.AnimalId == animalId).ToList();
        return visits;
    }

    [HttpPost]
    public ActionResult<Visit> PostVisit(Visit visit)
    {
        _context.Visits.Add(visit);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetVisitsByAnimal), new { animalId = visit.AnimalId }, visit);
    }
}