namespace Application.Interfaces;

public interface IBaseService<DTO, InputCreateDTO, InputUpdateDTO>
{
    Task<IEnumerable<DTO>> FindAll();
    Task<DTO?> FindOne(int id);
    Task<DTO> Create(InputCreateDTO model);
    Task<DTO> Update(int id, InputUpdateDTO model);
    Task<bool> Delete(int id);
}
