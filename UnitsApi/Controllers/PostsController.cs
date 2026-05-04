using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UnitsApi.Models;
using UnitsApi.Services;

[ApiController]
[Route("api/[controller]")]

public class PostsController : ControllerBase
{
    private readonly PostService _postService;
    public PostsController(PostService postService)// the dependency injection
    {
        _postService = postService;
    }
    [HttpGet]
    public async Task<ActionResult<List<Post>>> Get()
    {
        return Ok(await _postService.GetPost());
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Post>> Get(string id)
    {
        var post = _postService.GetPostById(id);
        if (post is null)
            return NotFound();
        else
            return Ok(post);
        
    }
    [HttpPost]
    public async Task<IActionResult> Post(Post newPost)
    {
        await _postService.CreatePost(newPost);
        return CreatedAtAction(nameof(Get),new{id=newPost.Id},newPost);
    }
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Post updatedPost)
    {
        if(id!=updatedPost.Id)
            return BadRequest();
        var res = await _postService.UpdatePost(id,updatedPost);
        if(!res)
            return NotFound();
        return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var res = await _postService.DeletePost(id);
        if(!res)
            return NotFound();
        return NoContent();
    }
}