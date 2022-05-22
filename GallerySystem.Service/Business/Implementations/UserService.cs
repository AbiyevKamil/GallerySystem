using System.Security.Claims;
using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.Service.Business.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GallerySystem.Service.Business.Implementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMailService _mailService;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mailService = mailService;
    }

    public virtual async Task<IdentityResult> CreateAsync(User user, string password)
        => await _unitOfWork.Users.CreateAsync(user, password);


    public virtual async Task<User> FindByClaimsAsync(ClaimsPrincipal claims)
        => await _unitOfWork.Users.FindByClaimsAsync(claims);

    public virtual async Task<User> FindByEmailAsync(string email)
        => await _unitOfWork.Users.FindByEmailAsync(email);

    public virtual async Task<User> FindByUserNameAsync(string userName)
        => await _unitOfWork.Users.FindByUserNameAsync(userName);

    public virtual async Task<User> FindByIdAsync(string id)
        => await _unitOfWork.Users.FindByIdAsync(id);

    public virtual async Task<bool> CheckPasswordAsync(User user, string password)
        => await _unitOfWork.Users.CheckPasswordAsync(user, password);

    public virtual async Task<bool> IsInRoleAsync(User user, string role)
        => await _unitOfWork.Users.IsInRoleAsync(user, role);

    public virtual async Task<IdentityResult> AddToRoleAsync(User user, string role)
        => await _unitOfWork.Users.AddToRoleAsync(user, role);

    public virtual async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        => await _unitOfWork.Users.AddToRolesAsync(user, roles);

    public virtual async Task<IList<string>> GetRolesAsync(User user)
        => await _unitOfWork.Users.GetRolesAsync(user);

    public virtual async Task<string> GetEmailConfirmationTokenAsync(User user)
        => await _unitOfWork.Users.GetEmailConfirmationTokenAsync(user);

    public virtual async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        => await _unitOfWork.Users.ConfirmEmailAsync(user, token);

    public virtual async Task<IdentityResult> UpdateUserAsync(User user)
        => await _unitOfWork.Users.UpdateUserAsync(user);

    public virtual async Task<IdentityResult> DeleteUserAsync(User user)
        => await _unitOfWork.Users.DeleteUserAsync(user);

    public virtual async Task<string> GetResetPasswordTokenAsync(User user)
        => await _unitOfWork.Users.GetResetPasswordTokenAsync(user);

    public virtual async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        => await _unitOfWork.Users.ResetPasswordAsync(user, token, newPassword);

    public virtual async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        => await _unitOfWork.Users.ChangePasswordAsync(user, currentPassword, newPassword);

    public virtual async Task AddProfilePictureAsync(User user)
        => await _unitOfWork.Users.AddProfilePictureAsync(user);

    public bool SendEmailConfirmationLinkAsync(string email, string url)
    {
        string msg = $"Your email confirmation link: <a href=\"{url}\">Click here</a>";
        return _mailService.SendEmail(msg, email);
    }
}