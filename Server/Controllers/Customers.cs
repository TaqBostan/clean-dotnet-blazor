using CleanDotnetBlazor.Application.Customers.Commands.CreateCustomer;
using CleanDotnetBlazor.Application.Customers.Commands.DeleteCustomer;
using CleanDotnetBlazor.Application.Customers.Queries.GetCustomers;
using CleanDotnetBlazor.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using CleanDotnetBlazor.Server.Infrastructure;
using CleanDotnetBlazor.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanDotnetBlazor.Server.Controllers
{
    public class Customers : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetCustomers)
                .MapGet(GetCustomer, "{id}")
                .MapPost(UpsertCustomer)
                .MapDelete(DeleteCustomer, "{id}");
        }

        public async Task<Created<int>> UpsertCustomer(ISender sender, UpsertCustomerCommand command)
        {
            var id = await sender.Send(command);

            return TypedResults.Created($"/{nameof(Customers)}/{id}", id);
        }

        public async Task<Ok<IEnumerable<CustomerBriefDto>>> GetCustomers(ISender sender)
        {
            var customers = await sender.Send(new GetCustomersQuery());

            return TypedResults.Ok(customers);
        }

        public async Task<Ok<CustomerDto>> GetCustomer(ISender sender, int id)
        {
            var customer = await sender.Send(new GetCustomerQuery(id));

            return TypedResults.Ok(customer);
        }

        public async Task<NoContent> DeleteCustomer(ISender sender, int id)
        {
            await sender.Send(new DeleteCustomerCommand(id));

            return TypedResults.NoContent();
        }
    }
}