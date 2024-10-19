using Application.DTOs.Chore;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class ChoreService(
    IChoreRepository choreRepository,
    IMapper mapper,
    IUnityOfWork uof
) 
: IChoreService
{
    private readonly IChoreRepository _choreRepository = choreRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _uof = uof;

    public async Task<IEnumerable<ChoreDTO>> FindAll()
    {
        try
        {
            IEnumerable<Chore> chores = await _choreRepository.FindAll();
            return _mapper.Map<IEnumerable<ChoreDTO>>(chores);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindAll: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindAll: {error.Message}"
            );
        }
    }

    public async Task<ChoreDTO?> FindOne(int id)
    {
        try
        {
            Chore chore = await _choreRepository.FindOne(id);
            return _mapper.Map<ChoreDTO>(chore);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<ChoreDTO> Create(ChoreInputCreateDTO model)
    {
        try
        {
            Chore chore = _mapper.Map<Chore>(model);

            var choreCreated = await _choreRepository.Create(chore);

            await _uof.Commit();

            return _mapper.Map<ChoreDTO>(choreCreated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method Create: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method Create: {error.Message}"
            );
        }
    }

    public async Task<ChoreDTO> Update(int id, ChoreInputUpdateDTO model)
    {
        try
        {
            var choreById = await FindOne(id) ?? 
            throw new Exception($"Error in chore services method Update: user id:{id} not found");

            Chore chore = _mapper.Map<Chore>(model);

            Chore choreUpdated = await _choreRepository.Update(id, chore);

            await _uof.Commit();

            return _mapper.Map<ChoreDTO>(choreUpdated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var choreById = await FindOne(id) ?? 
            throw new Exception($"Error in chore services method Delete: user id:{id} not found");


            bool choreDeleted = await _choreRepository.Delete(id);

            await _uof.Commit();

            return choreDeleted;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<IEnumerable<ChoreDTO>> FindAllByUser(int userId)
    {
        try
        {
            IEnumerable<ChoreDTO> chores = await FindAll();

            IEnumerable<ChoreDTO> newChores = chores.Where(x => x.UserId == userId).ToList();

            return newChores;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindAllByUser: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindAllByUser: {error.Message}"
            );
        }
    }

    public async Task<ChoreDTO> FindOneByUser(int id, int userId)
    {
        try
        {
            Chore chore = await _choreRepository.FindOneByUser(id, userId);
            return _mapper.Map<ChoreDTO>(chore);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOneByUser: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOneByUser: {error.Message}"
            );
        }
    }

    public async Task<bool> DeleteByUser(int id, int userId)
    {
        try
        {
            var choreById = await FindOneByUser(id, userId) ?? 
            throw new Exception(
                $"Error in chore services method Delete: user id:{id} or userId:{userId} not found"
            );


            bool choreDeleted = await _choreRepository.Delete(id);

            await _uof.Commit();

            return choreDeleted;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<ChoreDTO> UpdateByUser(int id, int userId, ChoreInputUpdateDTO model)
    {
        try
        {
            var choreById = await FindOneByUser(id, userId) ?? 
            throw new Exception(
                $"Error in chore services method Delete: user id:{id} or userId:{userId} not found"
            );

            Chore chore = _mapper.Map<Chore>(model);

            Chore choreUpdated = await _choreRepository.Update(id, chore);

            await _uof.Commit();

            return _mapper.Map<ChoreDTO>(choreUpdated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in chore services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in chore services method FindOne: {error.Message}"
            );
        }
    }

}
