namespace WEB4_ToDoServices.Models
{
    public class Board
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Column> ?Columns { get; set; }
        #endregion
    }
}
