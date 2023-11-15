using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicTacToeApi.Models.DTOs.Authontication;
using TicTacToeApi.Models.DTOs.Services;

namespace TicTacToeApi.BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDTO> LoginAsync(UserLoginDTO userLoginDTO);

        Task<AuthServiceResponseDTO> RegisterAsync(UserSignupDTO userSignupDTO, string role);

        Task<AuthServiceResponseDTO> LogoutAsync();

        Task<AuthServiceResponseDTO> MeAsync(Guid userId);

        JwtSecurityToken GetToken(List<Claim> authClaim);
    }
}
