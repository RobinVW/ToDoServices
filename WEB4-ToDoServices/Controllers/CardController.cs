using Microsoft.AspNetCore.Mvc;
using WEB4_ToDoServices.Models;
using Microsoft.AspNetCore.Http;

namespace WEB4_ToDoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        static public List<Card> _cards = new List<Card>
        {
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
        };

        [HttpGet("{id}")]
        public ActionResult<Card> GetCard(int id)
        {
            var card = _cards.Find(x => x.Id == id);
            if(card == null)
                return NotFound("Deze card bestaat niet");
            return Ok(card);    
        }

        [HttpPut]
        public ActionResult<List<Card>> UpdateCard(Card updatedCard)
        {
            var card = _cards.Find(x => x.Id == updatedCard.Id);
            if (card == null)
                return NotFound("Deze card bestaat niet");
            card.Title = updatedCard.Title;
            card.Notes = updatedCard.Notes;
            card.Status = updatedCard.Status;
            card.DateChanged = DateTime.Now;
            card.ColumnId = updatedCard.ColumnId;

            return Ok(_cards);
        }


        [HttpPost]
        public ActionResult<List<Card>> CreateCard(Card card)
        {
            _cards.Add(card);
            return Ok(_cards);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Card>> DeleteCard(int id)
        {
            var card = _cards.Find(x => x.Id == id);
            if (card == null)
                return NotFound("Deze card bestaat niet");
            _cards.Remove(card);

            return Ok(_cards);
        }
    }
}
