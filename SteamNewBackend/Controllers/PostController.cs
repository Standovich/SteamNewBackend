using Microsoft.AspNetCore.Mvc;
using SteamNewBackend.Database;
using SteamNewBackend.Models;
using SteamNewBackend.Models.RequestClasses;

namespace SteamNewBackend.Controllers
{
    [ApiController]
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
        public IActionResult AddPost([FromForm] FormPostRequest post)
        {
            try
            {
                Post newPost = new()
                {
                    Post_Title = post.Title,
                    Post_Content = post.Content
                };

                if (_mariaDb.Posts.Where(p => p.Post_Title == newPost.Post_Title).Any())
                    return BadRequest("Post already exists!");
                else
                {
                    _mariaDb.Posts.Add(newPost);
                    _mariaDb.SaveChanges();
                    return Ok(post);
                }
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
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
                return NotFound("Database couldn't be reached.");
            }
        }

        [HttpGet("getPosts")]
        public IActionResult GetPosts()
        {
            try
            {
                var posts = _mariaDb.Posts.ToList();

                if (posts != null) return Ok(posts);
                else return NotFound();
            }
            catch
            {
                return NotFound("Database couldn't be reached.");
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
                    return Ok(post);
                }
                else return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updatePost")]
        public IActionResult UpdatePost([FromForm] Post newPost)
        {
            try
            {
                var post = _mariaDb.Posts.FirstOrDefault(p => p.Id == newPost.Id);
                if (post != null)
                {
                    post.Post_Title = newPost.Post_Title;
                    post.Post_Content = newPost.Post_Content;
                    _mariaDb.SaveChanges();
                    return Ok(newPost);
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
