namespace WEB4_ToDoServices.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateChanged { get; set; }
        public int ?ColumnId { get; set; }
        public Column ?Column { get; set; }
    }
}
