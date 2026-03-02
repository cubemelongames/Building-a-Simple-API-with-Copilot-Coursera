using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        // GET: /api/users
        [HttpGet]
        public IActionResult GetAll()
            => Ok(_repo.GetAll());

        // GET: /api/users/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var u = _repo.GetById(id);
            return u == null ? NotFound(new { error = $"User {id} not found" }) : Ok(u);
        }

        // POST: /api/users
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            // [ApiController] auto-validates DataAnnotations and returns 400 if invalid.
            var created = _repo.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: /api/users/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            var ok = _repo.Update(id, user);
            return ok ? NoContent() : NotFound(new { error = $"User {id} not found" });
        }

        // DELETE: /api/users/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var ok = _repo.Delete(id);
            return ok ? NoContent() : NotFound(new { error = $"User {id} not found" });
        }
    }
}