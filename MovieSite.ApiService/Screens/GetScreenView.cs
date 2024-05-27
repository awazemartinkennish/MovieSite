namespace MovieSite.ApiService.Screens
{
    public record GetScreenView
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public int Capacity { get; init; }

        public required List<ScreeningView> Screenings { get; init; }

        public record ScreeningView
        {
            public int ScreeningId { get; init; }
            public required string MovieTitle { get; init; }
            public DateTimeOffset ScreeningTime { get; init; }
        }
    }
}
