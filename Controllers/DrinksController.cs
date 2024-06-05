using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TacoLab.Models;

namespace TacoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContextD = new FastFoodTacoDbContext();

        [HttpGet]

        public IActionResult GetAllDrinks(string? SortByCost)
        {
            {
                List<Drink> result = dbContextD.Drinks.ToList();


                if (SortByCost == "descending")
                { result = result.OrderByDescending(dr => dr.Cost).ToList(); }


                else if (SortByCost == "ascending")
                {
                    result = result.OrderBy(dr => dr.Cost).ToList();
                }


                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDrinks(int id)
        {
            Drink result = dbContextD.Drinks.Find(id);

            if (result == null)
            { return NotFound(); }
            else { return Ok(result); }
         }


        [HttpPost]

        public IActionResult PostDrink([FromBody] Drink drink)
        {
            dbContextD.Drinks.Add(drink);
            dbContextD.SaveChanges();
            return Created($"/api/Drinks/{drink.Id}",drink);

        }


        [HttpPut]

        public IActionResult PutDrinks([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id) { return NoContent(); }
            if (!dbContextD.Drinks.Any(d => d.Id == id)) { return NotFound(); }

            dbContextD.Drinks.Update(targetDrink);
            dbContextD.SaveChanges();
            return NoContent();



        }
    }
}
