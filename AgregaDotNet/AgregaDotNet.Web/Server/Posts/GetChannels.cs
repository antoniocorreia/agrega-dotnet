using AgregaDotNet.Features.Posts._Entities;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Sparc.Features;

namespace AgregaDotNet.Features.Posts
{
    public class GetChannels : PublicFeature<List<Channel>>
    {
        private IConfiguration _configuration;

        public GetChannels(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override async Task<List<Channel>> ExecuteAsync()
        {
            var channels = _configuration.GetSection("Channels").Get<List<Channel>>();

            return await Task.FromResult(channels);
        }

    }
}