using AutoMapper;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.DTOs.Authontication;
using TicTacToeApi.Models.DTOs.Game;
using TicTacToeApi.Models.DTOs.Player;
using TicTacToeApi.Models.Identity;

namespace TicTacToeApi.Models.AutoMapper
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<UserSignupDTO, AppIdentityUser>();

            CreateMap<AppIdentityUser, PlayerCreateDTO>();

            CreateMap<PlayerUpdateDTO, Player>();

            CreateMap<PlayerCreateDTO, Player>();

            CreateMap<GameCreateDTO, Game>();
        }
    }
}
