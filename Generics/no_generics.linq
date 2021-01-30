<Query Kind="Program" />

void Main()
{
	Hasher.Hash(new Post()).Dump();
	Hasher.Hash(new Comment()).Dump();
}

public class Content
{
	public string Id { get; set; } = "id";
	public string Author { get; set; } = "author";
	public string Text { get; set; } = "text";
}

public class Post : Content { }

public class Comment : Content
{
	public string PostId { get; set; } = "postid";
}

public class ContentHashingStrategy
{
	public static int Hash(Post post) =>
		post.Text.Length
		+ post.Author.Length;
}

public class CommentHashingStrategy
{
	public static int Hash(Comment comment) =>
		comment.Text.Length
		+ comment.Author.Length
		+ comment.PostId.Length;
}

public class Hasher
{
	public static int Hash(Content content)
	{
		if (content is Post p) return ContentHashingStrategy.Hash(p);
		if (content is Comment c) return CommentHashingStrategy.Hash(c);
		return 0;
	}
}