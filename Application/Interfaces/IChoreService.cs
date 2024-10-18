using Application.DTOs.Chore;

namespace Application.Interfaces;

public interface IChoreService 
: IBaseService<ChoreDTO, ChoreInputCreateDTO, ChoreInputUpdateDTO>
{
    Task<IEnumerable<ChoreDTO>> FindAllByUser(int userId);

}
