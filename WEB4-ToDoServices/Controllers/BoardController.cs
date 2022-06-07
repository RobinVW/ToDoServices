using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WEB4_ToDoServices.Data;
using WEB4_ToDoServices.Models;
using WEB4_ToDoServices.Models.DTO;

namespace WEB4_ToDoServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly string _userId;

        public BoardController(ToDoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetAll()
        {
            return await _context.Boards.Where(i => i.UserId == _userId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            var board = await _context.Boards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (board == null)
                return NotFound("Dit board bestaat niet");
            return board;
        }

        [HttpGet("{id}/Columns")]
        public async Task<ActionResult<List<Column>>> GetColumnsByBoard(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null || board.UserId != _userId)
                return NotFound("Deze kolommen bestaan niet");

            var columns = board.Columns.Where(i => i.UserId == _userId);
            if (columns == null)
                return NotFound("Deze kolommen bestaat niet");
            return Ok(columns);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDTO>> CreateBoard(BoardDTO boardDTO)
        {
            var board = new Board
            {
                Name = boardDTO.Name,
                UserId = _userId,
                Columns = null
            };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        [HttpPost("{id}/Column")]
        public async Task<ActionResult<ColumnDTO>> AddColumnToBoard(int id, ColumnDTO columnDTO)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null || board.UserId != _userId)
            {
                return NotFound();
            }

            var column = new Column
            {
                Titel = columnDTO.Titel,
                UserId = _userId,
                BoardId = board.Id,
                Cards = null
            };


            _context.Columns.Add(column);
            await _context.SaveChangesAsync();

            return Created(nameof(column), _context.Columns.Find(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBoard(int id, BoardDTO boardDTO)
        {
            /*if (id != board.Id)
            {
                return BadRequest();
            }*/

            var board = await _context.Boards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();

            if (board == null)
            {
                return NotFound();
            }

            board.Name = boardDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BoardExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var board = await _context.Boards.Where(i => i.Id == id && i.UserId == _userId).FirstOrDefaultAsync();
            if (board == null)
                return NotFound("Dit board bestaat niet");
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id && e.UserId == _userId);
        }

        private static BoardDTO BoardDTO(Board board) =>
            new BoardDTO
            {
                Name = board.Name
            };
    }
}


