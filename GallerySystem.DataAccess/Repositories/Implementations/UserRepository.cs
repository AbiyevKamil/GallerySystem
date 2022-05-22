using System.Security.Claims;
using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace GallerySystem.DataAccess.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly GalleryContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(GalleryContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public virtual async Task<IdentityResult> CreateAsync(User user, string password)
        => await _userManager.CreateAsync(user, password);


    public virtual async Task<User> FindByClaimsAsync(ClaimsPrincipal claims)
        => await _userManager.GetUserAsync(claims);


    public virtual async Task<User> FindByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public virtual async Task<User> FindByIdAsync(string id)
        => await _userManager.FindByIdAsync(id);

    public virtual async Task<User> FindByUserNameAsync(string userName)
        => await _userManager.FindByNameAsync(userName);

    public virtual async Task<bool> CheckPasswordAsync(User user, string password)
        => await _userManager.CheckPasswordAsync(user, password);

    public virtual async Task<bool> IsInRoleAsync(User user, string role)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IList<string>> GetRolesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<string> GetEmailConfirmationTokenAsync(User user)
        => await _userManager.GenerateEmailConfirmationTokenAsync(user);

    public virtual async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        => await _userManager.ConfirmEmailAsync(user, token);

    public virtual async Task<IdentityResult> UpdateUserAsync(User user)
        => await _userManager.UpdateAsync(user);

    public virtual async Task<IdentityResult> DeleteUserAsync(User user)
        => await _userManager.DeleteAsync(user);

    public virtual async Task<string> GetResetPasswordTokenAsync(User user)
        => await _userManager.GeneratePasswordResetTokenAsync(user);

    public virtual async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        => await _userManager.ResetPasswordAsync(user, token, newPassword);

    public virtual async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

    public virtual async Task AddProfilePictureAsync(User user)
    {
        throw new NotImplementedException();
    }
}