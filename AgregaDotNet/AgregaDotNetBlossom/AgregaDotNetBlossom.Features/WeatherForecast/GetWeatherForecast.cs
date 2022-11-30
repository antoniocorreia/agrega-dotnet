namespace AgregaDotNetBlossom.Features
{
    public class GetBlogs : PublicFeature<List<Post>>
    {
        public GetBlogs(IRepository<Post> weatherForecastRep)
        {
            WeatherForecastRep = weatherForecastRep;
        }

        public IRepository<Post> WeatherForecastRep { get; set; }

        public override async Task<List<Post>> ExecuteAsync()
        {
            return await Task.FromResult(WeatherForecastRep.Query.ToList());
        }
    }
}