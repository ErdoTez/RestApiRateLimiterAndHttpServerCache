namespace RestApiServerResponceCache.Services
{
    public class HttpResponseCache : IHttpResponseCache
    {
        public async Task<string> GetAllMessage(string req)
        {
            return req.ToUpper();
        }
    }
}
