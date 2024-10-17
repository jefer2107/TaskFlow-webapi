using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserService 
: IBaseService<UserDTO, UserInputCreateDTO, UserInputUpdateDTO>
{
    Task<bool> ChangePassword(UserInputChangePasswordDTO user);
    Task<string> ForgotMyPassword(ForgotPasswordDTO model);
    Task<bool> ResetPassword(ResetPasswordDTO model);
}
