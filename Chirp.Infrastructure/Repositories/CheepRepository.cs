using Chirp.Core.DTOs;
using Chirp.Core.IRepository;
using Chirp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository(ChirpDBContext _context) : ICheepRepository {
    public async ValueTask<List<CheepDTO>> GetCheepByPage(int pageNumber, int pageSize) => 
        await _context.Cheeps
            .OrderByDescending(c => c.TimeStamp)
            .Skip((pageNumber-1) * pageSize)
            .Take(pageSize)
            .Select(c => new CheepDTO
            {
                Message = c.Message,
                TimeStamp = c.TimeStamp,
                AuthorId = c.AuthorId,
                AuthorName = c.Author.DisplayName ?? c.Author.UserName!,
            }).ToListAsync();

    public async ValueTask<List<CheepDTO>> GetCheepsByAuthor(string authorName, int pageNumber, int pageSize) =>
        await _context.Authors
            .Where(a => a.UserName == authorName)
            .SelectMany(a => a.Cheeps)
            .OrderByDescending(c => c.TimeStamp)
            .Skip((pageNumber-1) * pageSize)
            .Take(pageSize)
            .Select(c => new CheepDTO
            {
                Message = c.Message,
                TimeStamp = c.TimeStamp,
                AuthorId = c.AuthorId,
                AuthorName = c.Author.DisplayName ?? c.Author.UserName!,
            }).ToListAsync();

    public async Task CreateCheep(CheepDTO cheepDTO) {
        var author = await _context.Authors.Include(a => a.Cheeps).FirstOrDefaultAsync(a => a.Id == cheepDTO.AuthorId);

        if (author == null)
        {
            throw new ArgumentException($"Could not find author with id {cheepDTO.AuthorId}");
        }

        author.Cheeps.Add(new Cheep()
        {
            Message = cheepDTO.Message,
            TimeStamp = DateTime.UtcNow,
            Author = author,
        });

        await _context.SaveChangesAsync();
    }
}
