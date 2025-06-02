namespace Blog.Api;

public static class Endpoints
{
    private const string baseUrl = "api";
    private const string version = "v0";
    private const string controller = "articles";
    public static class Articles
    {
        private const string Base = $"{baseUrl}/{version}/{controller}";
        public const string Post = Base;
        public const string Get = $"{Base}/{{id:int}}";
        public const string GetAll = Base;
        public const string GetTags = $"{Base}/tags";
        public const string Put = $"{Base}/{{id:int}}";
        public const string Delete = $"{Base}/{{id:int}}";
    }
}
