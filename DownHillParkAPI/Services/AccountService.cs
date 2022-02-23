using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DownHillParkAPI.Services
{
    public interface IAccountService
    {
        Task<User> RegisterAsync(RegisterUser item);
        Task<User> LoginAsync(LoginUser item);
        Task LogoutAsync();
        Task<IdentityResult> ChangeSelfPasswordAsync(ChangePassword item);
        Task<ClaimsIdentity> GetIdentityAsync(string username, string password);
    }
    public class AccountService : IAccountService
    {
        public AccountService(UserManager<User> userManager,SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public async Task<User> RegisterAsync(RegisterUser item)
        {
            var user = _mapper.Map<RegisterUser,User>(item);
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, item.Password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(user.Email);

                await _userManager.AddToRolesAsync(newUser, new[] { "User" });
                return newUser;
            }
            if (result.Succeeded == false)
            {
                var errors = result.Errors;
                string errorString = null;
                foreach (IdentityError error in errors)
                {
                    errorString += error.Code + "\n" + error.Description + "\n";
                }
                throw new CustomException(errorString);
            }
            return null;
        }

        public async Task<User> LoginAsync(LoginUser item)
        {
            var result = await _signInManager.PasswordSignInAsync(item.Email, item.Password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(item.Email);
                await _signInManager.SignInAsync(user, false);
                if (user != null)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangeSelfPasswordAsync(ChangePassword item)
        {
            var user = await _userManager.FindByEmailAsync(item.Email);
            return await _userManager.ChangePasswordAsync(user, item.CurrentPasword, item.NewPassword);
            
        }

        public async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.UserName == username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (user != null && result == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, (await _userManager.GetRolesAsync(user)).First())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
