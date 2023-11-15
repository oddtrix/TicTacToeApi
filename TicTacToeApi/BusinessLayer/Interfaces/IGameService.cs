using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.DTOs.Game;

namespace TicTacToeApi.BusinessLayer.Interfaces
{
    public interface IGameService
    {
        Game CreateGame(GameCreateDTO createDTO);
    }
}
