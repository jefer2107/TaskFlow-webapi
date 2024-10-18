using Application.DTOs.User;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService 
: IBaseService<UserDTO, UserInputCreateDTO, UserInputUpdateDTO>
{
    Task<bool> ChangePassword(UserInputChangePasswordDTO user);
    Task<string> ForgotMyPassword(ForgotPasswordDTO model);
    Task<bool> ResetPassword(ResetPasswordDTO model);
    Task<UserDTO> FindUserWithoutChoreWithCategory(int id);
}
