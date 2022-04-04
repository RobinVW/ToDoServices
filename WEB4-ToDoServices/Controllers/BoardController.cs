using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB4_ToDoServices.Models;

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private List<Board> _boards = new List<Board> {
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
        public async Task<ActionResult<List<Board>>> GetAll()
        {
      
            return _boards;
        }
        



    }
}


