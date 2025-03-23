using CleanDotnetBlazor.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using CleanDotnetBlazor.Server.Infrastructure;
using CleanDotnetBlazor.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanDotnetBlazor.Server.Controllers
{
    public class WeatherForecasts : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetWeatherForecasts);
        }

        public async Task<Ok<IEnumerable<WeatherForecast>>> GetWeatherForecasts(ISender sender)
        {
            var forecasts = await sender.Send(new GetWeatherForecastsQuery());

            return TypedResults.Ok(forecasts);
        }
    }
}