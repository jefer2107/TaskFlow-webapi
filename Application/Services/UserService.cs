using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) 
: IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

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
            User user = await _userRepository.FindOne(id);
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
            User user = _mapper.Map<User>(model);

            var userCreated = await _userRepository.Create(user);

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

            var userUpdated = await _userRepository.Update(id, user);

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

            var result = await _userRepository.Delete(id);
            if(!result) throw new Exception($"Error in user services method Delete");

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

}
