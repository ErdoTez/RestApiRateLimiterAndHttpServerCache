namespace RestApiServerResponceCache.Services
{
    public interface IHttpResponseCache
    {
        Task<string> GetAllMessage(string req);
    }
}
