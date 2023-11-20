using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.DTOs.Authontication;
using Domain.DTOs.Services;

namespace ApplicationCore.Interfaces
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
