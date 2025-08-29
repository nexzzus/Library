namespace Library.Share.Core;

public class Response<T>
{
    public bool Succeded { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public T? Result { get; set; }
}