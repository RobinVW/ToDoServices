namespace WEB4_ToDoServices.Models
{
    public class Column
    {
        #region Properties
        public int Id { get; set; }
        public string Titel { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<Card> Cards { get; set; }  = new List<Card>();      
        #endregion

    }
}
