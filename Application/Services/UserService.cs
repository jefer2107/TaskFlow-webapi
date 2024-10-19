using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService(
    IUserRepository userRepository, 
    IMapper mapper, 
    IUnityOfWork uof,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager
    ) 
: IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _uof = uof;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<IEnumerable<UserDTO>> FindAll()
    {
        try
        {
            IEnumerable<User> models = await _userRepository.FindAll();

            return _mapper.Map<IEnumerable<UserDTO>>(models);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method FindAll: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method FindAll: {error.Message}"
            );
        }
    }

    public async Task<UserDTO?> FindOne(int id)
    {
        try
        {
            User user = await _userRepository.FindOneWithChores(id);
            return user != null ? _mapper.Map<UserDTO>(user): null;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<UserDTO> Create(UserInputCreateDTO model)
    {
        try
        {
            var user = _mapper.Map<User>(model);

            user.Password = "********";

            var userCreated = await _userRepository.Create(user);

            IdentityUser identityUser = new()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var identityUserCreated = await _userManager
            .CreateAsync(identityUser, model.Password);

            if(!identityUserCreated.Succeeded)
                throw new Exception($"Error in user services method Create");

            await _signInManager.SignInAsync(identityUser, false);

            await _uof.Commit();

            return _mapper.Map<UserDTO>(userCreated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method Create: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method Create: {error.Message}"
            );
        }
    }

    public async Task<UserDTO> Update(int id, UserInputUpdateDTO model)
    {
        try
        {
            var user = _mapper.Map<User>(model);

            var userById = await FindOne(id) ??
            throw new Exception($"Error in user services method Update: user id:{id} not found");

            var identityUserByEmail = await _userManager
            .FindByEmailAsync(userById.Email) ?? 
            throw new Exception($"Error in user services method Update: identity user not found");

            var identityUserById = await _userManager.FindByIdAsync(identityUserByEmail.Id) ??
            throw new Exception($"Error in user services method Update: identity user not found");

            if(model.Password != null)
            {
                throw new Exception(
                    $"Error in user services method Update: You cannot change password using this method."
                );
            }

            if(model.Email != null)
            {
                var identityUserEmailUpdated = await _userManager
                .SetEmailAsync(identityUserById, model.Email);

                var usernameUpdateResult = await _userManager
                .SetUserNameAsync(identityUserById, model.Email);

                if(!identityUserEmailUpdated.Succeeded) 
                    throw new Exception(
                        $"Error in user services method Update: identity user not Email updated"
                    );

                if(!usernameUpdateResult.Succeeded) 
                    throw new Exception(
                        $"Error in user services method Update: identity user not UserName updated"
                    );
            }

            user.UpdatedAt = DateTime.UtcNow;

            var userUpdated = await _userRepository.Update(id, user);
            await _uof.Commit();

            return _mapper.Map<UserDTO>(userUpdated);
        }
        catch (Exception error)
        {
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method Update: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method Update: {error.Message}"
            );
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            UserDTO user = await FindOne(id) ?? 
            throw new Exception($"Error in user services method Delete: ID:{id} não encontrado");

            var userIdentity = await _userManager.FindByEmailAsync(user.Email) ??
            throw new Exception($"Error in user services method Delete: Email:{user.Email} não encontrado");

            var resultUserIdentityDelete = await _userManager.DeleteAsync(userIdentity);
            if(!resultUserIdentityDelete.Succeeded) throw new Exception($"Error in user services method Delete");

            var result = await _userRepository.Delete(id);
            if(!result) throw new Exception($"Error in user services method Delete");

            await _uof.Commit();

            return result;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method Delete: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method Delete: {error.Message}"
            );
        }
    }

    public async Task<bool> ChangePassword(UserInputChangePasswordDTO user)
    {
        try
        {
            User userByEmail = await _userRepository.FindByEmail(user.Email) ??
            throw new Exception(
                $"Error in user services method ChangePassword: Email:{user.Email} not found"
            );

            var userIdentityByEmail = await _userManager.FindByEmailAsync(user.Email) ??
            throw new Exception(
                $"Error in user services method ChangePassword: Email:{user.Email} not found"
            );

            var result = await _userManager
            .CheckPasswordAsync(userIdentityByEmail, user.CurrentPassword);

            if(!result)
            {
                Console.WriteLine(
                    $"Error in user services method ChangePassword: Password:{user.CurrentPassword} not found"
                );
                throw new Exception(
                    $"Password:{user.CurrentPassword} not found"
                );
            }


            var passwordUpdatedResult = await _userManager
            .ChangePasswordAsync(userIdentityByEmail, user.CurrentPassword, user.NewPassword);

            if(!passwordUpdatedResult.Succeeded)
            throw new Exception(
                $"Error in user services method ChangePassword"
            );

            return true; 
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method ChangePassword: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method ChangePassword: {error.Message}"
            );
        }
    }

    public async Task<string> ForgotMyPassword(ForgotPasswordDTO model)
    {
        try
        {
            var userIdentity = await _userManager.FindByEmailAsync(model.Email) ??
            throw new Exception(
                $"Error in user services method ForgotMyPassword. User identity:{model.Email} not found"
            );

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);
            var resetPasswordUrl = $"{model.Domain}?token={resetToken}&email={model.Email}";

            return resetPasswordUrl;

        }
        catch (Exception error)
        {
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method ForgotMyPassword: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method ForgotMyPassword: {error.Message}"
            );
            
        }
    }

    public async Task<bool> ResetPassword(ResetPasswordDTO model)
    {
        try
        {
            var userIdentity = await _userManager.FindByEmailAsync(model.Email) ??
            throw new Exception(
                $"Error in user services method ResetPassword. User identity:{model.Email} not found"
            );

            var passwordReset = await _userManager
            .ResetPasswordAsync(userIdentity, model.Token, model.newPassword);

            return !!passwordReset.Succeeded;

        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method ResetPassword: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method ResetPassword: {error.Message}"
            );
        }
    }

    public async Task<UserDTO> FindUserWithoutChoreWithCategory(int id)
    {
        try
        {
            UserDTO user = await FindOne(id);

            user.Chores = user.Chores.Where(x => x.CategoryId == null).ToList();

            return user;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in user services method FindUserWithoutCategory: {error.InnerException}"
                );

            throw new Exception(
                $"Error in user services method FindUserWithoutCategory: {error.Message}"
            );
        }
    }

}
