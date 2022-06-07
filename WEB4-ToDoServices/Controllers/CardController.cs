using Microsoft.AspNetCore.Mvc;
using WEB4_ToDoServices.Models;
using Microsoft.AspNetCore.Http;
using WEB4_ToDoServices.Data;
using WEB4_ToDoServices.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WEB4_ToDoServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ToDoContext _context;
        private readonly string _userId;

        public CardController(ToDoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (card == null)
                return NotFound("Deze card bestaat niet");
            return Ok(card);    
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateCard(int id, CardDTO cardDTO)
        {
            var card = await _context.Cards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (card == null)
                return NotFound("Deze card bestaat niet");

            card.Title = cardDTO.Title;
            card.ColumnId = card.ColumnId;
            card.Notes = card.Notes;

            if (cardDTO.Notes != null)
                card.Notes = cardDTO.Notes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CardExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /*
        [HttpPost]
        public ActionResult<List<Card>> CreateCard(Card card)
        {
            _cards.Add(card);
            return Ok(_cards);
        }
        */

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (card == null)
                return NotFound("Deze card bestaat niet");
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id && e.UserId == _userId);
        }
    }
}
