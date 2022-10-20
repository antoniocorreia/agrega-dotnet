using AgregaDotNet.Features.Posts._Entities;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Sparc.Features;

namespace AgregaDotNet.Features.Posts
{
    public class GetYoutubeVideos : PublicFeature<List<Post>>
    {
        private IConfiguration _configuration;
        public GetYoutubeVideos(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override async Task<List<Post>> ExecuteAsync()
        {
            var channels = _configuration.GetSection("Channels").Get<List<Channel>>();

            List<Post> posts = new List<Post>();
            try
            {
                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = _configuration.GetValue<string>("YoutubeKey") });

                foreach (var c in channels)
                {
                    var channelsListRequest = yt.Channels.List("contentDetails");
                    channelsListRequest.ForUsername = c.Id;


                    var channelsListResponse = channelsListRequest.Execute();
                    if (channelsListResponse.Items == null)
                    {
                        channelsListRequest.ForUsername = null;
                        channelsListRequest.Id = c.Id;
                        channelsListResponse = channelsListRequest.Execute();
                    }

                    foreach (var channel in channelsListResponse.Items)
                    {
                        // of videos uploaded to the authenticated user's channel.
                        var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
#pragma warning disable CS0219 // The variable 'nextPageToken' is assigned but its value is never used
                        var nextPageToken = "";
#pragma warning restore CS0219 // The variable 'nextPageToken' is assigned but its value is never used
                        //while (nextPageToken != null)
                        //{
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 5;
                        //playlistItemsListRequest.PageToken = nextPageToken;
                        // Retrieve the list of videos uploaded to the authenticated user's channel.
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        foreach (var playlistItem in playlistItemsListResponse.Items)
                        {
                            // Print information about each video.
                            //Console.WriteLine("Video Title= {0}, Video ID ={1}", playlistItem.Snippet.Title, playlistItem.Snippet.ResourceId.VideoId);
                            Post video = new Post(c);
                            //video.VideoId = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId;
                            video.Id = playlistItem.Snippet.ResourceId.VideoId;
                            video.Subject = playlistItem.Snippet.Title;
                            video.Description = playlistItem.Snippet.Description;
                            video.ImgUrl = playlistItem.Snippet.Thumbnails.High.Url;
                            video.PublishDate = playlistItem.Snippet.PublishedAt.Value;
                            video.Url = "https://www.youtube.com/watch?v=" + video.Id;

                            posts.Add(video);
                        }
                        //    nextPageToken = playlistItemsListResponse.NextPageToken;
                        //}
                    }
                }
                
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {
                return null;
            }

            return await Task.FromResult(posts);
        }
    }
}
