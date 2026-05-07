using MongoDB.Driver;
using UnitsApi.Models;
namespace UnitsApi.Services;

public class PostService
{
    private readonly IMongoCollection<Post> _Collection;

    public PostService()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("unitsDatabase");

        _Collection = database.GetCollection<Post>("posts");

    }

    public async Task<List<Post>> GetPost() //reading all posts in the collection
    {
        return await _Collection.Find(_ => true).ToListAsync();
    }

    public async Task CreatePost(Post post) //adding a post to the collection
     {
        post.CreatedAt = DateTime.UtcNow;
        post.UpdatedAt = DateTime.UtcNow;
      await _Collection.InsertOneAsync(post);
    }
    public async Task<bool> UpdatePost(string id, Post updatedPost) // updating a post
    {
        updatedPost.UpdatedAt = DateTime.UtcNow;
        var res = await _Collection.ReplaceOneAsync(p => p.Id == id , updatedPost);
        return res.ModifiedCount > 0;
    }
    public async Task<Post?> GetPostById(string id) //reading a specific post with an id
    {
        return await _Collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    public async Task<bool> DeletePost(string id)//delete a post
    {
        var res = await _Collection.DeleteOneAsync(p => p.Id == id);
        return res.DeletedCount > 0; 
    }
}