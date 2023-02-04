using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.Dto;

namespace SteamNewBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<MariaDbContext> _logger;
        private readonly MariaDbContext _mariaDb;
        public PostController(ILogger<MariaDbContext> logger, MariaDbContext context)
        {
            _logger = logger;
            _mariaDb = context;
        }

        [HttpPost("addPost")]
        public IActionResult AddPost([FromForm] NewPost post)
        {
            try
            {
                if(post == null) return BadRequest();
                else
                {
                    Post newPost = new()
                    {
                        Post_Title = post.Title,
                        Post_Content = post.Content,
                        Game_Id = post.GameId
                    };

                    _mariaDb.Posts.Add(newPost);
                    _mariaDb.SaveChanges();
                    return Ok(new
                    {
                        Message = "Post successfully created!"
                    });
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getPost/{id}")]
        public IActionResult GetPost([FromRoute] int id)
        {
            try
            {
                var post = _mariaDb.Posts.FirstOrDefault(p => p.Id == id);
                if (post != null) return Ok(post);
                else return NotFound();
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
                var posts = _mariaDb.Posts.ToList();

                if (posts != null) return Ok(posts);
                else return BadRequest(new
                {
                    Message = "No posts."
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("getPostsByGame/{id}")]
        public IActionResult GetPostsByGame([FromRoute] int id)
        {
            try
            {
                var posts = _mariaDb.Posts.Where(p => p.Game_Id == id);

                if (posts != null) return Ok(posts);
                else return BadRequest(new
                {
                    Message = "No posts."
                });
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("deletePost/{id}")]
        public IActionResult DeletePost([FromRoute] int id)
        {
            try
            {
                var post = _mariaDb.Posts.FirstOrDefault(p => p.Id == id);
                if (post != null)
                {
                    _mariaDb.Posts.Remove(post);
                    _mariaDb.SaveChanges();
                    return Ok(new
                    {
                        Message = "Post successfully deleted!"
                    });
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updatePost")]
        public IActionResult UpdatePost([FromForm] UpdatePost newPost)
        {
            try
            {
                var post = _mariaDb.Posts.FirstOrDefault(p => p.Id == newPost.Id);
                if (post != null)
                {
                    post.Post_Title = newPost.Post_Title;
                    post.Post_Content = newPost.Post_Content;
                    _mariaDb.SaveChanges();
                    return Ok(new
                    {
                        Message = "Post successfully updated!"
                    });
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
