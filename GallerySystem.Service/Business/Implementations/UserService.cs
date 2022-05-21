using System.Security.Claims;
using GallerySystem.Core.Entities;
using GallerySystem.Service.Business.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.Service.Business.Implementations;

public class UserService : IUserService
{
    
    
    public Task<IdentityResult> CreateAsync(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByClaimsAsync(ClaimsPrincipal claims)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(User user, string role)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
    {
        throw new NotImplementedException();
    }

    public Task<IList<string>> GetRolesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetEmailConfirmationTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetResetPasswordTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task AddProfilePictureAsync(User user)
    {
        throw new NotImplementedException();
    }
}