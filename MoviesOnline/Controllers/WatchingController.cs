using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesOnline.Data;
using MoviesOnline.Models;
using MoviesOnline.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesOnline.Controllers
{

    [ApiController]
    [Route(template: "v1")]
    public class WatchingController : ControllerBase
    {

        [HttpGet]
        [Route(template: "watching/{userId}")]

        public async Task<IActionResult> GetByUserIdAsync([FromServices] AppDbContext context, 
                                                    [FromRoute] int userId)
        {
            //var watching = await context.Watching.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();

            var watching = await (from w in context.Watching.AsNoTracking()
                            where w.UserId == userId
                            select w).ToListAsync(); 

            var titles = new List<Title>();

            if (watching.Count == 0)
                return NoContent();
            
            foreach (var item in watching)
            {
                var title = (from t in context.Titles.AsNoTracking()
                                   where t.Id == item.TitleId
                                   select t).FirstOrDefault();
                titles.Add(title);
            }

            return Ok(titles);
        }

        [HttpPost]
        [Route(template: "watching/{userId}")]

        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
                                                                [FromRoute] int userId,
                                                    [FromBody] Title model)
        {

            if (!ModelState.IsValid)
                BadRequest();

            var watching = new Watching
            {
                UserId = userId,
                TitleId = model.Id,
                StartDate = System.DateTime.Now
                
            };

            try
            {
                await context.Watching.AddAsync(watching);
                await context.SaveChangesAsync();
                return Created($"v1/watching/{watching.Id}", watching);

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

    } 
}
