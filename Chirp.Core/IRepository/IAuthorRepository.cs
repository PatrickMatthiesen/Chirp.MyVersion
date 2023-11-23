using Chirp.Core.DTOs;

namespace Chirp.Core.IRepository;
public interface IAuthorRepository {
    ValueTask<AuthorDetailsDTO> GetAuthorDetails(string id);
    ValueTask<(bool success, string err)> FollowUser(string userName, string userToFollow);
    ValueTask<List<FollowerDTO>> GetFollowers(string authorId);
    ValueTask<List<CheepDTO>> GetMyTimeline(string id, int pageNumber, int pageSize);
    ValueTask<(bool success, string err)> UnfollowUser(string userName, string userToUnfollow);
    ValueTask<bool> DeleteAuthor(string id);
}