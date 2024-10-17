using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserService 
: IBaseService<UserDTO, UserInputCreateDTO, UserInputUpdateDTO>
{
}
