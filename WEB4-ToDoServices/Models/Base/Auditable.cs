namespace WEB4_ToDoServices.Models.Base
{
    public abstract class Auditable
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
    }
}
