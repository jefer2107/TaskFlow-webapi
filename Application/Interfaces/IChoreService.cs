using Application.DTOs.Chore;

namespace Application.Interfaces;

public interface IChoreService 
: IBaseService<ChoreDTO, ChoreInputCreateDTO, ChoreInputUpdateDTO>
{
    Task<IEnumerable<ChoreDTO>> FindAllByUser(int userId);
    Task<ChoreDTO> FindOneByUser(int id, int userId);
    Task<ChoreDTO> UpdateByUser(int id, int userId, ChoreInputUpdateDTO model);
    Task<bool> DeleteByUser(int id, int userId);

}
