using Chirp.Core.DTOs;

namespace Chirp.Core.IRepository;
public interface ICheepRepository {
    ValueTask<List<CheepDTO>> GetCheepByPage(int pageNumber, int pageSize);
    ValueTask<List<CheepDTO>> GetCheepsByAuthor(string authorId, int pageNumber, int pageSize);
    Task CreateCheep(CheepCreateDTO cheepDTO);
}
