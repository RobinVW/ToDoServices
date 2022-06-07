using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB4_ToDoServices.Auth.Constants;
using WEB4_ToDoServices.Data;
using WEB4_ToDoServices.Models;

namespace WEB4_ToDoServices.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {   
        private readonly ToDoContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(ToDoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("users")]
        public IQueryable<IdentityUser> GetUsers()
        {
            return _userManager.Users;
        }

        [HttpGet("boards")]
        public async Task<ActionResult<IEnumerable<Board>>> GetAll()
        {
            return await _context.Boards.ToListAsync();
        }
    }
}
