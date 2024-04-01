using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Authontication;
using Domain.DTOs.Chat;
using Domain.DTOs.Game;
using Domain.DTOs.Player;
using Domain.Entities;
using Domain.Identity;

namespace TicTacToeApi.Models.AutoMapper
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<UserSignupDTO, AppIdentityUser>();

            CreateMap<AppIdentityUser, Player>();

            CreateMap<PlayerUpdateDTO, Player>();

            CreateMap<PlayerCreateDTO, Player>();

            CreateMap<GameCreateDTO, Game>();

            CreateMap<GameUpdateDTO, Game>();

            CreateMap<BaseDTO, Guid>();

            CreateMap<ChatCreateDTO, Chat>();

            CreateMap<Chat, ChatCreateDTO>();

        }
    }
}
