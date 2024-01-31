using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NM.Core.Database.Models; 
using NM.Core.Database.Services; 

namespace NM.Core.Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(id, cancellationToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var createdUser = await _userService.CreateAsync(user, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateAsync(int id, User user, CancellationToken cancellationToken)
        {
            var updatedUser = await _userService.UpdateAsync(id, user, cancellationToken);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var deletedUser = await _userService.DeleteAsync(id, cancellationToken);

            if (deletedUser == null)
            {
                return NotFound();
            }

            return Ok(deletedUser);
        }
    }
}
