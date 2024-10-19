using Application.DTOs.Category;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;

namespace Application.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IUnityOfWork uof
) 
: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnityOfWork _uof = uof;

    public async Task<IEnumerable<CategoryDTO>> FindAll()
    {
        try
        {
            IEnumerable<Category> categories = await _categoryRepository.FindAllWithChores();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindAll: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindAll: {error.Message}"
            );
        }
    }

    public async Task<IEnumerable<CategoryDTO>> FindAllByUser(int userId)
    {
        try
        {
            IEnumerable<CategoryDTO> categories = await FindAll();

            IEnumerable<CategoryDTO> newCategories = categories.Where(x => x.UserId == userId).ToList();

            return newCategories;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindAllByUser: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindAllByUser: {error.Message}"
            );
        }
    }

    public async Task<CategoryDTO?> FindOne(int id)
    {
        try
        {
            Category category = await _categoryRepository.FindOne(id);
            return _mapper.Map<CategoryDTO>(category);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<CategoryDTO> FindOneByUser(int id, int userId)
    {
        try
        {
            Category chore = await _categoryRepository.FindOneByUser(id, userId);
            return _mapper.Map<CategoryDTO>(chore);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOneByUser: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOneByUser: {error.Message}"
            );
        }
    }

    public async Task<CategoryDTO> Create(CategoryInputCreateDTO model)
    {
        try
        {
            Category category = _mapper.Map<Category>(model);

            var categoryCreated = await _categoryRepository.Create(category);

            await _uof.Commit();

            return _mapper.Map<CategoryDTO>(categoryCreated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method Create: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method Create: {error.Message}"
            );
        }
    }

    public async Task<CategoryDTO> Update(int id, CategoryInputUpdateDTO model)
    {
        try
        {
            var categoryById = await FindOne(id) ?? 
            throw new Exception($"Error in category services method Update: user id:{id} not found");

            Category category = _mapper.Map<Category>(model);

            Category categoryUpdate = await _categoryRepository.Update(id, category);

            await _uof.Commit();

            return _mapper.Map<CategoryDTO>(categoryUpdate);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<CategoryDTO> UpdateByUser(int id, int userId, CategoryInputUpdateDTO model)
    {
        try
        {
            var categoryById = await FindOneByUser(id, userId) ?? 
            throw new Exception(
                $"Error in category services method Delete: user id:{id} or userId:{userId} not found"
            );

            Category category = _mapper.Map<Category>(model);

            Category categoryUpdated = await _categoryRepository.Update(id, category);

            await _uof.Commit();

            return _mapper.Map<CategoryDTO>(categoryUpdated);
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var categoryById = await FindOne(id) ?? 
            throw new Exception($"Error in category services method Delete: user id:{id} not found");


            bool choreDeleted = await _categoryRepository.Delete(id);

            await _uof.Commit();

            return choreDeleted;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOne: {error.Message}"
            );
        }
    }

    public async Task<bool> DeleteByUser(int id, int userId)
    {
        try
        {
            var categoryByUser = await FindOneByUser(id, userId) ?? 
            throw new Exception(
                $"Error in category services method Delete: user id:{id} or userId:{userId} not found"
            );


            bool categoryDeleted = await _categoryRepository.Delete(id);

            await _uof.Commit();

            return categoryDeleted;
        }
        catch (Exception error)
        {
            
            if(error.InnerException != null)
                throw new Exception(
                    $"Error in category services method FindOne: {error.InnerException}"
                );

            throw new Exception(
                $"Error in category services method FindOne: {error.Message}"
            );
        }
    }

}
