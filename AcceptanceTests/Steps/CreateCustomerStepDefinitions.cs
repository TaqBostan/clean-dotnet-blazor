using Bunit;
using CleanDotnetBlazor.Shared;
using Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using TestContext = Bunit.TestContext;

namespace CleanDotnetBlazor.AcceptanceTests.Steps;

[Binding]
public sealed class CreateCustomerStepDefinitions
{
    private CustomerDto _customer;
    private IRenderedComponent<Customer> _component;
    private bool _onSavedCalled;

    public CreateCustomerStepDefinitions()
    {
    }

    [Given("a new customer")]
    public void GivenANewCustomer()
    {
        _customer = new CustomerDto
        {
            FirstName = "Mehdi",
            LastName = "Rajaei",
            PhoneNumber = "123",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today)
        };
    }

    [When("the new customer is valid")]
    public async Task TheCustomerIsValidAsync()
    {
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created
        };

        var mockHandler = new MockHttpMessageHandler(mockResponse);
        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri("https://localhost:7045")
        };

        var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(httpClient);

        _component = ctx.RenderComponent<Customer>(parameters => parameters
            .Add(p => p.OnSaved, EventCallback.Factory.Create(this, () => _onSavedCalled = true))
        );

        await _component.Instance.Open(0);

        _component.Find("#first-name").Change(_customer.FirstName);
        _component.Find("#last-name").Change(_customer.LastName);
        _component.Find("#date-of-birth").Change(_customer.DateOfBirth.ToString("yyyy-MM-dd"));
        _component.Find("#phone-number").Change(_customer.PhoneNumber);

        await _component.Find("form").SubmitAsync();
    }

    [Then("the customer is created successfully")]
    public void TheCustomerIsCreatedSuccessfully()
    {
        Assert.AreEqual("none", _component.Instance.ModalDisplay);
        Assert.True(_onSavedCalled, "OnSaved event should be triggered upon successful creation.");
    }
}