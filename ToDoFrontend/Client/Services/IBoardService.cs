using ToDoFrontend.Client.Services.Base;

namespace ToDoFrontend.Client.Services
{
    public interface IBoardService
    {
        Task<Response<List<Board>>> GetAll();
        Task<Response<BoardDTO>> Create(BoardDTO board);
        Task<Response<int>> Delete(int id);
    }
}
