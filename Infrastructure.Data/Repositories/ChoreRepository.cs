using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories;

public class ChoreRepository(AppDbContext context) 
: BaseRepository<Chore>(context), IChoreRepository
{
}
