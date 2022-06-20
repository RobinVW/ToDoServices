using Blazored.LocalStorage;
using ToDoFrontend.Client.Services.Base;


namespace ToDoFrontend.Client.Services
{
    public class BoardService : BaseHttpService, IBoardService
    {
        private readonly IClient client;

        public BoardService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }

        public async Task<Response<List<Board>>> GetAll()
        {
            Response<List<Board>> response;
            try
            {
                await GetBearerToken();
                var data = await client.BoardAllAsync();
                response = new Response<List<Board>>
                {
                    Data = data.ToList(),
                    Success = true,
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<Board>>(exception);
            }

            return response;
        }

        public async Task<Response<BoardDTO>> Create(BoardDTO board)
        {
            Response<BoardDTO> response = new();

            try
            {
                await GetBearerToken();
                var data = await client.BoardPOSTAsync(board);
                response = new Response<BoardDTO>
                {
                    Data = data,
                    Success = true,
                };

            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<BoardDTO>(exception);
            }

            return response;
        }

        public async Task<Response<int>> Delete(int id)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await client.BoardDELETEAsync(id); 
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<int>(exception);
            }

            return response;
        }
    }
}
