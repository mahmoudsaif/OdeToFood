using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Resturants
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantsController : ControllerBase
    {
        private readonly OdeToFoodContext _context;

        public ResturantsController(OdeToFoodContext context)
        {
            _context = context;
        }

        // GET: api/Resturants
        [HttpGet]
        public IEnumerable<Resturant> GetResturants()
        {
            return _context.Resturants;
        }

        // GET: api/Resturants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResturant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resturant = await _context.Resturants.FindAsync(id);

            if (resturant == null)
            {
                return NotFound();
            }

            return Ok(resturant);
        }

        // PUT: api/Resturants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResturant([FromRoute] int id, [FromBody] Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resturant.Id)
            {
                return BadRequest();
            }

            _context.Entry(resturant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResturantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Resturants
        [HttpPost]
        public async Task<IActionResult> PostResturant([FromBody] Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Resturants.Add(resturant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResturant", new { id = resturant.Id }, resturant);
        }

        // DELETE: api/Resturants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResturant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resturant = await _context.Resturants.FindAsync(id);
            if (resturant == null)
            {
                return NotFound();
            }

            _context.Resturants.Remove(resturant);
            await _context.SaveChangesAsync();

            return Ok(resturant);
        }

        private bool ResturantExists(int id)
        {
            return _context.Resturants.Any(e => e.Id == id);
        }
    }
}