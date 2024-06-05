using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(bool? SoftShell = null)
        {

            List<Taco> result = dbContext.Tacos.ToList();
            if (SoftShell == true)
            { result = result.Where(t => t.SoftShell == true).ToList(); }

            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
           Taco result = dbContext.Tacos.Find(id);
            if (result == null)
            { return NotFound(); }
            else { return Ok(result); }
        }

        [HttpPost]

        public IActionResult AddTaco([FromBody] Taco result)

        {
            dbContext.Tacos.Add(result);
            dbContext.SaveChanges();
            return Created($"/api/Tacos/{result.Id}", result);
        }

        [HttpDelete]

        public IActionResult DeleteById(int id)

        {
            Taco result = dbContext.Tacos.Find(id);

            if (result == null)
            { return NotFound(); }
            else
            {
                dbContext.Remove(result);
                dbContext.SaveChanges();
                return Ok(result);
                
            }
        }
      }  
}
