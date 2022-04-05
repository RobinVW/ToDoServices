using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB4_ToDoServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        static public List<Column> _columns = new List<Column> {
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
            },
            new Column{
                Id = 1,
                Titel = "column 1",
                BoardId = 2,
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
                    Id = 12,
                    Title = "test 12",
                    Notes = "test notes 12",
                    Status = "Done",
                    DateChanged = DateTime.Now,
                    DateCreated = DateTime.Now,
                    ColumnId = 1,
                    }
                }
            },
            new Column
            {
                Id = 2,
                Titel = "column 2",
                BoardId = 1,
                Cards = null
            }
        };

        [HttpGet("{id}")]
        public ActionResult<Column> GetColumn(int id)
        {
            var column = _columns.Find(x => x.Id == id);
            if (column == null)
                return NotFound("Deze cards bestaan niet");
            return Ok(column);
        }

        [HttpGet("{id}/cards")]
        public ActionResult<List<Card>> GetCardsByColumn(int id)
        {
            var cards = _columns.Find(x => x.Id == id).Cards;
            if (cards == null)
                return NotFound("Deze cards bestaan niet");
            return Ok(cards);
        }

        [HttpGet("{id}/cards/{status}")]
        public ActionResult<List<Card>> GetCardsByColumnWithSpecificStatus(int id,string status)
        {
            var cards = _columns.Find(x => x.Id == id).Cards.Where(y => y.Status.Equals(status));
            if (cards == null)
                return NotFound("Deze kolom bestaat niet");
            return Ok(cards);
        }


        [HttpPost]
        public ActionResult<List<Column>> CreateColumn(Column column)
        {
            _columns.Add(column);
            return Ok(_columns);
        }

        [HttpPut]
        public ActionResult<List<Column>> UpdateColumn(Column updatedColumn)
        {
            var column = _columns.Find(x => x.Id == updatedColumn.Id);
            if (column == null)
                return NotFound("Deze column bestaat niet");

            column.Titel = updatedColumn.Titel;

            return Ok(_columns);
        }


        // DELETE api/<ColumnController>/5
        [HttpDelete("column/{id}")]
        public ActionResult<List<Column>> DeleteColumn(int id)
        {
            var column = _columns.Find(x => x.Id == id);
            if (column == null)
                return NotFound("Deze collom bestaat niet");
            _columns.Remove(column);

            return Ok(_columns);
        }
    }
}
