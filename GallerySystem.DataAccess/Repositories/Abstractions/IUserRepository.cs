using System.Security.Claims;
using GallerySystem.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.DataAccess.Repositories.Abstractions;

public interface IUserRepository
{
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<User> FindByClaimsAsync(ClaimsPrincipal claims);
    Task<User> FindByEmailAsync(string email);
    Task<User> FindByIdAsync(string id);
    Task<User> FindByUserNameAsync(string userName);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<string> GetEmailConfirmationTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<IdentityResult> UpdateUserAsync(User user);
    Task<IdentityResult> DeleteUserAsync(User user);
    Task<string> GetResetPasswordTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
}