<Query Kind="Program" />

void Main()
{
	Hasher.Hash(new Post()).Dump();
	Hasher.Hash(new Comment()).Dump();
}

public interface IHashingStrategy {
	int Hash(Content content);
}

public abstract class HashingStrategy<T> : IHashingStrategy
	where T : Content 
{
	public int Hash(Content content) => Hash((T) content);

	protected abstract int Hash(T content);
}

public class Content
{
	public string Id { get; set; } = "id";
	public string Author { get; set; } = "author";
	public string Text { get; set; } = "text";
}

public class Content<T> : Content where T : IHashingStrategy, new() {}

public class Post : Content<ContentHashingStrategy> { }

public class Comment : Content<CommentHashingStrategy>
{
	public string PostId { get; set; } = "postid";
}

public class ContentHashingStrategy : HashingStrategy<Post>
{
	protected override int Hash(Post content)
	{
		return content.Text.Length
			+ content.Author.Length;
	}
}

public class CommentHashingStrategy : HashingStrategy<Comment>
{
	protected override int Hash(Comment comment)
	{
		return comment.Text.Length
			+ comment.Author.Length
			+ comment.PostId.Length;
	}
}

public class Hasher
{
	public static int Hash<H>(Content<H> content)
		where H : IHashingStrategy, new()
	{
		return Activator.CreateInstance<H>().Hash(content);
	}
}