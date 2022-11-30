namespace AgregaDotNetBlossom.Features
{
    public class GetBlogPosts : PublicFeature<List<Post>>
    {
        public GetBlogPosts(IRepository<Post> weatherForecastRep)
        {
            WeatherForecastRep = weatherForecastRep;
        }

        public IRepository<Post> WeatherForecastRep { get; set; }

        private static readonly string[] Summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public override async Task<List<Post>> ExecuteAsync()
        {
            await WeatherForecastRep.AddAsync(Enumerable.Range(1, 5).Select(index => new Post
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList());

            return await Task.FromResult(WeatherForecastRep.Query.ToList());
        }
    }
}