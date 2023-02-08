using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public interface IPostService
    {
        Task<int> AddPost(NewPost post);
        Task<Post> GetPost(int id);
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetPostsByGame(int id);
        Task<int> DeletePost(int id);
        Task<int> UpdatePost(UpdatePost newPost);
    }
}
