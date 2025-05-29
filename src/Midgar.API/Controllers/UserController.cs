using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midgar.Domain.Entities;
using Midgar.Persistence.Interfaces;

namespace Midgar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            return user is null ? NotFound() : Ok(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != user.Id) 
                return BadRequest();

            var existingUser = await _repository.GetByIdAsync(id);
            
            if (existingUser == null)
                return NotFound($"User with ID {id} not found.");
            
            await _repository.UpdateAsync(user);
           
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _repository.GetByIdAsync(id);

            if (existingUser == null)
                return NotFound($"User with ID {id} not found.");
                        
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}