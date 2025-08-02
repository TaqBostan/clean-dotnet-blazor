using Bunit;
using CleanDotnetBlazor.Server.Infrastructure;
using CleanDotnetBlazor.Shared;
using Client.Components;
using Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;
using TestContext = Bunit.TestContext;

namespace CleanDotnetBlazor.AcceptanceTests.Steps;

[Binding]
public sealed class CustomersListStepDefinitions
{
    private List<CustomerBriefDto> _customers;
    private CustomerBriefDto _customerToRemove;
    private IRenderedComponent<CustomersList> _component;
    private TestContext _ctx;

    public CustomersListStepDefinitions()
    {
        _ctx = new TestContext();
    }

    [Given(@"some customers exist")]
    public void SomeCustomersExist()
    {
        _customers = new List<CustomerBriefDto>
        {
            new CustomerBriefDto { Id = 1, FirstName = "Mehdi", LastName = "Rajaei" },
            new CustomerBriefDto { Id = 2, FirstName = "Ali", LastName = "Amiri" }
        };

        var mockHandler = new MockHttpMessageHandler(request =>
        {
            if (request.Method == HttpMethod.Get)
            {
                var jsonString = JsonSerializer.Serialize(_customers);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
                };
            }
            else if (request.Method == HttpMethod.Delete)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(Constants.BaseUrl)
        };

        _ctx.Services.AddSingleton<HttpClient>(httpClient);
    }

    [When(@"the customers list component is rendered")]
    public async Task ComponentIsRendered()
    {
        _component = _ctx.RenderComponent<CustomersList>();
        await Task.Delay(100);
    }

    [Then(@"the customers should appear in the table")]
    public void CustomerAppearsInTable()
    {
        var html = _component.Markup;
        foreach (var customer in _customers)
        {
            Assert.That(html, Does.Contain(customer.FirstName));
            Assert.That(html, Does.Contain(customer.LastName));
        }
    }

    [When(@"clicking the delete button for customer A")]
    public void ClickingDelete()
    {
        _customerToRemove = _customers[0];
        var deleteButton = _component.FindAll("button").First(b => b.InnerHtml.Contains("oi-trash"));

        deleteButton.Click();

        _customers = _customers.Where(c => c.FirstName != _customerToRemove.FirstName && c.LastName != _customerToRemove.LastName).ToList();
        _component.Render();
    }

    [Then(@"customer A should be removed from the table")]
    public void CustomerIsGone()
    {
        var html = _component.Markup;
        Assert.That(html, Does.Not.Contain(_customerToRemove.FirstName));
        Assert.That(html, Does.Not.Contain(_customerToRemove.LastName));
    }
}