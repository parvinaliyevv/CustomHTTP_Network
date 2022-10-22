namespace CustomHTTP.Client.HTTP;

public abstract class HttpHeader
{
    public HttpMethod Method { get; set; }

    public string? Content { get; set; }


    public HttpHeader() => Content = null;
}
