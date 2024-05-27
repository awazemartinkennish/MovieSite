namespace MovieSite.ApiService.Screens
{
    public record GetScreenView
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Capacity { get; init; }

        public List<ScreeningView> Screenings { get; init; }

        public record ScreeningView
        {
            public string MovieTitle { get; init; }
            public DateTimeOffset ScreeningTime { get; init; }
        }
    }
}
