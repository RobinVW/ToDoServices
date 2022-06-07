using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using WEB4_ToDoServices.Data;
using WEB4_ToDoServices.Models;
using WEB4_ToDoServices.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly string _userId;

        public ColumnController(ToDoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            var column = await _context.Columns.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (column == null)
                return NotFound("Deze kolom bestaat niet");
            return column;
        }

        [HttpGet("{id}/cards")]
        public async Task<ActionResult<List<Card>>> GetCardsByColumn(int id)
        {
            var column = await _context.Columns.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (column == null)
                return NotFound("Deze cards bestaan niet");

            var cards = column.Cards.Where(c => c.UserId == _userId);
            if (cards == null)
                return NotFound("Deze cards bestaan niet");
            return Ok(cards);
        }

        /*[HttpGet("{id}/cards/{status}")]
        public async Task<ActionResult<List<Card>>> GetCardsByColumnWithSpecificStatus(int id,string status)
        {
            var column = await _context.Columns.FindAsync(id);
            if(column == null)
            {
                return NotFound("Deze cards bestaan niet");
            }
            var cards = column.Cards.Where(y => y.Status.Equals(status));
            if (cards == null)
                return NotFound("Deze kolom bestaat niet");
            return Ok(cards);
        }*/


        /*[HttpPost]
        public ActionResult<List<Column>> CreateColumn(Column column)
        {
            _columns.Add(column);
            return Ok(_columns);
        }*/
        [HttpPost("{id}/card")]
        public async Task<ActionResult<List<Card>>> AddCardToColumn(int id,CardDTO cardDTO)
        {
            var column = await _context.Columns.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (column == null)
            {
                return NotFound();
            }

            var card = new Card
            {
                Title = cardDTO.Title,
                ColumnId = column.Id,
                UserId = _userId,
            };

            if (cardDTO.Notes != null)
                card.Notes = cardDTO.Notes;

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return Created(nameof(card), card);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateColumn(int id, ColumnDTO columnDTO)
        {
            var column = await _context.Columns.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();

            if (column == null)
            {
                return NotFound();
            }

            column.Titel = columnDTO.Titel;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ColumnExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE api/<ColumnController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            var column = await _context.Columns.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (column == null)
                return NotFound("Deze kolom bestaat niet");
            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColumnExists(int id)
        {
            return _context.Columns.Any(e => e.Id == id && e.UserId == _userId);
        }

        private static ColumnDTO ColumnDTO(Column column) =>
            new ColumnDTO
            {
                Titel = column.Titel
            };
    }
}
