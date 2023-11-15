using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicTacToeApi.BusinessLayer.Interfaces;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.DTOs.Authontication;
using TicTacToeApi.Models.DTOs.Player;
using TicTacToeApi.Models.DTOs.Services;
using TicTacToeApi.Models.Identity;

namespace TicTacToeApi.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        public IMapper mapper;

        public IEntityService<Player, PlayerCreateDTO, PlayerUpdateDTO> playerService;

        public IConfiguration Configuration { get; }

        public UserManager<AppIdentityUser> userManager { get; }

        public SignInManager<AppIdentityUser> signInManager { get; }

        public RoleManager<IdentityRole<Guid>> roleManager { get; }

        public AuthService(IConfiguration Configuration,
            UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IEntityService<Player, PlayerCreateDTO, PlayerUpdateDTO> playerService,
            IMapper mapper)
        {
            this.Configuration = Configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.playerService = playerService;
            this.mapper = mapper;

        }

        public JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSignInToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:Issuer"],
                audience: Configuration["JWT:Audience"],
                claims: authClaim,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(authSignInToken, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<AuthServiceResponseDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var user = await this.userManager.FindByEmailAsync(userLoginDTO.Email);

            if (user != null)
            {
                if (await this.userManager.CheckPasswordAsync(user, userLoginDTO.Password))
                {
                    var authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString())
                    };

                    var userRoles = await userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var token = this.GetToken(authClaim);

                    await this.signInManager.SignOutAsync();

                    var result = await this.signInManager.PasswordSignInAsync(user, userLoginDTO.Password, false, false);

                    if (result.Succeeded)
                    {
                        return new AuthServiceResponseDTO { IsSucceed = true, Message = new JwtSecurityTokenHandler().WriteToken(token) };
                    }

                    return new AuthServiceResponseDTO { IsSucceed = false, Message = "Login error" };
                }
            }

            return new AuthServiceResponseDTO { IsSucceed = false, Message = "User doesn`t exist" };
        }

        public async Task<AuthServiceResponseDTO> LogoutAsync()
        {
            await this.signInManager.SignOutAsync();

            return new AuthServiceResponseDTO { IsSucceed = true, Message = "Logged out" };
        }

        public async Task<AuthServiceResponseDTO> MeAsync(Guid userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                return new AuthServiceResponseDTO
                {
                    IsSucceed = true,
                    Message = "Your id: " + userId.ToString()
                };
            }

            return new AuthServiceResponseDTO
            {
                IsSucceed = false,
                Message = "Unauthorized"
            };
        }

        public async Task<AuthServiceResponseDTO> RegisterAsync(UserSignupDTO userSignupDTO, string role)
        {
            var isUserExist = await this.userManager.FindByEmailAsync(userSignupDTO.Email);

            if (isUserExist != null)
            {
                return new AuthServiceResponseDTO { IsSucceed = false, Message = "User already exists" };
            }

            var newUser = this.mapper.Map<AppIdentityUser>(userSignupDTO);

            if (await this.roleManager.RoleExistsAsync(role))
            {
                var result = await this.userManager.CreateAsync(newUser, userSignupDTO.Password);            
                var newDomainUser = this.mapper.Map<PlayerCreateDTO>(newUser);
                                
                if (result.Succeeded)
                {
                    this.playerService.Create(newDomainUser);
                }
                else
                {
                    var errorString = new StringBuilder("User creating failed because: ");
                    foreach (var error in result.Errors)
                    {
                        errorString.AppendLine(error.Description);
                    }

                    return new AuthServiceResponseDTO
                    {
                        IsSucceed = false,
                        Message = errorString.ToString(),
                    };
                }

                await this.userManager.AddToRoleAsync(newUser, role);

                return new AuthServiceResponseDTO { IsSucceed = true, Message = "User created successfully" };
            } 
            else
            {
                return new AuthServiceResponseDTO
                {
                    IsSucceed = false,
                    Message = "This role doesn`t exist."
                };
            }
        }
    }
}
