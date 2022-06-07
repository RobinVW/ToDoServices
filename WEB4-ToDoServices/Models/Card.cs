using WEB4_ToDoServices.Models.Base;

namespace WEB4_ToDoServices.Models
{
    public class Card : Auditable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ?Notes { get; set; }
        public string UserId { get; set; }
        public int ColumnId { get; set; }
    }
}
