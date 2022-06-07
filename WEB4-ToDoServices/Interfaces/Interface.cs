using WEB4_ToDoServices.Models;

namespace WEB4_ToDoServices.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<Board>> GetAllBoards();
        Task<IEnumerable<Board>> GetBoard(int id);
    }
}
