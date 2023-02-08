using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Services
{
    public class PostService : IPostService
    {
        MariaDbContext _mariaDb;
        public PostService(MariaDbContext mariaDb)
        {
            _mariaDb = mariaDb;
        }

        public async Task<int> AddPost(NewPost newPostData)
        {
            Post newPost = new()
            {
                Post_Title = newPostData.Title,
                Post_Content = newPostData.Content,
                Game_Id = newPostData.GameId
            };

            await _mariaDb.Posts.AddAsync(newPost);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeletePost(int postId)
        {
            var post = await _mariaDb.Posts
                .FirstOrDefaultAsync(post => post.Id == postId);
            if (post == null) return 0;

            _mariaDb.Posts.Remove(post);
            await _mariaDb.SaveChangesAsync();
            return 1;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _mariaDb.Posts.ToListAsync();
            if (posts == null) return null;
            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _mariaDb.Posts
                .FirstOrDefaultAsync(post => post.Id == postId);
            if (post == null) return null;
            return post;
        }

        public async Task<List<Post>> GetPostsByGame(int gameId)
        {
            var test = _mariaDb.Posts
                .Where(post => post.Game_Id == gameId).ToList();
            var posts = await _mariaDb.Posts
                .Where(post => post.Game_Id == gameId).ToListAsync();
            if (posts == null) return null;
            return posts;
        }

        public async Task<int> UpdatePost(UpdatePost updatePostData)
        {
            var post = await _mariaDb.Posts
                .FirstOrDefaultAsync(p => p.Id == updatePostData.Id);
            if (post == null) return 0;

            post.Post_Title = updatePostData.Post_Title;
            post.Post_Content = updatePostData.Post_Content;
            await _mariaDb.SaveChangesAsync();
            return 1;
        }
    }
}
