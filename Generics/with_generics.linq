<Query Kind="Program" />

void Main()
{
	Hasher.Hash(new Post()).Dump();
	Hasher.Hash(new Comment()).Dump();
}

public interface IHashingStrategy
{
	int Hash(Content content);
}

public class Content
{
	public string Id { get; set; } = "id";
	public string Author { get; set; } = "author";
	public string Text { get; set; } = "text";
}

public class Content<T> : Content where T : IHashingStrategy { }

public class Post : Content<ContentHashingStrategy> { }

public class Comment : Content<CommentHashingStrategy>
{
	public string PostId { get; set; } = "postid";
}

public class ContentHashingStrategy : IHashingStrategy
{
	public int Hash(Content content)
	{
		return content.Text.Length
			+ content.Author.Length;
	}
}

public class CommentHashingStrategy : IHashingStrategy
{
	public int Hash(Content content)
	{
		var comment = content as Comment;
		return comment.Text.Length
			+ comment.Author.Length
			+ comment.PostId.Length;
	}
}

public class Hasher
{
	public static int Hash<T>(Content<T> content)
		where T : IHashingStrategy, new()
	{
		return Activator.CreateInstance<T>().Hash(content);
	}
}