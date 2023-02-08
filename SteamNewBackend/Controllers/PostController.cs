using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;
using SteamNewBackend.Services;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly MariaDbContext _mariaDb;
        public PostController(IPostService postService, MariaDbContext context)
        {
            _postService = postService;
            _mariaDb = context;
        }

        [HttpPost("addPost")]
        public async Task<IActionResult> AddPost([FromForm] NewPost newPostData)
        {
            try
            {
                if (newPostData == null) return BadRequest();
                
                await _postService.AddPost(newPostData);
                return Ok(new
                {
                    Message = "Post successfully created!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getPost/{postId}")]
        public async Task<IActionResult> GetPost([FromRoute] int postId)
        {
            try
            {
                var post = await _postService.GetPost(postId);
                if(post == null) return NotFound();
                return Ok(post);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getPosts")]
        public IActionResult GetPosts()
        {
            try
            {
                var posts = _postService.GetAllPosts();
                if (posts == null) return NotFound();
                return Ok(posts);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getPostsByGame/{gameId}")]
        public async Task<IActionResult> GetPostsByGame([FromRoute] int gameId)
        {
            try
            {
                var posts = await _postService.GetPostsByGame(gameId);
                if (posts == null) return NotFound();
                return Ok(posts);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("deletePost/{postId}")]
        public async Task<IActionResult> DeletePost([FromRoute] int postId)
        {
            try
            {
                var result = await _postService.DeletePost(postId);
                if(result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Post successfully deleted!"
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updatePost")]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePost updatePostData)
        {
            try
            {
                if(updatePostData == null) return NotFound();

                var result = await _postService.UpdatePost(updatePostData);
                if(result == 0) return NotFound();
                return Ok(new
                {
                    Message = "Post successfully updated!"
                });
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
