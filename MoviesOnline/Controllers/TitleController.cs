using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesOnline.Data;
using MoviesOnline.Models;
using MoviesOnline.ViewModels;
using System.Threading.Tasks;

namespace MoviesOnline.Controllers
{

    [ApiController]
    [Route(template: "v1")]

    public class TitleController : ControllerBase
    {

        [HttpGet]
        [Route(template: "titles")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var title = context.Users.AsNoTracking().ToListAsync();
            return Ok(title);

        }

        [HttpGet]
        [Route(template: "titles/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var title = await context.Users.AsNoTracking().
                                            FirstOrDefaultAsync(x => x.Id == id);

            if (title == null)
                return NotFound();

            return Ok(title);
        }

        [HttpPost]
        [Route(template: "titles")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
                                                    [FromBody] CreateTitleViewModel model)
        {

            if (!ModelState.IsValid)
                BadRequest();

            var title = new Title
            {
                TitleName = model.TitleName,
                Plot = model.Plot,
                ReleaseDate = model.ReleaseDate,
                
            };

            try
            {
                await context.Titles.AddAsync(title);
                await context.SaveChangesAsync();
                return Created($"v1/titles/{title.Id}", title);

            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpPut]
        [Route(template: "titles/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] AppDbContext context, 
                                                    [FromRoute] int id, 
                                                    [FromBody] UpdateTitleViewModel model)
        {

            if (!ModelState.IsValid)
                BadRequest();

            var title = await context.Titles.FirstOrDefaultAsync(x => x.Id == id);

            if (title == null)
                return NotFound();

            try
            {
                title.TitleName = model.TitleName;
                title.Plot = model.Plot;
                title.ReleaseDate = model.ReleaseDate;
                

                context.Titles.Update(title);
                await context.SaveChangesAsync();
                return Ok(title);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route(template: "titles/{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] AppDbContext context, 
                                                        [FromRoute] int id)
        {
            var title = await context.Titles.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Titles.Remove(title);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }


        }
    }
}
