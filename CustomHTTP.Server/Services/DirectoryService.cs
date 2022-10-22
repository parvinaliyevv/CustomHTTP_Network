namespace CustomHTTP.Server.Services;

public static class DirectoryService
{
    public static string GetProjectParentFolder()
        => Directory.GetCurrentDirectory().Split("\\bin")[0] + '\\';
}
