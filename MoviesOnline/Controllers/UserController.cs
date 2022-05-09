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
    public class UserController : ControllerBase
    {   
        [HttpGet]
        [Route(template:"users")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context) 
        {
            var users = context.Users.AsNoTracking().ToListAsync();
            return Ok(users);
        
        } 

        [HttpGet]
        [Route(template: "users/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context,[FromRoute] int id)
        {
            var user = await context.Users.AsNoTracking().
                                            FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route(template:"users")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, 
                                                    [FromBody] CreateUserViewModel model)
        {

            if (!ModelState.IsValid)    
                BadRequest();

            var user = new User
            {
                UserName = model.UserName,
                UserMiddleName = model.UserMiddleName,
                Email = model.Email,
                Password = model.Password

            };

            try
            {   
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return Created($"v1/users/{user.Id}", user);

            }
            catch (System.Exception)
            {

                return BadRequest();
            }
           
        }

        [HttpPut]
        [Route(template:"users/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] AppDbContext context, [FromRoute] int id, [FromBody] UpdateUserViewModel model )
        {

            if (!ModelState.IsValid)
                BadRequest();

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            try
            {
                user.UserName = model.UserName;
                user.Password = model.Password;
                user.Email = model.Email;
                user.UserMiddleName = model.UserMiddleName;

                context.Users.Update(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpDelete]
        [Route(template:"users/{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] AppDbContext context, [FromRoute] int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id==id);

            try
            {
                context.Users.Remove(user);
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
