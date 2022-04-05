using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB4_ToDoServices.Models;

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        static public List<Board> _boards = new List<Board> {
                new Board{
                    Id = 1,
                    Name = "board 1",
                    Columns = new List<Column> {
                        new Column{
                              Id = 1,
                              Titel = "column 1",
                              BoardId = 1,
                              Cards = new List<Card> {
                                 new Card{
                                    Id = 1,
                                    Title = "test",
                                    Notes = "test notes",
                                    Status = "Done",
                                    DateChanged = DateTime.Now,
                                    DateCreated = DateTime.Now,
                                    ColumnId = 1,
                                 },
                                 new Card{
                                    Id = 2,
                                    Title = "test 2",
                                    Notes = "test notes 2",
                                    Status = "Done",
                                    DateChanged = DateTime.Now,
                                    DateCreated = DateTime.Now,
                                    ColumnId = 1,
                                 }
                              }
                        }
                    }
                },
                new Board{
                    Id = 2,
                    Name = "board 2",
                    Columns = null
                },
                new Board{
                    Id = 3,
                    Name = "board 3",
                    Columns = new List<Column> {
                        new Column{
                              Id = 5,
                              Titel = "column 5",
                              BoardId = 3,
                              Cards = new List<Card> {
                                 new Card{
                                    Id = 10,
                                    Title = "test",
                                    Notes = "test notes",
                                    Status = "Done",
                                    DateChanged = DateTime.Now,
                                    DateCreated = DateTime.Now,
                                    ColumnId = 5,
                                 },
                                 new Card{
                                    Id = 11,
                                    Title = "test 11",
                                    Notes = "test notes 11",
                                    Status = "Done",
                                    DateChanged = DateTime.Now,
                                    DateCreated = DateTime.Now,
                                    ColumnId = 5,
                                 }
                              }
                        }
                    }
                }
        };
        [HttpGet]
        public ActionResult<List<Board>> GetAll()
        {    
            return _boards;
        }

        [HttpGet("{id}")]
        public ActionResult<Board> GetBoard(int id)
        {
            var board = _boards.Find(x => x.Id == id);
            if(board == null)
                return NotFound("Dit board bestaat niet");
            return Ok(board);
        }

        [HttpGet("{id}/columns")]
        public ActionResult<List<Column>> GetColumnsByBoard(int id)
        {
            var columns = _boards.Find(x => x.Id == id).Columns;
            if (columns == null)
                return NotFound("Deze kolom bestaat niet");
            return Ok(columns);
        }

        [HttpPost]
        public ActionResult<List<Board>> CreateBoard(Board board)
        {
            _boards.Add(board);
            return Ok(_boards);
        }
        
        [HttpPut]
        public ActionResult<List<Board>> UpdateBoard(Board updatedBoard)
        {
            var board = _boards.Find(x => x.Id == updatedBoard.Id);
            if (board == null)
                return NotFound("Dit board bestaat niet");

            board.Name = updatedBoard.Name;

            return Ok(_boards);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Board>> DeleteBoard(int id)
        {
            var board = _boards.Find(x => x.Id == id);
            if(board == null)
                return NotFound("Dit board bestaat niet");
            _boards.Remove(board);

            return Ok(_boards);
        }
    }
}


