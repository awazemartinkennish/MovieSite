using MovieSite.ApiService.Movies;
using MovieSite.ApiService.MovieScreenings;
using MovieSite.ApiService.Screens;

namespace MovieSite.Web;

public class MovieApiClient(HttpClient httpClient)
{
    public async Task<List<GetMovieView>> GetMovies(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<GetMovieView>? movies = null;

        await foreach (var movie in httpClient.GetFromJsonAsAsyncEnumerable<GetMovieView>("/movie", cancellationToken))
        {
            if (movies?.Count >= maxItems)
            {
                break;
            }
            if (movie is not null)
            {
                movies ??= [];
                movies.Add(movie);
            }
        }

        return movies ?? [];
    }

    public async Task<List<GetScreenView>> GetScreens(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<GetScreenView>? screens = null;

        await foreach (var screen in httpClient.GetFromJsonAsAsyncEnumerable<GetScreenView>("/screen", cancellationToken))
        {
            if (screens?.Count >= maxItems)
            {
                break;
            }
            if (screen is not null)
            {
                screens ??= [];
                screens.Add(screen);
            }
        }

        return screens ?? [];
    }

    public async Task<List<GetMovieScreeningView>> GetMovieScreenings(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<GetMovieScreeningView>? screenings = null;
        
        await foreach (var screening in httpClient.GetFromJsonAsAsyncEnumerable<GetMovieScreeningView>("/moviescreening", cancellationToken))
        {
            if (screenings?.Count >= maxItems)
            {
                break;
            }
            if (screening is not null)
            {
                screenings ??= [];
                screenings.Add(screening);
            }
        }

        return screenings ?? [];
    }
}
