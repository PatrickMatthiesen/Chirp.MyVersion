using Chirp.Core.DTOs;
using Chirp.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chirp.Infrastructure.Repositories;
public class AuthorRepository(ChirpDBContext _context, ILogger<AuthorRepository> _logger) : IAuthorRepository {

    public async ValueTask<List<CheepDTO>> GetMyTimeline(string id, int pageNumber, int pageSize) {
        var user = await _context.Authors.Include(u => u.Followers).FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            _logger.LogError("User with id {id} not found.", id);
            return [];
        }

        var cheeps = await _context.Cheeps
            .Where(m => m.Author.Id == user.Id || user.Followers.Contains(m.Author))
            .OrderByDescending(m => m.TimeStamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CheepDTO
            {
                Message = x.Message,
                TimeStamp = x.TimeStamp,
                AuthorName = x.Author.UserName,
                DisplayName = x.Author.DisplayName ?? x.Author.UserName,
                GithubId = x.Author.ExternalLogins.FirstOrDefault(l => l.LoginProvider == "GitHub").ProviderKey,
            })
            .ToListAsync();

        return cheeps;
    }

    public async ValueTask<List<FollowerDTO>> GetFollowers(string authorId) =>
        await _context.Authors
            .Where(a => a.Id == authorId)
            .SelectMany(a => a.Followers)
            .Select(f => new FollowerDTO
            {
                UserName = f.UserName,
                DisplayName = f.DisplayName ?? f.UserName,
            }).ToListAsync();

    public async ValueTask<(bool success, string err)> FollowUser(string userId, string userToFollow) {
        var user = await _context.Authors.Include(u => u.Followers).FirstOrDefaultAsync(u => u.Id == userId);
        var toFollow = await _context.Authors.FirstOrDefaultAsync(a => a.UserName == userToFollow);

        if (user == null)
        {
            return (false, $"Could not find user with name {userId}");
        }

        if (toFollow == null)
        {
            return (false, $"Could not find user with name {userToFollow}");
        }

        user.Followers.Add(toFollow);

        await _context.SaveChangesAsync();
        return (true, "");
    }

    public async ValueTask<(bool success, string err)> UnfollowUser(string userId, string userToUnfollow) {
        var user = await _context.Authors.Include(u => u.Followers).FirstOrDefaultAsync(u => u.Id == userId);
        var toUnfollow = await _context.Authors.FirstOrDefaultAsync(a => a.UserName == userToUnfollow);

        if (user == null)
        {
            return (false, $"Could not find user with name {userId}");
        }

        if (toUnfollow == null)
        {
            return (false, $"Could not find user with name {userToUnfollow}");
        }

        user.Followers.Remove(toUnfollow);

        await _context.SaveChangesAsync();
        return (true, "");
    }
}
