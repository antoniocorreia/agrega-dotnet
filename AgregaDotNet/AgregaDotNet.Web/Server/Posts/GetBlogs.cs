using AgregaDotNet.Features.Posts._Entities;
using Microsoft.Extensions.Configuration;
using Sparc.Features;
using System.ServiceModel.Syndication;
using System.Xml;

namespace AgregaDotNet.Features.Posts
{
    public class GetBlogs : PublicFeature<List<Blog>>
    {
        private IConfiguration _configuration;

        public GetBlogs(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override async Task<List<Blog>> ExecuteAsync()
        {
            var blogs = _configuration.GetSection("Blogs").Get<List<Blog>>();

            return await Task.FromResult(blogs);
        }

    }
}