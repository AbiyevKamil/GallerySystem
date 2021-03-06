using System.Security.Claims;
using GallerySystem.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.Service.Business.Data.Abstractions;

public interface IUserService
{
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<User> FindByClaimsAsync(ClaimsPrincipal claims);
    Task<User> FindByEmailAsync(string email);
    Task<User> FindByUserNameAsync(string userName);
    Task<User> FindByIdAsync(string id);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<string> GetEmailConfirmationTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    Task<IdentityResult> UpdateUserAsync(User user);
    Task<IdentityResult> DeleteUserAsync(User user);
    Task<string> GetResetPasswordTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<IdentityResult> AddProfilePictureAsync(User user, IFormFile file);
    bool SendEmailConfirmationLink(string email, string url);
    bool SendResetPasswordLink(string email, string url);
}