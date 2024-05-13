using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase{

    private static readonly List<User> _users = new List<User>
    {
        new User {
            Id = 1,
            Username = "johndoe",
            Email = "John Doe",
            Fullname = "John Doe",
        },
        new User {
            Id = 2,
            Username = "janedoe",
            Email = "Jane Doe",
            Fullname = "Jane Doe1",
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Fullname = user.Fullname;

        return Ok(existingUser);
    }

    [HttpDelete("{id}")]

    public ActionResult DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }
    
}