using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.DTOs.Authontication;
using Domain.DTOs.Services;
using Domain.Entities;
using Domain.Identity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper Mapper;

        private readonly IUnitOfWork unitOfWork;

        public IConfiguration Configuration { get; }

        public UserManager<AppIdentityUser> UserManager { get; }

        public SignInManager<AppIdentityUser> SignInManager { get; }

        public RoleManager<IdentityRole<Guid>> RoleManager { get; }

        public AuthService(IConfiguration Configuration,
            UserManager<AppIdentityUser> UserManager,
            SignInManager<AppIdentityUser> SignInManager,
            RoleManager<IdentityRole<Guid>> RoleManager,
            IUnitOfWork unitOfWork,
            IMapper Mapper)
        {
            this.Configuration = Configuration;
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;

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
            var user = await UserManager.FindByEmailAsync(userLoginDTO.Email);

            if (user != null)
            {
                if (await UserManager.CheckPasswordAsync(user, userLoginDTO.Password))
                {
                    var authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName.ToString())
                    };

                    var userRoles = await UserManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var token = GetToken(authClaim);

                    await SignInManager.SignOutAsync();

                    var result = await SignInManager.PasswordSignInAsync(user, userLoginDTO.Password, false, false);

                    if (result.Succeeded)
                    {
                        return new AuthServiceResponseDTO
                        {
                            IsSucceed = true,
                            Message = new JwtSecurityTokenHandler().WriteToken(token)
                        };
                    }

                    return new AuthServiceResponseDTO { IsSucceed = false, Message = "Login error" };
                }
            }

            return new AuthServiceResponseDTO { IsSucceed = false, Message = "User doesn`t exist" };
        }

        public async Task<AuthServiceResponseDTO> LogoutAsync()
        {
            await SignInManager.SignOutAsync();

            return new AuthServiceResponseDTO { IsSucceed = true, Message = "Logged out" };
        }

        public async Task<AuthServiceResponseDTO> MeAsync(Guid userId)
        {
            var user = await UserManager.FindByIdAsync(userId.ToString());

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
            var isUserExist = await UserManager.FindByEmailAsync(userSignupDTO.Email);

            if (isUserExist != null)
            {
                return new AuthServiceResponseDTO { IsSucceed = false, Message = "User already exists" };
            }

            var newUser = Mapper.Map<AppIdentityUser>(userSignupDTO);

            if (await RoleManager.RoleExistsAsync(role))
            {
                var result = await UserManager.CreateAsync(newUser, userSignupDTO.Password);
                var newDomainUser = Mapper.Map<Player>(newUser);

                if (result.Succeeded)
                {
                    this.unitOfWork.PlayerRepository.Create(newDomainUser);
                    this.unitOfWork.Save();
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

                await UserManager.AddToRoleAsync(newUser, role);

                return new AuthServiceResponseDTO
                {
                    IsSucceed = true,
                    Message = "User created successfully"
                };
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
